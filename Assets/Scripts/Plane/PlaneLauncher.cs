using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneLauncher : MonoBehaviour
{
    [Header("Dependencies")]
    public PlaneController planeController; // 引用飞行控制器
    public ChangeCamera cameraSwitcher;       // 引用摄像机切换器
    public Camera paperPlaneCamera;             // 纸飞机上的相机 (用于屏幕坐标转换)
    public Transform launchPoint;               // 飞机弹射的初始位置 (空GameObject)

    [Header("Launch Settings")]
    public float maxDragDistance = 5f;          // 鼠标最大可拖拽距离
    public float launchForceMultiplier = 5f;    // 拖拽距离转换为速度的乘数
    public float minLaunchForce = 1f;           // 最小弹射速度 (防止距离太短没有速度)
    public float launchHeightOffset = 1f;       // 弹射后飞机稍微抬高一点

    private Rigidbody rb;
    private Vector3 dragStartPos;           // 鼠标拖拽开始时飞机的屏幕坐标
    private Vector3 initialPlanePos;        // 飞机在弹射前的初始位置
    private bool isDragging = false;
    private bool hasLaunched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError("Rigidbody missing on PaperPlaneLauncher!"); enabled = false; return; }
        if (planeController == null) { Debug.LogError("PaperPlaneController missing on PaperPlaneLauncher!"); enabled = false; return; }
        if (cameraSwitcher == null) { Debug.LogError("CameraSwitcher missing on PaperPlaneLauncher!"); enabled = false; return; }
        if (paperPlaneCamera == null) { Debug.LogError("PaperPlaneCamera missing on PaperPlaneLauncher!"); enabled = false; return; }
        if (launchPoint == null) { Debug.LogError("LaunchPoint missing on PaperPlaneLauncher! Please create an empty GameObject for launch point."); enabled = false; return; }

        planeController.enabled = false; // 初始禁用飞行控制器
        rb.isKinematic = true;          // 初始设置为Kinematic

        // 设置飞机初始位置
        transform.position = launchPoint.position;
        transform.rotation = launchPoint.rotation;
        initialPlanePos = launchPoint.position;
    }

    public void ActivateLauncher()
    {
        // 激活鼠标光标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Launcher Activated. Drag the plane to launch.");
    }

    public void DeactivateLauncher()
    {
        // 禁用鼠标光标（在飞行控制器中重新锁定）
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (hasLaunched || cameraSwitcher.IsTransitioning() || !cameraSwitcher.GetCurrentActiveCamera().Equals(paperPlaneCamera)) return; // 弹射后或切换中禁用

        // 鼠标按下开始拖拽
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = paperPlaneCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Plane"))) // 假设纸飞机在"Plane"层
            {
                if (hit.collider.gameObject == gameObject) // 确保点击的是纸飞机本身
                {
                    isDragging = true;
                    // 记录拖拽开始时飞机的屏幕坐标（Z轴深度）
                    dragStartPos = paperPlaneCamera.WorldToScreenPoint(transform.position);
                    initialPlanePos = transform.position; // 记录实际的世界坐标
                }
            }
        }

        // 鼠标拖拽中
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragStartPos.z);
            Vector3 currentWorldPoint = paperPlaneCamera.ScreenToWorldPoint(currentScreenPoint);

            // 计算拖拽距离和方向 (限制在水平面)
            Vector3 dragDirection = currentWorldPoint - initialPlanePos;
            dragDirection.y = 0; // 只在水平方向拖拽

            // 限制最大拖拽距离
            if (dragDirection.magnitude > maxDragDistance)
            {
                dragDirection = dragDirection.normalized * maxDragDistance;
            }

            // 更新飞机位置 (Kinematic模式下直接设置Transform)
            transform.position = initialPlanePos + dragDirection;
        }

        // 鼠标松开结束拖拽，进行弹射
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            DeactivateLauncher(); // 禁用鼠标光标

            // 计算弹射速度
            Vector3 launchDirection = initialPlanePos - transform.position; // 弹射方向与拖拽方向相反
            launchDirection.y = 0; // 仍然只考虑水平方向

            float launchMagnitude = launchDirection.magnitude * launchForceMultiplier;
            launchMagnitude = Mathf.Max(launchMagnitude, minLaunchForce); // 确保最小速度

            // 恢复飞机到初始位置，然后施加力 (模拟弹簧弹回)
            transform.position = initialPlanePos;
            rb.isKinematic = false; // 允许物理引擎控制

            // 施加水平初速度
            Vector3 launchVelocity = launchDirection.normalized * launchMagnitude;
            rb.velocity = launchVelocity;

            // 弹射后给一个小的向上力，模拟抬升
            rb.AddForce(Vector3.up * launchHeightOffset, ForceMode.Impulse);

            hasLaunched = true;
            planeController.enabled = true; // 激活飞行控制器
            //planeController.OnLaunch(rb); // 通知飞行控制器已弹射

            Debug.Log($"纸飞机弹射！方向: {launchDirection.normalized}, 速度: {launchMagnitude}");
        }
    }

    public bool HasLaunched()
    {
        return hasLaunched;
    }

    public void ResetLauncher()
    {
        hasLaunched = false;
        isDragging = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = launchPoint.position;
        transform.rotation = launchPoint.rotation;
        planeController.enabled = false;
        // 如果需要，重新激活鼠标光标
    }
}

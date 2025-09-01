using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; // 移除 Unity.VisualScripting，如果你的项目不使用它

public class PlaneController : MonoBehaviour
{
    [Header("References")]
    public Camera planeCamera;          // 子物体相机
    public Transform plane;             // 飞机本体
    private Rigidbody rb;

    [Header("Drag Settings")]
    public float dragRadius = 1f;       // 最大拖拽范围（相机前方的小区域）
    public float launchPower = 2f;      // 发射力度 (降低默认值)
    public float maxLaunchSpeed = 2f;   // 限制最大发射速度 (降低默认值)

    [Header("Flight Settings")]
    public float gravity = 1.0f;        // 模拟下落 (增加默认值，确保飞机往下掉)
    public float moveForce = 0.001f;        // 操控力 (降低默认值)
    public float rotateSpeed = 50f;     // 旋转速度
    public float maxFlightSpeed = 0.1f; // 最大飞行速度
    
    [Header("Rigidbody Physics")]
    public float flightDrag = 50000f;     // 飞行时的线性阻力
    public float flightAngularDrag = 0.8f; // 飞行时的角阻力
    public float dragPhaseDrag = 10f; // 拖拽阶段的阻力（防止发射后瞬移）
    
    private Vector3 initialCameraLocalOffset;
    private Vector3 Pos;
    private Quaternion initialCameraLocalRotation; // 记录相机相对于飞机的初始旋转

    private Vector3 initialPlanePosition; // 记录飞机初始位置，用于拖拽平面的Y轴
    private bool isDragging = false;
    private bool isFlying = false;
    private bool canDrag = false;
    private bool canFly = false;

    // 事件订阅和取消订阅
    private void OnEnableDrag() => canDrag = true;
    private void OnDisableDrag() => canDrag = false;
    private void OnEnableMove() => canFly = true;
    private void OnDisableMove() => canFly = false;

    private void OnDisable()
    {
        EventManager.OnDisablePlaneMovement -= OnDisableMove;
        EventManager.OnDisablePlaneDrag -= OnDisableDrag;
    }

    private void OnEnable()
    {
        EventManager.OnEnablePlaneMovement += OnEnableMove;
        EventManager.OnEnablePlaneDrag += OnEnableDrag;
    }

    void Start()
    {
        rb = plane.GetComponent<Rigidbody>();
        rb.isKinematic = true; // 拖拽阶段不受物理影响

        initialPlanePosition = plane.position; // 记录飞机的初始位置，用于拖拽时的Y轴固定

        // 计算相机相对于飞机的初始局部偏移和旋转
        if (plane != null && planeCamera != null)
        {
            Pos=plane.position-planeCamera.transform.position;
            initialCameraLocalOffset = plane.InverseTransformPoint(planeCamera.transform.position);
            initialCameraLocalRotation = Quaternion.Inverse(plane.rotation) * planeCamera.transform.rotation;
        }

        // 初始化Rigidbody的阻力设置
        rb.drag = dragPhaseDrag; // 拖拽阶段高阻力，防止发射瞬间速度过快
        rb.angularDrag = flightAngularDrag; // 角阻力在两个阶段都可以用
    }

    void Update()
    {
        if (!canDrag) return;
        if (!isFlying)
        {
            HandleDrag();
        }
    }

    void FixedUpdate()
    {
        if (isFlying && canFly) // 只有在飞行中且允许飞行时才处理飞行逻辑
        {
            HandleFlight();
        }
        if (rb.velocity.magnitude > maxFlightSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxFlightSpeed;
        }
    }

    // 拖拽阶段逻辑
    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            // 当开始拖拽时，确保飞机不会因重力下落
            // rb.isKinematic 已经为 true，所以这里不需要额外操作
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            LaunchPlane();
        }

        if (isDragging)
        {
            Ray ray = planeCamera.ScreenPointToRay(Input.mousePosition);

            // 获取桌面高度，确保飞机在拖拽时Y轴不变
            float dragPlaneY = initialPlanePosition.y; 
            Plane groundPlane = new Plane(Vector3.up, new Vector3(initialPlanePosition.x, dragPlaneY, initialPlanePosition.z));

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);

                // 限制在圆形范围内，并保持Y轴不变
                Vector3 offset = hitPoint - initialPlanePosition; // 从初始位置计算偏移
                offset.y = 0; // 强制Y轴偏移为0

                offset = Vector3.ClampMagnitude(offset, dragRadius);

                // 设置飞机位置，固定Y轴为初始Y轴
                plane.position = initialPlanePosition + offset;
            }
        }
        else
        {
            // 松开时飞机自动回到初始位置（发射前视觉效果），保持Y轴不变
            Vector3 targetPosWithFixedY = new Vector3(initialPlanePosition.x, initialPlanePosition.y, initialPlanePosition.z);
            plane.position = Vector3.Lerp(plane.position, targetPosWithFixedY, Time.deltaTime * 10f);
            //Debug.Log(plane.position);
        }
    }

    // 发射逻辑
    void LaunchPlane()
    {
        rb.isKinematic = false; // 启用物理模拟
        rb.drag = flightDrag; // 切换到飞行阻力
        rb.angularDrag = flightAngularDrag; // 启用角阻力

        // 发射方向 = 从当前拖拽点指向初始位置
        Vector3 dir = (initialPlanePosition - plane.position).normalized;

        // 避免发射时速度为零或方向不明确
        if (dir == Vector3.zero) dir = plane.forward;

        Vector3 velocity = dir * launchPower;
        velocity = Vector3.ClampMagnitude(velocity, maxLaunchSpeed);

        rb.velocity = velocity;

        isFlying = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void HandleFlight()
    {
        if (rb.velocity.magnitude > maxFlightSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxFlightSpeed;
        }
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        
        float moveHorizontalInput = Input.GetAxis("Horizontal"); 
        float moveVerticalInput = Input.GetAxis("Vertical");    
        
        Vector3 keyboardMove = plane.transform.up* moveVerticalInput + -plane.transform.right * -moveHorizontalInput;
        
        Vector3 currentHorizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (currentHorizontalVelocity.magnitude < maxFlightSpeed) 
        {
            rb.AddForce(keyboardMove * moveForce, ForceMode.Force); 
        }
        
        
        float mouseX = Input.GetAxis("Mouse X"); 
        float mouseY = Input.GetAxis("Mouse Y");
        
        plane.transform.Rotate((Vector3.forward+Vector3.left)/2 * (mouseX * rotateSpeed * Time.deltaTime), Space.World);
        plane.transform.Rotate((Vector3.forward+Vector3.right)/2 * (mouseY * rotateSpeed * Time.deltaTime), Space.World);
        
        Vector3 targetCameraPosition = plane.TransformPoint(initialCameraLocalOffset);
        Quaternion targetCameraRotation = plane.rotation * initialCameraLocalRotation;

        planeCamera.transform.position = Vector3.Lerp(planeCamera.transform.position, targetCameraPosition, Time.deltaTime * 5f);
        //planeCamera.transform.position= plane.position - Pos;
        planeCamera.transform.rotation = Quaternion.Slerp(planeCamera.transform.rotation, targetCameraRotation, Time.deltaTime * 20f);
        
        if (rb.velocity.magnitude > maxFlightSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxFlightSpeed;
        }
        
    }
}
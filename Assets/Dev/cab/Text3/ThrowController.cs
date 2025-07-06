using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    public Camera playerCamera;
    public float throwForce;//丢出力度
    public Transform target;
    public LayerMask mask;
    private bool _isDragging;
    private Rigidbody rb;
    private float dragDistance;
    
    private Vector3 lastMousePosition;

    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            EndDrag();
        }

        if (_isDragging)
        {
            DragUpdate();
        }
            
    }
    
    private void TryStartDrag()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == target)
        {
            
            dragDistance = hit.distance;
            _isDragging = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.angularVelocity = Vector3.zero;
            lastMousePosition = Input.mousePosition;
        }
    }
    private void DragUpdate()
    {
        
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, dragDistance, mask))
        {
            Debug.Log(hit.transform.name);
            target.position=hit.point - ray.direction * 0.1f; // 留一点间距
        }
        else
        {
            //Debug.Log("No drag");
            target.position=ray.origin + ray.direction * dragDistance; 
        }
        //target.position=ray.origin + ray.direction * dragDistance; 
        //lastMousePosition = Input.mousePosition;
    }   
    private void EndDrag()
    {
        
        _isDragging = false;
        rb.isKinematic = false;
        rb.useGravity = true;

       /* float depth = dragDistance;
        
        Vector3 screenStart = new Vector3(lastMousePosition.x, lastMousePosition.y, depth);
        Vector3 screenEnd = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        
        Vector3 worldStart = playerCamera.ScreenToWorldPoint(screenStart);
        Vector3 worldEnd = playerCamera.ScreenToWorldPoint(screenEnd);
        
        Vector3 throwDir = (worldEnd - worldStart).normalized;
        throwDir = Vector3.ProjectOnPlane(throwDir, Vector3.up); // 只保留水平分量,防止抛到脑后
        
        float dynamicForce = throwForce * (worldEnd - worldStart).magnitude;//根据鼠标移动距离改变力度
       // rb.AddForce(throwDir * dynamicForce, ForceMode.VelocityChange);//加力*/
        
    }

}
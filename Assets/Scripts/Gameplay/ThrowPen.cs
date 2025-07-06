using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPen : MonoBehaviour
{
    public Camera playerCamera;
    public Transform target;
    public LayerMask mask;
    private bool _isDragging;
    private Rigidbody rb;
    private float dragDistance;
    
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
        }
    }
    private void DragUpdate()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, dragDistance, mask))
        {
            //Debug.Log(hit.transform.name);
            target.position=hit.point - ray.direction * 0.1f; // 留一点间距
        }
        else
        {
            //Debug.Log("No drag");
            target.position=ray.origin + ray.direction * dragDistance; 
        }
    }   
    private void EndDrag()
    {
        
        _isDragging = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}

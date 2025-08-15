using UnityEngine;

public class ThrowPen : MonoBehaviour
{
    public Camera playerCamera;
    public Transform target;
    public LayerMask mask;
    public LayerMask maskonlypepn;
    private bool _isDragging;
    private float dragDistance;
    private Rigidbody rb;

    private void Start()
    {
        rb = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartDrag();
        else if (Input.GetMouseButtonUp(0) && _isDragging) EndDrag();

        if (_isDragging) DragUpdate();
    }

    private void TryStartDrag()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100f, maskonlypepn);
        //Debug.Log(hit.collider.gameObject.layer);
        if (Physics.Raycast(ray, out hit, 100f, maskonlypepn) && hit.transform == target)
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
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, dragDistance, mask))
            //Debug.Log(hit.transform.name);
            target.position = hit.point - ray.direction * 0.1f; // 留一点间距
        else
            //Debug.Log("No drag");
            target.position = ray.origin + ray.direction * dragDistance;
    }

    private void EndDrag()
    {
        _isDragging = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
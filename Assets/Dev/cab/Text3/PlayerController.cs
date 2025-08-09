using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public CharacterController controller;
    public float MousemouseSensitivity = 100f;
    public Transform Player;
    public Transform Camera;

    public float Movespeed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    private bool _isGrounded;
    private Vector3 _velocity;
    private float xRotation;

    private void Start()
    {
        _velocity.y = -2f;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log("角色位置 Y = " + transform.position.y);

        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out var hit, 10f, groundMask))
        {
            //Debug.Log("探测到地面 Y = " + hit.point.y);
        }
    }


    private void Update()
    {
        Move();
        CameraControl();
    }

    private void OnEnable()
    {
        _velocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(_isGrounded);
        if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;
        //Debug.Log(_isGrounded);
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;
        move.y = -2f;
        controller.Move(move * Movespeed * Time.deltaTime);

        if (_isGrounded && Input.GetButtonDown("Jump")) _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //Debug.Log("Jump velocity = " + Mathf.Sqrt(jumpHeight * -2f * gravity));
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void CameraControl()
    {
        var MouseX = Input.GetAxis("Mouse X") * MousemouseSensitivity * Time.deltaTime;
        var MouseY = Input.GetAxis("Mouse Y") * MousemouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Player.Rotate(Vector3.up * MouseX);
    }
}
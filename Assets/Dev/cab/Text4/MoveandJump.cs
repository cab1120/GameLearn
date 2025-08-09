using UnityEngine;

public class MoveandJump : MonoBehaviour
{
    public CharacterController controller;
    public float MouseSensitivity = 100f;
    public Transform Player;
    public Transform Camera;
    public Animator animator;

    public float gravity = -9.81f;
    public float Movespeed = 3f;

    // 跳跃系统
    public float jumpHeight = 1.5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Vector3 _velocity;
    private bool isGrounded;

    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // 地面检测
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && _velocity.y < 0) _velocity.y = -2f;

        Move();
        CameraControl();

        if (Input.GetButtonDown("Jump") && isGrounded) _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        if (move != Vector3.zero)
            animator.SetBool("CanWalk", true);
        else
            animator.SetBool("CanWalk", false);

        controller.Move(move * Movespeed * Time.deltaTime);
    }

    private void CameraControl()
    {
        var MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        var MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 70f);
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Player.Rotate(Vector3.up * MouseX);
    }
}
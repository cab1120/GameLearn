using UnityEngine;

public class MoveController : MonoBehaviour
{
    public CharacterController controller;
    public float MouseSensitivity = 100f;
    public Transform Player;
    public Transform Camera;
    public Animator animator;

    public float gravity = -9.81f;
    public float Movespeed = 3f;
    private Vector3 _velocity;
    private float xRotation;

    private void Start()
    {
        _velocity.y = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        Move();
        CameraControl();
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = new Vector3();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            animator.SetBool("CanWalk", true);
            move = transform.right * x + transform.forward * z;
            controller.Move(move * Movespeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("CanWalk", false);
        }

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
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
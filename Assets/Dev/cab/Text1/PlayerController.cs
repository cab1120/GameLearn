using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float MousemouseSensitivity = 100f;
    public Transform Player;
    public Transform Camera;
    private float xRotation = 0f;

    public float Movespeed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight;
    
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    private bool _isGrounded;
    private Vector3 _velocity;
    
    void OnEnable()
    {
        //Debug.Log("OnEnable");
        _velocity = Vector3.zero;
    }
    void Start()
    {
        _velocity.y = -2f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        Move();
        CameraControl();
    }

    void Move()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3();
        move=Vector3.zero;
        move = transform.right * x + transform.forward * z;
        controller.Move(move * Movespeed * Time.deltaTime);
        
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    void CameraControl()
    {
        float MouseX = Input.GetAxis("Mouse X")*MousemouseSensitivity*Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y")*MousemouseSensitivity*Time.deltaTime;
        
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        Player.Rotate(Vector3.up*MouseX);
    }
    
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}

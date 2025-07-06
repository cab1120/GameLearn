using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public CharacterController controller;
    public float MousemouseSensitivity = 100f;
    public Transform Player;
    public Transform Camera;
    private float xRotation = 0f;
    public Animator animator;
    
    public float gravity = -9.81f;
    public float Movespeed = 3f;
    private Vector3 _velocity;
    
    void Start()
    {
        _velocity.y = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        Move();
        CameraControl();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3();
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

    void CameraControl()
    {
        float MouseX = Input.GetAxis("Mouse X")*MousemouseSensitivity*Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y")*MousemouseSensitivity*Time.deltaTime;
        
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 45f);
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        Player.Rotate(Vector3.up*MouseX);
    }
}

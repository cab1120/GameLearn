using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    public PenState penState;

    private Rigidbody rb;
    private bool isControlled = false;    // 标记是否正在控制笔

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EventManager.OnEnablePenInput += OneableMovement;
    }

    private void OnDisable()
    {
        EventManager.OnEnablePenInput -= OneableMovement;
    }

    private void OneableMovement()
    {
        rb.useGravity = false;
        isControlled = true;
    }

    private void OndisableMovement()
    {
        rb.useGravity = true;
        isControlled = false;
    }
    void FixedUpdate() 
    {
        if (isControlled)
        {
            HandlePenMovement();
            ApplyControlledDownwardMotion(); 
        }
    }

    void HandlePenMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D 键或左右箭头
        float verticalInput = Input.GetAxis("Vertical");     // W/S 键或上下箭头

        Vector3 moveDirection = -transform.forward * horizontalInput + transform.right * verticalInput;
        moveDirection.y = 0; // 确保只在水平方向移动

        // 直接设置水平速度，保持垂直速度不变（由ApplyControlledDownwardMotion控制）
        rb.velocity = new Vector3(moveDirection.x * penState.moveSpeed, rb.velocity.y, moveDirection.z * penState.moveSpeed);
    }

    void ApplyControlledDownwardMotion()
    {
        rb.velocity = new Vector3(rb.velocity.x, -penState.downwardSpeed, rb.velocity.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    float xRotation, yRotation;
    public PlayerState playerState;
    
    private bool isInputEnabled = true;
    
    private void EnableInput() => isInputEnabled = true;
    private void DisableInput() => isInputEnabled = false;
    private void OnEnable()
    {
        EventManager.OnEnablePlayerInput += EnableInput;
        EventManager.OnDisablePlayerInput += DisableInput;
    }

    private void OnDisable()
    {
        EventManager.OnEnablePlayerInput -= EnableInput;
        EventManager.OnDisablePlayerInput -= DisableInput;
    }
    void Update()
    {
        if (!isInputEnabled) return;
        //检测移动输入
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var move = transform.right * x + transform.forward * z;
        EventManager.RaiseMoveInput(move);
        
        //检测鼠标输入
        var MouseX = Input.GetAxis("Mouse X") * playerState.MouseSensitivity * Time.deltaTime;
        var MouseY = Input.GetAxis("Mouse Y") * playerState.MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 70f);
        
        EventManager.RaiseCameraInput(new Vector3(xRotation, 0f, 0f),Vector3.up * MouseX);
        
        //检测跳跃
        if(Input.GetButtonDown("Jump"))
            EventManager.RaiseJumpInput();
    }
}

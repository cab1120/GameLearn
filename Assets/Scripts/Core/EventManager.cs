using System;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class EventManager
{
    
    /// <summary>
    /// 定义事件
    /// </summary>
    //玩家移动
    public static event Action<Vector3> OnMoveInput;
    public static event Action OnJumpInput;
    public static event Action<Vector3,Vector3> OnRotateInput; 
    public static event Action OnEnablePlayerInput;
    public static event Action OnDisablePlayerInput;
    //UI控制
    public static event Action<string> OnShowDialogue; //(string 是对话内容)
    public static event Action OnHideDialogue; 
    public static event Action<string> OnShowTaskList; //(string 是任务内容)
    public static event Action OnHideTaskList;
    //初始景深
    public static event Action OnDepthofField;
    //脚本控制
    public static event Action OnEnableScripts;
    public static event Action OnDisableScripts;
    //换层
    public static event Action OnChangetoSecond;
    //时停
    public static event Action<float> OnTimeStop; 
    
    /// <summary>
    /// 发布方法
    /// </summary>
    public static void RaiseMoveInput(Vector3 direction) => OnMoveInput?.Invoke(direction);
    public static void RaiseJumpInput() => OnJumpInput?.Invoke();
    public static void RaiseCameraInput(Vector3 camerarotation,Vector3 playerrotation)=>OnRotateInput?.Invoke(camerarotation,playerrotation);
    public static void RaiseEnablePlayerInput() => OnEnablePlayerInput?.Invoke();
    public static void RaiseDisablePlayerInput() => OnDisablePlayerInput?.Invoke();
    public static void RaiseShowDialogue(string text) => OnShowDialogue?.Invoke(text);
    public static void RaiseHideDialogue() => OnHideDialogue?.Invoke();
    public static void RaiseShowTaskList(string text) => OnShowTaskList?.Invoke(text);
    public static void RaiseHideTaskList() => OnHideTaskList?.Invoke();
    public static void RaiseDepthofField() => OnDepthofField?.Invoke();
    public static void RaiseEnableScripts() => OnEnableScripts?.Invoke();
    public static void RaiseDisableScripts() => OnDisableScripts?.Invoke();
    public static void RaiseChangetoSecond() => OnChangetoSecond?.Invoke();
    public static void RaiseTimeStop(float timeStop) => OnTimeStop?.Invoke(timeStop);
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeToPenState : IGameState
{
    public GameFlowManager gameFlowManager;
    
    public ChangeToPenState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        /*EventManager.RaiseShowTaskList("操控钥匙，直到钥匙到达足够大小");
        GameState.instance.penRigidbody.useGravity = false;
        GameState.instance.penRigidbody.velocity = Vector3.zero;
        GameState.instance.paperPlane.SetActive(true);
        TransitionManager.Instance.StartCharacterSwitch(() =>
            {
                GameState.instance.mainCamera.gameObject.SetActive(false);
                GameState.instance.penCamera.gameObject.SetActive(true);
            },
            () =>
            {
                EventManager.RaiseDisablePlayerInput();
                EventManager.RaiseEnablePenInput();
            });*/
        //EventManager.RaiseShowTaskList("操控纸飞机直到钥匙足够大");
        EventManager.RaiseHideTaskList();
        GameState.instance.fakePlane.SetActive(false);
        GameState.instance.Plane.SetActive(true);
        EventManager.RaiseDisablePlayerInput();
        EventManager.RaiseOnDisableConnectedMovement();
        CatchPen.instance.changeTarget();
        ChangeCamera.instance.SwitchToPlaneCamera();
        EventManager.RaiseEnablePlaneDrag();
        EventManager.RaiseEnablePlaneMovement();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

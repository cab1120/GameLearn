using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackState : IGameState
{
    public GameFlowManager gameFlowManager;

    public ChangeBackState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        EventManager.RaiseShowTaskList("拿起足够大的钥匙，把门“打”开");
        TransitionManager.Instance.StartCharacterSwitch(() =>
            {
                GameState.instance.mainCamera.enabled = true;
                GameState.instance.penCamera.gameObject.SetActive(false);
            },
            () =>
            {
                EventManager.RaiseEnablePlayerInput();
                EventManager.RaiseOnEnableConnectedMovement();
                //EventManager.RaiseDisablePenInput();
            });
        GameState.instance.DoorCheck1.SetActive(false);
        GameState.instance.DoorCheck2.SetActive(true);
        GameState.instance.SecondPen.SetActive(true);
        GameState.instance.catchPen.enabled = false;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

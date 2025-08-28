using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetDownState : IGameState
{
    public GameFlowManager gameFlowManager;

    public GetDownState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Enter()
    {
        EventManager.RaiseShowTaskList("捡起钥匙开门");
        //EventManager.RaiseChangetoSecond();
        TransitionManager.Instance.StartCharacterSwitch(() =>
        {
            GameState.instance.player.SetActive(false);
            GameState.instance.connetted.enabled = false;
            GameState.instance.playerMovement.enabled = true;
            GameState.instance.playerInputHandler.enabled = true;
            GameState.instance.throwPen.enabled = true;
            GameState.instance.camera2.SetActive(true);
            GameState.instance.fakeplayer.layer = LayerMask.NameToLayer("Player");
            foreach (var child in GameState.instance.fakeplayer.GetComponentsInChildren<Transform>(true))
                child.gameObject.layer = LayerMask.NameToLayer("Player");
            GameState.instance.fakeplayer.tag = "Player";
        });
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytoDoorState : IGameState
{
    private GameFlowManager gameFlowManager;

    public PlaytoDoorState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        EventManager.RaiseEnablePlayerInput();
        EventManager.RaiseEnableScripts();
        EventManager.RaiseShowTaskList("尝试着开门到另外的教室盛水");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

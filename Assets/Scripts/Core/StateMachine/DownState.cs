using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownState : IGameState
{
    public GameFlowManager gameFlowManager;

    public DownState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        EventManager.RaiseEnablePlayerInput();
        EventManager.RaiseEnableScripts();
        EventManager.RaiseShowTaskList("寻找钥匙，并将其带入下一层");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

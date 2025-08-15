using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytoDrinkState : IGameState
{
    private GameFlowManager gameFlowManager;
    
    public PlaytoDrinkState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        EventManager.RaiseEnablePlayerInput();
        EventManager.RaiseEnableScripts();
        EventManager.RaiseShowTaskList("寻找房间异常之处");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

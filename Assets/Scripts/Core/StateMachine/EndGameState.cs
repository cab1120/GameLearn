using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : IGameState
{
    private GameFlowManager gameFlowManager;

    public EndGameState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }
    public void Enter()
    {
        EventManager.RaiseHideTaskList();
        GameState.instance.Endpic.SetActive(true);
        EventManager.RaiseTimeStop(3f);
        
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
    
}

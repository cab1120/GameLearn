using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        EventManager.RaiseTimeStop(0.5f);
        EventManager.RaiseChangeScenceEvent(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
    
}

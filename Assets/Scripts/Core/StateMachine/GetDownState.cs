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
        EventManager.RaiseChangetoSecond();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
    
}

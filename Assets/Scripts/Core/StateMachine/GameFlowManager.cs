using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private IGameState currentState;

    void Start()
    {
        // 游戏开始时，我们进入“开场白状态”
        ChangeState(new IntroState(this)); 
    }

    void Update()
    {
        // 每帧执行当前状态的逻辑
        currentState?.Execute();
    }

    public void ChangeState(IGameState newState)
    {
        // 1. 调用旧状态的退出逻辑
        currentState?.Exit();

        // 2. 切换到新状态
        currentState = newState;

        // 3. 调用新状态的进入逻辑
        currentState.Enter();
    }
}

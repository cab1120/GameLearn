using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheckState : IGameState
{
    public GameFlowManager gameFlowManager;
    
    private int dialogueIndex = 0;
    private string[] dialogues = { "没道理啊，为什么这个门没有锁孔", 
        "难道说这里实际上不是真实世界吗", 
        "那看来不能按照常理来开门了，得看看这个世界究竟有什么不同" };

    public DoorCheckState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }

    public void Enter()
    {
        EventManager.RaiseDisableScripts();
        EventManager.RaiseDisablePlayerInput();
        EventManager.RaiseShowDialogue(dialogues[dialogueIndex]);
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0))
        {
            dialogueIndex++;
            if (dialogueIndex < dialogues.Length)
            {
                EventManager.RaiseShowDialogue(dialogues[dialogueIndex]);
            }
            else
            {
                gameFlowManager.ChangeState(new PlaytoDrinkState(gameFlowManager));
            }
        }
    }

    public void Exit()
    {
        EventManager.RaiseHideDialogue();
        GameState.instance.Drink.SetActive(true);
        GameState.instance.waterCheck.SetActive(true);
    }
}

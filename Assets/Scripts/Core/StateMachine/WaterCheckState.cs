using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterCheckState : IGameState
{
    public GameFlowManager gameFlowManager;
    
    private int dialogueIndex = 0;
    private string[] dialogues = { "这里怎么有一个饮水机？为什么我一靠近又消失了？", 
        "饮水机下面怎么还有一个洞？让我看看……", 
        "……………………",
        "下面看起来是一个和我这一样的房间，而且我还能看到下面房间中的'我'的头顶",
        "那么，如果上面门没锁孔的话那说不定下面的门能够打开，不过可能得找找钥匙再下去",
        "看看这个房间还有没有别的异常吧，说不定就能找到'钥匙'"
    };

    public WaterCheckState(GameFlowManager gameFlowManager)
    {
        this.gameFlowManager = gameFlowManager;
    }

    public void Enter()
    {
        EventManager.RaiseDisableScripts();
        EventManager.RaiseDisablePlayerInput();
        EventManager.RaiseShowDialogue(dialogues[dialogueIndex]);
        
        GameState.instance.fakeFloor.GetComponent<MeshRenderer>().enabled = false;
        GameState.instance.Drink.SetActive(false);
        GameObject.FindGameObjectWithTag("Pen").layer = LayerMask.NameToLayer("Dropped");
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
                gameFlowManager.ChangeState(new DownState(gameFlowManager));
            }
        }
    }

    public void Exit()
    {
        EventManager.RaiseHideDialogue();
    }
}

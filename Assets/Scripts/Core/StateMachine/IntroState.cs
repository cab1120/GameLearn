using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroState : IGameState
{
    private GameFlowManager manager;
    private int dialogueIndex = 0;
    private string[] dialogues = { "好熟悉的场景，这里是……红客实验室吗？什么设备都有，唯独没有饮水机，那看来是红客实验室没跑了", 
        "还好我随身带着一瓶…………布豪，水瓶什么时候漏了，那我只能按老样子穿过两扇门去隔壁会议室盛水了。", 
        "唉，运气真是背啊，希望能别再出什么幺蛾子了" };
    public IntroState(GameFlowManager manager)
    {
        this.manager = manager;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Enter()
    {
        EventManager.RaiseDepthofField();
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
                manager.ChangeState(new PlaytoDoorState(manager));
            }
        }
    }

    public void Exit()
    {
        EventManager.RaiseHideDialogue();
    }
}

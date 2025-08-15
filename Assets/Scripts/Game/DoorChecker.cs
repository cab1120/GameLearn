using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    public Doorhitcontroller hintUI;

    private int times = 1;
    private GameFlowManager gameFlowManager;
    private void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && times == 1)
        {
            times++;
            gameFlowManager.ChangeState(new DoorCheckState(gameFlowManager));
        }

        if (other.gameObject.CompareTag("Pen"))
        {
            Debug.Log("showpen");
            hintUI.ShowHint("这个门没有锁孔，不能从这一侧打开");
        }
    }
}

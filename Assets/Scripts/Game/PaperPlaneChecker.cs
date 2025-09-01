using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlaneChecker : MonoBehaviour
{
    public PlayerState playerState;
    private int num = 1;
    private GameFlowManager gameFlowManager;
    private void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerState.havePen ==true&&num==1)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //Debug.Log("11111");
                num++;
                
                gameFlowManager.ChangeState(new ChangeToPenState(gameFlowManager));
            }            
        }
    }
}

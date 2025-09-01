using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorChecker : MonoBehaviour
{
    private GameFlowManager gameFlowManager;
    public PlayerState playerState;
    private void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerState.haveKey)
            {
                gameFlowManager.ChangeState(new EndGameState(gameFlowManager));
            }
        }
    }
}

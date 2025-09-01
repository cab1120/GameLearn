using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenChecker : MonoBehaviour
{
    private GameFlowManager gameFlowManager;
    private void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pen"))
        {
            //Debug.Log("Pen");
            gameFlowManager.ChangeState(new ChangeToPenState(gameFlowManager));
        }
    }
}
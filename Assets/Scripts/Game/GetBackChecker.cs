using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBackChecker : MonoBehaviour
{
    public int times=1;
    private GameFlowManager gameFlowManager;
    private void Start()
    {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pen")&&times==1)
        {
            times=0;
            gameFlowManager.ChangeState(new ChangeBackState(gameFlowManager));
        }
    }
}

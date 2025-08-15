using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChecker : MonoBehaviour
{
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
            gameFlowManager.ChangeState(new WaterCheckState(gameFlowManager));
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
            if (other.gameObject.CompareTag("Player"))
            {
                gameFlowManager.ChangeState(new GetDownState(gameFlowManager));
            }
    }
}

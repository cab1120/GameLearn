using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenPickChecker : MonoBehaviour
{
    public PlayerState playerState;

    void Awake()
    {
        playerState.havePen = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerState.havePen ==false)
        {
            //Debug.Log("Player entered");
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("pen pick check");
                playerState.havePen = true;
                EventManager.RaiseShowTopicList("已拾取钥匙");
                GameState.instance.Pen.SetActive(false);
            }            
        }
    }
}

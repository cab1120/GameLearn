using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChecker : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject pen;
    void Awake()
    {
        playerState.haveKey = false;
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player") && playerState.haveKey ==false)
        {
            Debug.Log(other.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                pen.SetActive(false);
                EventManager.RaiseShowTopicList(" 已拾取\"钥匙\" ");
                playerState.haveKey = true;
            }            
        }
    }
}

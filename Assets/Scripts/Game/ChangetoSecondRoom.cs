using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangetoSecondRoom : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnChangetoSecond += Change;
    }

    private void OnDisable()
    {
        EventManager.OnChangetoSecond -= Change;
    }

    private void Change()
    {
        Debug.Log("Changeto Second Room");
        GameState.instance.player.SetActive(false);
        GameState.instance.connetted.enabled = false;
        GameState.instance.playerMovement.enabled = true;
        GameState.instance.playerInputHandler.enabled = true;
        GameState.instance.throwPen.enabled = true;
        GameState.instance.camera2.SetActive(true);
        GameState.instance.fakeplayer.layer = LayerMask.NameToLayer("Player");
        foreach (var child in GameState.instance.fakeplayer.GetComponentsInChildren<Transform>(true))
            child.gameObject.layer = LayerMask.NameToLayer("Player");
        GameState.instance.fakeplayer.tag = "Player";
    }
}

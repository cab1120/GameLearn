using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsController : MonoBehaviour
{
    public CatchPen catchPen;
    public ThrowPen throwPen;

    private void OnEnable()
    {
        EventManager.OnEnableScripts += Open;
        EventManager.OnDisableScripts += Close;
    }

    private void OnDisable()
    {
        EventManager.OnEnableScripts -= Open;
        EventManager.OnDisableScripts -= Close;
    }
    private void Open()
    {
        catchPen.enabled = true;
        throwPen.enabled = true;
    }

    private void Close()
    {
        catchPen.enabled = false;
        throwPen.enabled = false;
    }
}

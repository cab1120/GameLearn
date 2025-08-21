using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScence : MonoBehaviour
{
    public AsyncLoader asyncLoader;
    private void OnEnable()
    {
        EventManager.OnChangeScenceEvent += ChangingScence;
    }

    private void OnDisable()
    {
        EventManager.OnChangeScenceEvent -= ChangingScence;
    }

    private void ChangingScence(int scenenum)
    {
        asyncLoader.StartLoading(scenenum);
    }
}

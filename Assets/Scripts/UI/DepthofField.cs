using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthofField : MonoBehaviour
{
    public Volume volume;
    private DepthOfField dof;

    private void OnEnable()
    {
        EventManager.OnDepthofField += StartDepthofField;
    }

    private void OnDisable()
    {
        EventManager.OnDepthofField -= StartDepthofField;
    }

    private void StartDepthofField()
    {
        if (volume.profile.TryGet(out dof))
        {
            dof.active = true;
            dof.mode.value = DepthOfFieldMode.Bokeh;
            dof.focusDistance.value = 0.1f; // 初始模糊
        }
        StartCoroutine(ClearFocusRoutine());
    }
    private IEnumerator ClearFocusRoutine()
    {
        float t = 0;
        var duration = 2.0f;
        var startFocus = 0.1f;
        var endFocus = 10f;
        Cursor.lockState = CursorLockMode.Confined;
        
        while (t < duration)
        {
            t += Time.deltaTime * 0.2f;
            var current = Mathf.Lerp(startFocus, endFocus, t / duration);
            dof.focusDistance.value = current;
            yield return null;
        }

        dof.focusDistance.value = endFocus;
    }
}

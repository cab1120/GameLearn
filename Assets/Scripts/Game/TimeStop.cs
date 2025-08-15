using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnTimeStop += StopTimeForSeconds;
    }

    private void OnDisable()
    {
        EventManager.OnTimeStop-=StopTimeForSeconds;
    }

    public void StopTimeForSeconds(float seconds)
    {
        StartCoroutine(StopTimeCoroutine(seconds));
    }

    private IEnumerator StopTimeCoroutine(float seconds)
    {
        Time.timeScale = 0f;  // 停止时间
        yield return new WaitForSecondsRealtime(seconds); // 不受 timeScale 影响
        Time.timeScale = 1f;  // 恢复时间
        GameState.instance.Endpic.SetActive(false);
    }
}

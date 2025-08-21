using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndGameChecker: MonoBehaviour
{
    public Image whiteScreen; 
    public TextMeshProUGUI text;
    public float fadeDuration = 5f; 

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FadeAndPause());
        }
    }

    private IEnumerator FadeAndPause()
    {
        
        float timer = 0f;
        Color c = whiteScreen.color;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // 用 unscaledDeltaTime，避免受 timeScale 影响
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            whiteScreen.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.transform.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Doorhitcontroller : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public CanvasGroup canvasGroup;
    public float showDuration = 3f;
    private Coroutine currentCoroutine;

    public void ShowHint(string message) {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ShowAndHide(message));
    }

    private IEnumerator ShowAndHide(string msg) {
        hintText.text = msg;

        // 淡入
        float t = 0f;
        float fadeTime = 0.2f;
        while (t < fadeTime) {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(showDuration);

        // 淡出
        t = 0f;
        while (t < fadeTime) {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}

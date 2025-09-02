using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public GameObject control;
    public GameObject background;
    public GameObject SliderScene;
    public GameObject Stages;
    public Slider progressBar; 
    public float fakeDuration = 2f; 
    public Text progressText;
    
    public CanvasGroup waitPanel;   
    public float fadeDuration = 0.5f; 
    public float displayTime = 2f; 

    public void ChangeControl()
    {
        EventManager.RaiseChangeButtonEvent(control);
    }

    public void ChangeBackground()
    {
        EventManager.RaiseChangeButtonEvent(background);
    }
    public void ChangeStages()
    {
        EventManager.RaiseChangeButtonEvent(Stages);
    }

    public void ChangeScene()
    {
        SliderScene.SetActive(true);
        StartCoroutine(LoadSceneAsyncWithFakeProgress(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void OnButtonClick()
    {
        // 清除当前选中的 UI，避免按钮卡在高亮
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private IEnumerator LoadSceneAsyncWithFakeProgress(int sceneIndex)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOp.allowSceneActivation = false; // 暂停激活

        float timer = 0f;

        while (!asyncOp.isDone)
        {
            timer += Time.deltaTime;
            
            // 伪进度条：0-1线性过渡到1
            float progress = Mathf.Clamp01(timer / fakeDuration);
            if (progressBar != null)
                progressBar.value = progress;
            if (progressText != null) 
                progressText.text = (progress * 100f).ToString("F0") + "%";

            // 当伪进度条完成后允许场景激活
            if (progress >= 1f)
                asyncOp.allowSceneActivation = true;

            yield return null;
        }
    }
    public void OnWaitButtonClick()
    {
        StopAllCoroutines();
        StartCoroutine(ShowAndHidePanel());
    }

    private IEnumerator ShowAndHidePanel()
    {
        waitPanel.gameObject.SetActive(true);
        waitPanel.interactable = false;
        waitPanel.blocksRaycasts = false;

        yield return StartCoroutine(FadeCanvasGroup(waitPanel, 0f, 1f, fadeDuration));

        yield return new WaitForSeconds(displayTime);

        yield return StartCoroutine(FadeCanvasGroup(waitPanel, 1f, 0f, fadeDuration));

        waitPanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}

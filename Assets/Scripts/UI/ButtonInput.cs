using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public GameObject control;
    public GameObject background;
    public GameObject SliderScene;
    public Slider progressBar; 
    public float fakeDuration = 2f; 
    public Text progressText;

    public void ChangeControl()
    {
        EventManager.RaiseChangeButtonEvent(control);
    }

    public void ChangeBackground()
    {
        EventManager.RaiseChangeButtonEvent(background);
    }

    public void ChangeScene()
    {
        SliderScene.SetActive(true);
        StartCoroutine(LoadSceneAsyncWithFakeProgress(SceneManager.GetActiveScene().buildIndex + 1));
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    public Slider loadingBar;        
    public Text progressText;     
    private float fakeDuration = 5f;
    
    public void StartLoading(int sceneNum)
    {
        gameObject.SetActive(true); 
        StartCoroutine(LoadSceneAsync(sceneNum));
    }

    private IEnumerator LoadSceneAsync(int sceneNum)
    {
        float timer = 0f;
        while (timer < fakeDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fakeDuration);

            if (loadingBar != null)
                loadingBar.value = progress;
            
            if (progressText != null)
                progressText.text = (progress * 100f).ToString("F0") + "%";

            
            if (progress >= 1f)
            {
                SceneManager.LoadScene(sceneNum);
            }

            yield return null;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering; // URP的Volume系统需要这个命名空间
using UnityEngine.Rendering.Universal; // URP的Volume系统需要这个命名空间

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [Header("UI & Volume")]
    [Tooltip("用于屏幕淡入淡出的黑色UI Image")]
    [SerializeField] private Image fadeImage;
    [Tooltip("场景中的全局Volume (Global Volume)")]
    [SerializeField] private Volume globalVolume; // 之前的 PostProcessVolume 替换为 URP 的 Volume

    [Header("Settings")]
    [SerializeField] private float transitionDuration = 1.0f;

    // URP中的后期效果类
    private LensDistortion lensDistortion;
    private Vignette vignette;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 从Volume Profile中尝试获取后期效果的引用
        if (globalVolume != null)
        {
            globalVolume.profile.TryGet<LensDistortion>(out lensDistortion);
            globalVolume.profile.TryGet<Vignette>(out vignette);
        }
        else
        {
            Debug.LogWarning("TransitionManager: Global Volume 未指定!", this);
        }

        ResetEffects();
    }

    // 重置所有效果到初始状态
    private void ResetEffects()
    {
        if(fadeImage != null) fadeImage.color = new Color(39, 39, 39, 0);
        
        // 修改URP的参数需要访问其 .value 属性
        if(lensDistortion != null) lensDistortion.intensity.value = 0f;
        if(vignette != null) vignette.intensity.value = 0f;
    }

    /// <summary>
    /// 公开的接口：开始一个带过渡效果的切换
    /// </summary>
    /// <param name="onSwitchAction">在屏幕全黑时执行的实际切换操作</param>
    public void StartCharacterSwitch(Action onSwitchAction)
    {
        // 确保效果是从默认状态开始的
        ResetEffects(); 
        StartCoroutine(TransitionCoroutine(onSwitchAction));
    }

    private IEnumerator TransitionCoroutine(Action onSwitchAction)
    {
        float halfDuration = transitionDuration / 2f;
        float elapsedTime = 0f;

        //淡出
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / halfDuration);

            if(fadeImage != null) fadeImage.color = new Color(0, 0, 0, progress);
            
            // 控制URP后期效果
            if(lensDistortion != null) lensDistortion.intensity.value = -1.0f * progress; // URP中LensDistortion强度范围通常是-1到1
            if(vignette != null) vignette.intensity.value = progress; // Vignette强度范围是0到1

            yield return null;
        }

        //执行核心切换逻辑
        onSwitchAction?.Invoke();

        //淡入
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(1 - (elapsedTime / halfDuration));

            if(fadeImage != null) fadeImage.color = new Color(0, 0, 0, progress);

            // 控制URP后期效果
            if(lensDistortion != null) lensDistortion.intensity.value = -1.0f * progress;
            if(vignette != null) vignette.intensity.value = progress;

            yield return null;
        }

        ResetEffects(); // 确保最终效果被完全重置
    }
}
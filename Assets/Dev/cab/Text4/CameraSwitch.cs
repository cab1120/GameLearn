using System;
using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public static CameraSwitch Instance { get; private set; }

    [Header("场景中的摄像机")]
    [Tooltip("透视摄像机")]
    public Camera perspectiveCamera;

    [Tooltip("正交摄像机")]
    public Camera orthographicCamera;

    [Header("动画参数")]
    [Tooltip("过渡动画的持续时间（秒）")]
    public float transitionDuration = 1.5f;

    [Tooltip("用于控制动画速度的缓动曲线")]
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isTransitioning = false;

    private void Awake()
    {
        // 设置单例实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        perspectiveCamera.enabled = true;
        orthographicCamera.enabled = false;
    }
    
    //从透视切换到正交
    public void PerToOrt()
    {
        if (!isTransitioning && perspectiveCamera.enabled)
        {
            StartCoroutine(AnimateTransition(perspectiveCamera, orthographicCamera));
        }
    }
    //从正交切换回透视
    public void OrtToPer()
    {
        if (!isTransitioning && orthographicCamera.enabled)
        {
            StartCoroutine(AnimateTransition(orthographicCamera, perspectiveCamera));
        }
    }

    private IEnumerator AnimateTransition(Camera source, Camera target)
    {
        isTransitioning = true;

        Vector3 originalTargetPosition = target.transform.position;
        Quaternion originalTargetRotation = target.transform.rotation;
        Matrix4x4 originalTargetProjection = target.projectionMatrix;

        target.transform.position = source.transform.position;
        target.transform.rotation = source.transform.rotation;
        target.projectionMatrix = source.projectionMatrix;

        target.enabled = true;
        source.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            float easedT = transitionCurve.Evaluate(t);

            target.transform.position = Vector3.Lerp(source.transform.position, originalTargetPosition, easedT);
            target.transform.rotation = Quaternion.Slerp(source.transform.rotation, originalTargetRotation, easedT);
            target.projectionMatrix = MatrixLerp(source.projectionMatrix, originalTargetProjection, easedT);

            yield return null;
        }

        target.transform.position = originalTargetPosition;
        target.transform.rotation = originalTargetRotation;
        target.ResetProjectionMatrix();

        isTransitioning = false;
    }

    private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
    {
        Matrix4x4 result = new Matrix4x4();
        for (int i = 0; i < 16; i++)
        {
            result[i] = Mathf.Lerp(from[i], to[i], t);
        }
        return result;
    }
}
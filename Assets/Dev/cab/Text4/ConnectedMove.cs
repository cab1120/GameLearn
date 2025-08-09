using System.Collections;
using UnityEngine;

public class ConnectedMove : MonoBehaviour
{
    [Tooltip("回弹动画的持续时间（秒）")]
    public float returnDuration = 0.4f;

    [Tooltip("自定义回弹动画的缓动曲线。\n横轴是时间(0->1)，纵轴是进度(0->1)")]
    public AnimationCurve returnCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // 提供一个默认曲线

    public Camera camera;
    private Vector3 dragOffset;
    private bool isDragging;
    
    private Vector3 originalPosition;
    private Coroutine returnCoroutine;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = camera.WorldToScreenPoint(transform.position).z;
        var worldMouse = camera.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            if (returnCoroutine != null)
            {
                StopCoroutine(returnCoroutine);
                returnCoroutine = null;
            }

            isDragging = true;
            dragOffset = transform.position - worldMouse;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                returnCoroutine = StartCoroutine(ReturnToOriginalPosition());
            }
        }

        if (isDragging)
        {
            var targetPos = worldMouse + dragOffset;
            var oldPos = transform.position;
            
            transform.position = new Vector3(targetPos.x, targetPos.y, originalPosition.z);
            
            var changePos = transform.position - oldPos;
            BroadcastBoxPosition(changePos);
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 lastPosition = startPosition;
        
        // 冲击力仍然根据总路程和总时间计算，以提供一个符合直觉的力度
        Vector3 impactVelocity = (originalPosition - startPosition) / returnDuration;

        while (elapsedTime < returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / returnDuration;

            // --- 核心修改：从AnimationCurve中获取缓动值 ---
            float easedT = returnCurve.Evaluate(t);

            transform.position = Vector3.LerpUnclamped(startPosition, originalPosition, easedT);
            
            BroadcastBoxPosition(transform.position - lastPosition);
            lastPosition = transform.position;

            yield return null;
        }

        // --- 动画结束，执行收尾 ---
        BroadcastBoxPosition(originalPosition - lastPosition);
        transform.position = originalPosition;

        // 广播冲击力。为了让效果最自然，建议您设计的曲线在结束时是加速的(斜率变大)
        if (impactVelocity.sqrMagnitude > 0)
        {
            BroadcastBoxVelocity(impactVelocity);
        }
        
        returnCoroutine = null;
    }

    private void BroadcastBoxPosition(Vector3 positionChange)
    {
        if (positionChange.sqrMagnitude == 0) return;
        var floatingObjects = GameObject.FindGameObjectsWithTag("FloatObj");
        foreach (var obj in floatingObjects)
        {
            var fo = obj.GetComponent<Floatingcontroller>();
            if (fo != null) fo.AddBoxPosition(positionChange);
        }
    }
    
    private void BroadcastBoxVelocity(Vector3 velocity)
    {
        var floatingObjects = GameObject.FindGameObjectsWithTag("FloatObj");
        foreach (var obj in floatingObjects)
        {
            var fo = obj.GetComponent<Floatingcontroller>();
            if (fo != null) fo.AddBoxVelocity(velocity);
        }
    }
}
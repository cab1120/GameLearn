using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentScale : MonoBehaviour
{
    private Vector3 initialLocalScale;

    void Start()
    {
        // 记录子物体原本的缩放
        initialLocalScale = new Vector3(transform.localScale.x/transform.parent.lossyScale.x, transform.localScale.y/transform.parent.lossyScale.y, transform.localScale.z/transform.parent.lossyScale.z);
    }

    void LateUpdate()
    {
        // 计算缩放抵消
        Vector3 parentScale = transform.parent != null ? transform.parent.lossyScale : Vector3.one;
        transform.localScale = new Vector3(
            initialLocalScale.x / parentScale.x,
            initialLocalScale.y / parentScale.y,
            initialLocalScale.z / parentScale.z
        );
    }
}

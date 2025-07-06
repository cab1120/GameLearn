using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera camera;
    void LateUpdate()
    {
        if (camera != null)
        {
            transform.LookAt(camera.transform);
            transform.Rotate(0, 180f, 0); // 背对摄像机转 180°
        }
    }
}

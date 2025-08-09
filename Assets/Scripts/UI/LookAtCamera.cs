using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera camerainLook;

    private void LateUpdate()
    {
        if (camerainLook != null)
        {
            transform.LookAt(camerainLook.transform);
            
            transform.Rotate(0, 180f, 0); // 背对摄像机转 180°
        }
    }
}
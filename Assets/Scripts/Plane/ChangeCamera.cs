using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour
{
    public static ChangeCamera instance;

    private void Awake()
    {
        instance = this;
    }

    public Camera mainCamera;
    public Camera planeCamera;
    public float transitionTime = 1.5f;
    private Vector3 startPos;
    private Quaternion startRot;

    private bool isSwitching = false;

    public void SwitchToPlaneCamera()
    {
        startPos = mainCamera.transform.position;
        startRot = mainCamera.transform.rotation;
        if (!isSwitching && mainCamera.enabled) 
            StartCoroutine(SmoothSwitch());
    }

    IEnumerator SmoothSwitch()
    {
        isSwitching = true;
        
        Vector3 targetPos = planeCamera.transform.position;
        Quaternion targetRot = planeCamera.transform.rotation;
        
        mainCamera.enabled = true;
        planeCamera.enabled = false; 

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null; 
        }
    
        mainCamera.enabled = false;
        mainCamera.transform.position = startPos;
        mainCamera.transform.rotation = startRot;
        planeCamera.enabled = true;
        
        isSwitching = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SwitchToMainCamera()
    {
        if (!isSwitching && planeCamera.enabled)
        {

            planeCamera.enabled = false;
            mainCamera.enabled = true;
            mainCamera.transform.position = startPos;
            mainCamera.transform.rotation = startRot;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
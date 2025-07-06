using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartDepthofField : MonoBehaviour
{
    public Volume volume;
    public MoveController moveController;
    public CatchPen catchPen;
    public ThrowPen throwPen;
    public StartDepthofField startDepthofField;
    private DepthOfField dof;
    public GameObject communicate;
    public GameObject Text1,Text2,Text3;
    private int num = 1;

    void Start()
    {
        if (volume.profile.TryGet(out dof)) {
            dof.active = true;
            dof.mode.value = DepthOfFieldMode.Bokeh;
            dof.focusDistance.value = 0.1f; // 初始模糊
        }
        StartClearFocusTransition();
        communicate.SetActive(true);
    }

    void Update()
    {
        Showtext();
    }

    public void StartClearFocusTransition() {
        StartCoroutine(ClearFocusRoutine());
    }

    IEnumerator ClearFocusRoutine() {
        float t = 0;
        float duration = 2.0f;
        float startFocus = 0.1f;
        float endFocus = 10f;
        Cursor.lockState = CursorLockMode.Confined;
        moveController.enabled = false;
        catchPen.enabled = false;
        throwPen.enabled = false;
        while (t < duration) {
            t += Time.deltaTime*0.2f;
            float current = Mathf.Lerp(startFocus, endFocus, t / duration);
            dof.focusDistance.value = current;
            yield return null;
        }

        dof.focusDistance.value = endFocus;
    }

    public void Showtext()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            num++;
        switch (num)
        {
            case 1:
                Text1.SetActive(true);
                break;
            case 2:
                Text1.SetActive(false);
                Text2.SetActive(true);
                break;
            case 3:
                Text2.SetActive(false);
                Text3.SetActive(true);
                break;
            case 4:
                communicate.SetActive(false);
                moveController.enabled = true;
                catchPen.enabled = true;
                throwPen.enabled = true;
                startDepthofField.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
}
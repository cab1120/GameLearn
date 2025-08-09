using UnityEngine;

public class ScreenLineRender : MonoBehaviour
{
    public delegate void LineDrawHandler(Vector3 start, Vector3 end, Vector3 depth);

    public Material lineMaterial; //划线的材质
    private bool dragging;
    private Vector3 end;
    private Camera mainCamera;

    private Vector3 start;

    private void Start()
    {
        mainCamera = Camera.main;
        dragging = false;
    }

    /// <summary>
    ///     鼠标检测
    /// </summary>
    private void Update()
    {
        if (!dragging && Input.GetMouseButtonDown(0))
        {
            dragging = true;
            start = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (dragging) end = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        if (dragging && Input.GetMouseButtonUp(0))
        {
            dragging = false;
            end = mainCamera.ScreenToViewportPoint(Input.mousePosition);

            var startRay = mainCamera.ViewportPointToRay(start);
            var endRay = mainCamera.ViewportPointToRay(end);

            OnLineDraw?.Invoke(startRay.GetPoint(mainCamera.nearClipPlane),
                endRay.GetPoint(mainCamera.nearClipPlane),
                endRay.direction.normalized);
        }
    }

    /// <summary>
    ///     在屏幕上划线
    /// </summary>
    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }

    public event LineDrawHandler OnLineDraw;

    private void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
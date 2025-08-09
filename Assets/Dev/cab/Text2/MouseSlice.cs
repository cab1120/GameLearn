using EzySlice;
using UnityEngine;
using Plane = UnityEngine.Plane;

public class MouseSlice : MonoBehaviour
{
    public Transform ObjectContainer;

    public ScreenLineRender lineRenderer;

    public Transform planeTransform;
    private bool drawplane;
    private Plane slicePlane;

    private void OnEnable()
    {
        lineRenderer.OnLineDraw += OnlineDraw;
    }

    private void OnDisable()
    {
        lineRenderer.OnLineDraw -= OnlineDraw;
    }

    private void OnlineDraw(Vector3 start, Vector3 end, Vector3 depth)
    {
        Debug.Log("OnLineDrawn");
        var Planepos = (end - start).normalized;
        if (Planepos == Vector3.zero)
            Planepos = Vector3.right;
        var normalVec = Vector3.Cross(Planepos, depth);
        slicePlane = new Plane(normalVec, start);
        //Showplane(slicePlane);
        SliceObjects(start, normalVec);
    }

    private void Showplane(Plane plane)
    {
        var planePoint = -plane.normal * plane.distance;
        var rotation = Quaternion.FromToRotation(Vector3.up, plane.normal);
        planeTransform.position = planePoint;
        planeTransform.rotation = rotation;
    }

    private void SliceObjects(Vector3 point, Vector3 normalVec)
    {
        var toSlice = GameObject.FindGameObjectsWithTag("Sliceable");
        GameObject obj;
        for (var i = 0; i < toSlice.Length; ++i)
        {
            obj = toSlice[i];
            if (IsObjectIntersectedByPlane(obj, slicePlane))
            {
                Debug.Log("1");
                var hull = obj.Slice(point, normalVec);
                var originalMaterial = obj.GetComponent<MeshRenderer>().sharedMaterial;
                if (hull != null)
                {
                    var upperhull = hull.CreateUpperHull(obj, originalMaterial);
                    var lowerhull = hull.CreateLowerHull(obj, originalMaterial);

                    SetObj(upperhull);
                    SetObj(lowerhull);

                    upperhull.transform.position += normalVec.normalized * 0.1f;
                    lowerhull.transform.position -= normalVec.normalized * 0.1f;

                    Destroy(obj);
                }
            }
        }
    }

    private bool IsObjectIntersectedByPlane(GameObject obj, Plane plane)
    {
        // 获取世界空间 AABB
        var bounds = obj.GetComponent<Renderer>().bounds;

        // 检查包围盒的 8 个点是否在平面两侧
        var corners = new Vector3[8];
        var center = bounds.center;
        var extents = bounds.extents;

        corners[0] = center + new Vector3(-extents.x, -extents.y, -extents.z);
        corners[1] = center + new Vector3(-extents.x, -extents.y, extents.z);
        corners[2] = center + new Vector3(-extents.x, extents.y, -extents.z);
        corners[3] = center + new Vector3(-extents.x, extents.y, extents.z);
        corners[4] = center + new Vector3(extents.x, -extents.y, -extents.z);
        corners[5] = center + new Vector3(extents.x, -extents.y, extents.z);
        corners[6] = center + new Vector3(extents.x, extents.y, -extents.z);
        corners[7] = center + new Vector3(extents.x, extents.y, extents.z);

        var hasFront = false;
        var hasBack = false;

        foreach (var corner in corners)
        {
            var side = plane.GetDistanceToPoint(corner);
            if (side > 0) hasFront = true;
            else if (side < 0) hasBack = true;

            if (hasFront && hasBack)
                return true; // 有点在两侧 → 平面穿过
        }

        return false; // 全部在一侧
    }

    private void SetObj(GameObject obj)
    {
        obj.transform.SetParent(ObjectContainer, false);
        obj.AddComponent<MeshCollider>();
        RecenterObjectToMesh(obj);
        obj.tag = "Sliceable";
    }

    private void RecenterObjectToMesh(GameObject obj)
    {
        var mf = obj.GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null) return;

        var mesh = mf.sharedMesh;
        var verts = mesh.vertices;

        // 1. 计算几何中心
        var center = Vector3.zero;
        foreach (var v in verts)
            center += v;
        center /= verts.Length;

        // 2. 所有顶点移动到以中心为原点
        for (var i = 0; i < verts.Length; i++)
            verts[i] -= center;

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // 3. 把物体的位置加上中心偏移（转换为世界空间）
        obj.transform.position += obj.transform.TransformVector(center);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Plane = UnityEngine.Plane;

public class MouseSlice : MonoBehaviour
{
    public Transform ObjectContainer;

    public ScreenLineRender lineRenderer;
    private Plane slicePlane;
    private bool drawplane;
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
        Vector3 normalVec = Vector3.Cross(Planepos, depth);
        
        SliceObjects(start, normalVec);
        slicePlane.SetNormalAndPosition(normalVec,start);
        //Showplane(slicePlane);
    }
    
    private void SliceObjects(Vector3 point, Vector3 normalVec)
    {
        GameObject[] toSlice = GameObject.FindGameObjectsWithTag("Sliceable");
        GameObject obj;
        for (int i = 0; i < toSlice.Length; ++i)
        {
            obj = toSlice[i];
            if (IsObjectIntersectedByPlane(obj, slicePlane))
            {
                Debug.Log(obj.name);
                SlicedHull hull = obj.Slice(point, normalVec);
                Material originalMaterial = obj.GetComponent<MeshRenderer>().sharedMaterial;
                if (hull != null)
                {
                    GameObject upperhull = hull.CreateUpperHull(obj, originalMaterial);
                    GameObject lowerhull = hull.CreateLowerHull(obj, originalMaterial);
                                    
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
        Bounds bounds = obj.GetComponent<Renderer>().bounds;

        // 检查包围盒的 8 个点是否在平面两侧
        Vector3[] corners = new Vector3[8];
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        corners[0] = center + new Vector3(-extents.x, -extents.y, -extents.z);
        corners[1] = center + new Vector3(-extents.x, -extents.y, extents.z);
        corners[2] = center + new Vector3(-extents.x, extents.y, -extents.z);
        corners[3] = center + new Vector3(-extents.x, extents.y, extents.z);
        corners[4] = center + new Vector3(extents.x, -extents.y, -extents.z);
        corners[5] = center + new Vector3(extents.x, -extents.y, extents.z);
        corners[6] = center + new Vector3(extents.x, extents.y, -extents.z);
        corners[7] = center + new Vector3(extents.x, extents.y, extents.z);

        bool hasFront = false;
        bool hasBack = false;

        foreach (Vector3 corner in corners)
        {
            float side = plane.GetDistanceToPoint(corner);
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
    void RecenterObjectToMesh(GameObject obj)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null) return;

        Mesh mesh = mf.sharedMesh;
        Vector3[] verts = mesh.vertices;

        // 1. 计算几何中心
        Vector3 center = Vector3.zero;
        foreach (var v in verts)
            center += v;
        center /= verts.Length;

        // 2. 所有顶点移动到以中心为原点
        for (int i = 0; i < verts.Length; i++)
            verts[i] -= center;

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // 3. 把物体的位置加上中心偏移（转换为世界空间）
        obj.transform.position += obj.transform.TransformVector(center);
    }
}

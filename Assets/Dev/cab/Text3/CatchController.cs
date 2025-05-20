using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchController3 : MonoBehaviour
{
    public LayerMask InteractorlayerMask;
    public LayerMask Putlayermask;
    private RaycastHit hitInfo;
    public new Camera camera;
    private Transform heldObject;
    private float distance;
    private Ray ray;

    private float olddistansexz;
    private float newdistansexz;
    private float js=1f;
    private void Update()
    {
        FindheldObject();
    }

    private void FindheldObject()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            if (Physics.Raycast(ray, out hitInfo, 1000f,InteractorlayerMask))
            {
                //Debug.Log("find");
                heldObject = hitInfo.collider.transform;
                distance = Vector3.Distance(ray.origin, heldObject.position);
            }
        }
        Catch();
        Put();
    }

    /*private void Catch()
    {
       // currentHoldDistance += Input.GetAxis("Mouse ScrollWheel") ;
        Debug.Log(currentHoldDistance);
        currentHoldDistance = Mathf.Clamp(currentHoldDistance, 0.2f, 2f);
        if (heldObject != null && Input.GetMouseButton(0))
        {
            
            Vector3 newPos = ray.origin +ray.direction * (currentHoldDistance * distance);
            heldObject.position = newPos;
            heldObject.rotation = Quaternion.identity;
            //heldObject.LookAt(camera.transform);
        }
    }*/
    private void Change(Rigidbody rb)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, Putlayermask))
        {
            /*Vector2 oldPos = new Vector2(heldObject.position.x, heldObject.position.z);
            Vector2 newPos = new Vector2(hit.point.x, hit.point.z);
            Vector2 startPos = new Vector2(ray.origin.x, ray.origin.z);
            float oldDistance = Vector2.Distance(oldPos, startPos);
            float newDistance = Vector2.Distance(newPos, startPos);
            Debug.Log("oldDistance: " + oldDistance);
            Debug.Log("newDistance: " + newDistance);
            oldDistance += heldObject.localScale.x*0.5f;
            //Debug.Log(heldObject.localScale.x);
            float scale = newDistance / oldDistance;*/
            Collider collider =heldObject.GetComponent<Collider>();
            float offsetDistance = GetColliderProjection(collider, hit.point);
            Debug.Log(heldObject.localScale.x);
           // float offsetDistance = heldObject.localScale.x*0.5f;
            Vector3 finalPosition = hit.point + hit.normal.normalized * offsetDistance;
            float oldDistance = distance;
            float newDistance = Vector3.Distance(ray.origin, finalPosition);
            float scale = newDistance / oldDistance;
            Debug.Log(scale);
            if (js != scale)
            {
                heldObject.localScale /= js;
                heldObject.localScale *= scale;
                js=scale;
            }
            
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.MovePosition(finalPosition);
        }
    }
    private void Catch()
    {
        if (heldObject != null && Input.GetMouseButton(0))
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb.useGravity)
            {
                rb.useGravity = false;
            }
            Change(rb);
        }
    }

    private void Put()
    {
        if (heldObject != null && Input.GetMouseButtonUp(0))
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }
            Change(rb);
            js = 1f;
        }
    }

    private float GetColliderProjection(Collider collider, Vector3 direction)
    {
        direction = direction.normalized;

        if (collider is BoxCollider boxCollider)
        {
            Vector3 scaledSize = Vector3.Scale(boxCollider.size, collider.transform.lossyScale);
            Vector3 absDirection = new Vector3(
                Mathf.Abs(direction.x),
                Mathf.Abs(direction.y),
                Mathf.Abs(direction.z)
            );
            return Vector3.Dot(scaledSize * 0.5f, absDirection);
        }
        else
            return 0;
    }
}

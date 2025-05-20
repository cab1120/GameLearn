using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameObject Player;
    
    private Vector3 oldPlayerPosition;
    private float oldDistance;
    private float newDistance;
    private float scale;

    void Start()
    {
        oldPlayerPosition = Player.transform.position;
    }
    void Update()
    {
        GetDistance();
    }

    private void LateUpdate()
    {
        oldPlayerPosition = Player.transform.position;
    }

    private void GetDistance()
    {
        //Debug.Log(Player.transform.position);
        //Debug.Log(keyPrefab.transform.position.x);
        newDistance = Vector3.Distance(Player.transform.position, keyPrefab.transform.position);
        if(oldDistance==0)oldDistance = newDistance;
        if (newDistance-oldDistance>0.01||newDistance-oldDistance<-0.01)
        {
            scale = newDistance/oldDistance;
            ChangeKey(scale);
            oldDistance = newDistance;
        }
    }

    private void ChangeKey(float Scale)
    {
        BoxCollider boxCollider = keyPrefab.GetComponent<BoxCollider>();
        Debug.Log(boxCollider.center.y);
        Rigidbody rb = keyPrefab.GetComponent<Rigidbody>();
        
        float oldHeighty = boxCollider.size.y*keyPrefab.transform.localScale.y;
        float Ground = keyPrefab.transform.position.y-oldHeighty*0.5f;
        
        keyPrefab.transform.localScale *= Scale;
        
        float newHeighty = boxCollider.size.y*keyPrefab.transform.localScale.y;
        float newY = Ground+newHeighty*0.5f;
        
        Vector3 newPos = new Vector3(keyPrefab.transform.position.x, newY, keyPrefab.transform.position.z);
        
        /*rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.MovePosition(newPos);*/
        keyPrefab.transform.position = newPos;

        
    }
    
}

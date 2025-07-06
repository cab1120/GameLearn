using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPen : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameObject Player;
    
    private float oldDistance;
    private float newDistance;
    private float scale;
    
    void LateUpdate()
    {
        GetDistance();
    }
    
    private void GetDistance()
    {
        newDistance = Vector3.Distance(Player.transform.position, keyPrefab.transform.position);
        if(oldDistance==0)oldDistance = newDistance;
        if (newDistance-oldDistance>0.03||newDistance-oldDistance<-0.03)
        {
            scale = newDistance/oldDistance;
            ChangeKey(scale);
            oldDistance = newDistance;
        }
    }

    private void ChangeKey(float Scale)
    {
        BoxCollider boxCollider = keyPrefab.GetComponent<BoxCollider>();
        
        float oldHeighty = boxCollider.size.y*keyPrefab.transform.localScale.y;
        float Ground = keyPrefab.transform.position.y-oldHeighty*0.5f;
        
        keyPrefab.transform.localScale *= Scale;
//        Debug.Log(Scale);
        float newHeighty = boxCollider.size.y*keyPrefab.transform.localScale.y;
        float newY = Ground+newHeighty*0.5f;
        
        Vector3 newPos = new Vector3(keyPrefab.transform.position.x, newY, keyPrefab.transform.position.z);
        
        keyPrefab.transform.position = newPos;

        
    }
}

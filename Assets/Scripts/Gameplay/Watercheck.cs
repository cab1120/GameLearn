using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercheck : MonoBehaviour
{
    public MoveController moveController;
    public CatchPen catchPen;
    public ThrowPen throwPen;
    public GameObject communicate;
    public GameObject Text1, Text2, Text3,Text4,Text5;

    public GameObject fakeFloor;
    private bool canMove = true;
    private int num = 1,times=1;

    private void Update()
    {
        if (!canMove)
        {
            Showtext();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player")&&times==1)
        {
            Cursor.lockState = CursorLockMode.Confined;
            moveController.enabled = false;
            catchPen.enabled = false;
            throwPen.enabled = false;
            canMove = false;
            communicate.SetActive(true);
        }
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
                fakeFloor.SetActive(false);
                Text3.SetActive(true);
                break;
            case 4:
                Text3.SetActive(false);
                Text4.SetActive(true);
                break;
            case 5:
                Text4.SetActive(false);
                Text5.SetActive(true);
                break;
            case 6:
                Debug.Log("222");
                times++;
                communicate.SetActive(false);
                moveController.enabled = true;
                catchPen.enabled = true;
                throwPen.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    static public SwitchController instance;
    public GameObject MaxPlayer;
    public GameObject MinPlayer;
    public GameObject MiddlePlayer;
    public GameObject MaxCamera;
    public GameObject MinCamera;
    public GameObject MiddleCamera;
    public int PlayerNumber=2;

    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwitchBigger();
        if (Input.GetKeyDown(KeyCode.Q))
            SwitchSmaller();
    }

    private void SwitchBigger()
    {
        if (PlayerNumber == 1)
        {
            //MinPlayer.GetComponent<PlayerController>().enabled = false;
            //MinPlayer.GetComponent<KeyController>().enabled = false;
            MinCamera.SetActive(false);
            //MiddlePlayer.GetComponent<PlayerController>().enabled = true;
            //MiddlePlayer.GetComponent<KeyController>().enabled = true;
            MiddleCamera.SetActive(true);
            PlayerNumber=2;
            ConnectedController3.instance.currentState = ConnectedController3.State.MidlleRoom;
        }
        else if (PlayerNumber == 2)
        {
            //MiddlePlayer.GetComponent<PlayerController>().enabled = false;
            //MiddlePlayer.GetComponent<KeyController>().enabled = false;
            MiddleCamera.SetActive(false);
            //MaxPlayer.GetComponent<PlayerController>().enabled = true;
            //MaxPlayer.GetComponent<KeyController>().enabled = true;
            MaxCamera.SetActive(true);
            PlayerNumber=3;
            ConnectedController3.instance.currentState = ConnectedController3.State.MaxRoom;
        }
    }

    private void SwitchSmaller()
    {
        if (PlayerNumber == 3)
        {
            //MaxPlayer.GetComponent<PlayerController>().enabled = false;
            //MaxPlayer.GetComponent<KeyController>().enabled = false;
            MaxCamera.SetActive(false);
            //MiddlePlayer.GetComponent<PlayerController>().enabled = true;
            //MiddlePlayer.GetComponent<KeyController>().enabled = true;
            MiddleCamera.SetActive(true);
            PlayerNumber = 2;
            ConnectedController3.instance.currentState = ConnectedController3.State.MidlleRoom;
        }
        else if (PlayerNumber == 2)
        {
            //MiddlePlayer.GetComponent<PlayerController>().enabled = false;
            //MiddlePlayer.GetComponent<KeyController>().enabled = false;
            MiddleCamera.SetActive(false);
            //MinPlayer.GetComponent<PlayerController>().enabled = true;
            //MinPlayer.GetComponent<KeyController>().enabled = true;
            MinCamera.SetActive(true);
            PlayerNumber = 1;
            ConnectedController3.instance.currentState = ConnectedController3.State.MinRoom;
        }
    }
}

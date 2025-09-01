using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    
    public GameObject waterCheck;
    public GameObject Drink;
    public GameObject fakeFloor;
    public GameObject paperPlane;

    public GameObject player;
    public GameObject fakeplayer;
    public GameObject camera2;

    public GameObject DoorCheck1;
    public GameObject DoorCheck2;
    public GameObject SecondPen;
    public GameObject Pen;
    public GameObject fakePlane;
    public GameObject Plane;
    
    public CatchPen catchPen;
    public Connetted connetted;
    public PlayerInputHandler playerInputHandler;
    public PlayerMovement playerMovement;

    public GameObject Endpic;
    
    public Camera penCamera; 
    public Camera mainCamera;
    
    public Rigidbody penRigidbody;
    void Awake()
    {
        instance = this;
        
    }
    
}

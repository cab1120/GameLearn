using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    
    public GameObject waterCheck;
    public GameObject Drink;
    public GameObject fakeFloor;

    public GameObject player;
    public GameObject fakeplayer;
    public GameObject camera2;
    
    public ThrowPen throwPen;
    public Connetted connetted;
    public PlayerInputHandler playerInputHandler;
    public PlayerMovement playerMovement;

    public GameObject Endpic;
    void Awake()
    {
        instance = this;
    }
    
}

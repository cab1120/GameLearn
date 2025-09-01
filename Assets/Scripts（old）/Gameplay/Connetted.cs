using System;
using UnityEngine;

public class Connetted : MonoBehaviour
{
    public static Connetted instance;
    public GameObject player;
    public GameObject player2;
    public Animator animator;
    private bool canMove=true;
    
    private void OnEnableMove()=>canMove=true;

    private void OnDisableMove()
    {
        canMove=false;
        animator.SetBool("CanWalk", false);
    }

    private void OnEnable()
    {
        EventManager.OnEnableConnectedMovement+=OnEnableMove;
        EventManager.OnDisableConnectedMovement+=OnDisableMove;
    }

    private void OnDisable()
    {
        EventManager.OnEnableConnectedMovement-=OnEnableMove;
        EventManager.OnDisableConnectedMovement-=OnDisableMove;
    }

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
                animator.SetBool("CanWalk", true);
            else
                animator.SetBool("CanWalk", false);
        }
        
    }

    private void LateUpdate()
    {
        player2.transform.localPosition = player.transform.localPosition;
        player2.transform.localRotation = player.transform.localRotation;
    }
}
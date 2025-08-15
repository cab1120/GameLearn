using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player/Stats")]
public class PlayerState : ScriptableObject
{
    [Header("Mouse")]
    public float MouseSensitivity = 100f;
    
    [Header("Movement")]
    public float Movespeed = 3f;
    
    [Header("Jumping")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float groundDistance = 0.4f;
    
    [Header("Checker")]
    public bool canJump = true;
}

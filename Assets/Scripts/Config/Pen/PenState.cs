using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPenStats", menuName = "Pen/Stats")]
public class PenState : ScriptableObject
{
    [Header("Speed")]
    public float moveSpeed = 5f;          // 笔的水平移动速度
    public float downwardSpeed = 2f;
}

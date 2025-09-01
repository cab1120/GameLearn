using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}

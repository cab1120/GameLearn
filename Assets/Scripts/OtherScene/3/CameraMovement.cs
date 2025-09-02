using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Update()
    {
        var vector3 = transform.position;
        vector3.z = vector3.z - Time.deltaTime * 0.5f;
        vector3.x = vector3.x - Time.deltaTime * 0.5f;
        transform.position = vector3;
    }
}

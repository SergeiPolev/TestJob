using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorScript : MonoBehaviour
{
    public float rotationSpeedX = 0f;
    public float rotationSpeedY = 0f;
    public float rotationSpeedZ = 0f;

    private void FixedUpdate()
    {
        transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ, Space.World);
    }
}

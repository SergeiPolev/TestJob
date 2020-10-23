using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    public float movingPointX = 0f;
    public float movingPointY = 0f;
    public float movingPointZ = 0f;

    public float step = 1f;

    Vector3 targetPoint;
    Vector3 startingPoint;
    Vector3 currentTargetPoint;

    private void Start()
    {
        targetPoint = new Vector3(movingPointX, movingPointY, movingPointZ);
        startingPoint = transform.localPosition;
        currentTargetPoint = targetPoint;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.localPosition, currentTargetPoint) >= 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTargetPoint, step);
        }
        else
        {
            currentTargetPoint = (currentTargetPoint == targetPoint ? startingPoint : targetPoint);
        }
    }
}
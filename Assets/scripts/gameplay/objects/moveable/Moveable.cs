using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionFace
{
    Up,
    UpLeft,
    UpRight,
    Down,
    DownLeft,
    DownRight,
    Left,
    Right
}

public class Moveable : MonoBehaviour
{

    private float moveSpeed = 4.0f;
    private float timeToDestination = 0.3f;
    private Vector3 targetPosition = new Vector3();
    private Vector3 velocity = Vector3.zero;
    private DirectionFace lastKnownDirection = DirectionFace.DownLeft;
    private float deltaThreshold = 0.2f;
    private float closenessBuffer = 0.4f;

    protected virtual void Awake()
    {
        targetPosition = transform.position;
    }

    public void setTargetPosition(Vector3 newTargetPosition)
    {
        if (!targetIsTooClose(newTargetPosition))
        {
            targetPosition.x = newTargetPosition.x;
            targetPosition.y = newTargetPosition.y;
            targetPosition.z = newTargetPosition.z;

            processLastKnownDirection();
        }
    }

    public void setMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void setClosenessBuffer(float newValue)
    {
        closenessBuffer = newValue;
    }

    private bool targetIsTooClose(Vector3 target)
    {
        return Vector3.Distance(target, transform.position) < deltaThreshold + closenessBuffer;
    }

    private void processLastKnownDirection()
    {
            
        float softThreshold = deltaThreshold;
        float deltaX = targetPosition.x - transform.position.x;
        float deltaY = targetPosition.y - transform.position.y;
            
        if (Mathf.Abs(deltaX) < softThreshold) { deltaX = 0; }
        if (Mathf.Abs(deltaY) < softThreshold) { deltaY = 0; }

        if (deltaX != 0 || deltaY != 0)
        {
            if (deltaX > 0 && deltaY > 0)
            {
                lastKnownDirection = DirectionFace.UpRight;
            }
            else if (deltaX < 0 && deltaY > 0)
            {
                lastKnownDirection = DirectionFace.UpLeft;
            }
            else if (deltaX > 0 && deltaY < 0)
            {
                lastKnownDirection = DirectionFace.DownRight;
            }
            else if (deltaX < 0 && deltaY < 0)
            {
                lastKnownDirection = DirectionFace.DownLeft;
            }
            else if (deltaX > 0)
            {
                lastKnownDirection = DirectionFace.Right;
            }
            else if (deltaX < 0)
            {
                lastKnownDirection = DirectionFace.Left;
            }
            else if (deltaY > 0)
            {
                lastKnownDirection = DirectionFace.Up;
            }
            else if (deltaY < 0)
            {
                lastKnownDirection = DirectionFace.Down;
            }

        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeToDestination, moveSpeed);
    }
}

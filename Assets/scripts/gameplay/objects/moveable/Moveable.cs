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

    protected virtual void Awake()
    {
        targetPosition = transform.position;
    }

    public void setTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition.x = newTargetPosition.x;
        targetPosition.y = newTargetPosition.y;
        targetPosition.z = newTargetPosition.z;
    }

    public void setMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    private void processLastKnownDirection()
    {
        if (!Helpers.vector3Approximately(targetPosition, transform.position))
        {
            float deltaX = targetPosition.x - transform.position.x;
            float deltaY = targetPosition.y - transform.position.y;
            //if delta x or y is less than a threshold change to zero
            //if both x and y are changed or not significant movement. use the last known
            //direction
            if (deltaX > 0 && deltaY > 0)
            {
                lastKnownDirection = DirectionFace.UpRight;
            } else if(deltaX < 0 && deltaY > 0){
                lastKnownDirection = DirectionFace.UpLeft;
            } else if (deltaX > 0 && deltaY < 0)
            {
                lastKnownDirection = DirectionFace.DownRight;
            } else if (deltaX < 0 && deltaY < 0)
            {
                lastKnownDirection = DirectionFace.DownLeft;
            } else if (deltaX > 0)
            {
                lastKnownDirection = DirectionFace.Right;
            } else if (deltaX < 0)
            {
                lastKnownDirection = DirectionFace.Left;
            } else if (deltaY > 0)
            {
                lastKnownDirection = DirectionFace.Up;
            } else if (deltaY < 0)
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
        processLastKnownDirection();
        Debug.Log(lastKnownDirection);
    }
}

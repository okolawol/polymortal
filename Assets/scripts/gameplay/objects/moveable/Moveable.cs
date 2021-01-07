using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{

    private float moveSpeed = 4.0f;
    private float timeToDestination = 0.3f;
    private Vector3 targetPosition = new Vector3();
    private Vector3 velocity = Vector3.zero;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Moveable
{
    private void moveOnTouch()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            setTargetPosition(targetPos);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        moveOnTouch();
        base.Update();
    }
}

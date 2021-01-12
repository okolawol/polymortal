using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Moveable
{
    private Animator animator;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void moveOnTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            setTargetPosition(targetPos);
        }
    }

    private void updateSprite()
    {
        updateStanding();
        updateFlip();
    }

    private void updateStanding()
    {
        if (lastKnownDirection == DirectionFace.Up)
        {
            animator.SetInteger("standing_options", MoveableGlobals.UP);
        }
        else if (lastKnownDirection == DirectionFace.UpLeft || lastKnownDirection == DirectionFace.UpRight)
        {
            animator.SetInteger("standing_options", MoveableGlobals.UP_LEFT);
        }
        else if (lastKnownDirection == DirectionFace.Left || lastKnownDirection == DirectionFace.Right)
        {
            animator.SetInteger("standing_options", MoveableGlobals.LEFT);
        }
        else if (lastKnownDirection == DirectionFace.DownLeft || lastKnownDirection == DirectionFace.DownRight)
        {
            animator.SetInteger("standing_options", MoveableGlobals.DOWN_LEFT);
        }
        else if (lastKnownDirection == DirectionFace.Down)
        {
            animator.SetInteger("standing_options", MoveableGlobals.DOWN);
        }
    }

    private void updateFlip()
    {
        if (lastKnownDirection == DirectionFace.Right
            || lastKnownDirection == DirectionFace.DownRight
            || lastKnownDirection == DirectionFace.UpRight)
        {
            sprite.flipX = true;
        } else
        {
            sprite.flipX = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        updateSprite();
        moveOnTouch();
        base.Update();
    }
}

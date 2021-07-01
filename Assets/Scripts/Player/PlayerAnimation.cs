using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Player
{
    private Animator m_Animator;

    private string m_CurrentAnimation = "";

    private enum FacingDirection 
    { 
        FacingRight,
        FacingLeft,
        FacingUp,
        FacingDown
    }

    private enum AnimNames
    {
        WalkingRight,
        WalkingLeft,
        WalkingUp,
        WalkingDown,
        IdleRight,
        IdleLeft,
        IdleUp,
        IdleDown
    }

    private FacingDirection m_FacingDirection;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
        m_FacingDirection = FacingDirection.FacingDown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if(m_Rb.velocity == Vector2.zero)
        {
            if(m_FacingDirection == FacingDirection.FacingRight)
            {
                ChangeAnimation(AnimNames.IdleRight);
            }
            else if (m_FacingDirection == FacingDirection.FacingLeft)
            {
                ChangeAnimation(AnimNames.IdleLeft);
            }
            else if (m_FacingDirection == FacingDirection.FacingUp)
            {
                ChangeAnimation(AnimNames.IdleUp);
            }
            else if (m_FacingDirection == FacingDirection.FacingDown)
            {
                ChangeAnimation(AnimNames.IdleDown);
            }
        }

        else if(m_Rb.velocity.x > 0.1f)
        {
            ChangeAnimation(AnimNames.WalkingRight);
            m_FacingDirection = FacingDirection.FacingRight;
        }
        else if (m_Rb.velocity.x < -0.1f)
        {
            ChangeAnimation(AnimNames.WalkingLeft);
            m_FacingDirection = FacingDirection.FacingLeft;
        }
        else if (m_Rb.velocity.y > 0.1f)
        {
            ChangeAnimation(AnimNames.WalkingUp);
            m_FacingDirection = FacingDirection.FacingUp;
        }
        else if (m_Rb.velocity.y < -0.1f)
        {
            ChangeAnimation(AnimNames.WalkingDown);
            m_FacingDirection = FacingDirection.FacingDown;
        }
    }

    private void ChangeAnimation(AnimNames animationName)
    {
        if (m_CurrentAnimation == animationName.ToString()) return;

        m_CurrentAnimation = animationName.ToString();
        m_Animator.Play(m_CurrentAnimation);
    }
}

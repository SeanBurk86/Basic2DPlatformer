﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    private bool isGrounded,
        isTouchingWall,
        isTouchingWallBack,
        isJumping,
        jumpInput,
        grabInput,
        attackInput,
        rollInput,
        jumpInputStop,
        isOnCoyoteTime,
        isOnWallJumpCoyoteTime,
        oldIsTouchinghWall,
        oldIsTouchingWallBack,
        isTouchingLedge,
        isTouchingMovingPlatform,
        dashInput;

    private float xInput,
        presetFriction;
    private int xInputNormalized;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchinghWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingMovingPlatform = player.CheckIfTouchingMovingPlatform();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if(!isOnWallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchinghWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        presetFriction = player.RB.sharedMaterial.friction;
        player.RB.sharedMaterial.friction = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.sharedMaterial.friction = presetFriction;
        oldIsTouchinghWall = false;
        oldIsTouchinghWall = false;
        isTouchingWall = false;
        isTouchingWallBack = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.InputXNormalized;
        xInputNormalized = (int)(player.InputHandler.InputXNormalized * Vector2.right).normalized.x;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        rollInput = player.InputHandler.RollInput;

        CheckJumpMultiplier();

        if (grabInput) { player.EnableGrabber(); }
        else { player.DisableGrabber(); }

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || isOnWallJumpCoyoteTime))
        {
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            isOnCoyoteTime = false;
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackInput && player.KickState.CanKick())
        {
            stateMachine.ChangeState(player.KickState);
        }
        else if (rollInput && player.RollState.CanRoll())
        {
            stateMachine.ChangeState(player.RollState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInputNormalized == player.FacingDirection && player.CurrentVelocity.y <= 0 )
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            player.CheckIfShouldFlip(xInputNormalized);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if(isOnCoyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            isOnCoyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (isOnWallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            isOnWallJumpCoyoteTime = true;
        }
        else
        {
            isOnWallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => isOnCoyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        isOnWallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
        

    public void StopWallJumpCoyoteTime() => isOnWallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}

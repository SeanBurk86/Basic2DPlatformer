using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded,
        isTouchingWall,
        grabInput,
        jumpInput,
        isTouchingLedge,
        isTouchingMovingPlatform;

    protected int xInputNormalized,
        yInput;

    protected float xInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingMovingPlatform = player.CheckIfTouchingMovingPlatform();


        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInputNormalized = (int)(player.InputHandler.InputXNormalized * Vector2.right).normalized.x;
        yInput = player.InputHandler.InputYNormalized;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInputNormalized != player.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

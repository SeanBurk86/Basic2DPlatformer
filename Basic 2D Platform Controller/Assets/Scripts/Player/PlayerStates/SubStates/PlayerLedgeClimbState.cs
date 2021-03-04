using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos,
        cornerPos,
        startPos,
        stopPos;

    private bool isHanging,
        isClimbing,
        isTouchingWall,
        isTouchingLedge,
        jumpInput;

    private int xInput,
        yInput;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
        player.UpdateMovingPlatformPositionOffset();
        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.position = detectedPos;
        player.UpdateAndSetMovingPlatformOffsetPosition();
        SetDetectedPosition(player.transform.position);
        player.SetVelocityZero();
    }

    

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {   
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        player.transform.position = detectedPos;
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            player.transform.position = stopPos;
            if (player.CheckForCeiling())
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else
        {
            xInput = player.InputHandler.InputXNormalized;
            yInput = player.InputHandler.InputYNormalized;
            jumpInput = player.InputHandler.JumpInput;

            player.SetMovingPlatformOffsetPosition();
            SetDetectedPosition(player.transform.position);
            player.SetVelocityZero();

            cornerPos = player.DetermineCornerPosition();
            SetStartAndStopPositions();
            player.transform.position = startPos;

            if (xInput == player.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                player.WallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.WallJumpState);
            }
        }
    }


    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;


    private void SetStartAndStopPositions()
    {
        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);
    }
}

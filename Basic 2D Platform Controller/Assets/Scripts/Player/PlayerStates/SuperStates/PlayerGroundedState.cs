﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInputNormalized,
        yInput;
    protected float xInput;
    protected bool isTouchingCeiling;

    private bool jumpInput,
        grabInput,
        attackInput,
        rollInput,
        isGrounded,
        isTouchingWall,
        isTouchingLedge,
        dashInput,
        interactInput;

    private float shotDirectionInputX,
        shotDirectionInputY;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingCeiling = player.CheckForCeiling();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.KickState.ResetAmountOfKicksLeft();
        player.RollState.ResetAmountOfRollsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.InputXNormalized;
        xInputNormalized = (int)(player.InputHandler.InputXNormalized * Vector2.right).normalized.x;
        yInput = player.InputHandler.InputYNormalized;

        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        rollInput = player.InputHandler.RollInput;
        interactInput = player.InputHandler.InteractButtonInput;


        if (interactInput) { player.EnableUseBox(); }
        else { player.DisableUseBox(); }

        if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackInput && player.KickState.CanKick() && player.SlideKickState.CheckIfCanSlideKick() && (isTouchingCeiling || yInput == -1))
        {
            stateMachine.ChangeState(player.SlideKickState);
        }
        else if (attackInput && player.KickState.CanKick() && player.KickState.isKickCooledDown() && !isTouchingCeiling && yInput != -1)
        {
            stateMachine.ChangeState(player.KickState);
        }
        else if (rollInput && player.RollState.CanRoll())
        {
            stateMachine.ChangeState(player.RollState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

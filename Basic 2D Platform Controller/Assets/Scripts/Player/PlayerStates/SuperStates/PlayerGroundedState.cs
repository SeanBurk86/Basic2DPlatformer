using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput,
        yInput;

    protected bool isTouchingCeiling;

    private bool jumpInput,
        grabInput,
        attackInput,
        rollInput,
        isGrounded,
        isTouchingWall,
        isTouchingLedge,
        dashInput;

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
        yInput = player.InputHandler.InputYNormalized;
        shotDirectionInputX = player.InputHandler.ShotDirectionInput.x;
        shotDirectionInputY = player.InputHandler.ShotDirectionInput.y;

        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        rollInput = player.InputHandler.RollInput;

        if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackInput && player.KickState.CanKick() && !isTouchingCeiling)
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

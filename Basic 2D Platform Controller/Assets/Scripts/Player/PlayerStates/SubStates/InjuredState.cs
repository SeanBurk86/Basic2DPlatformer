using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredState : PlayerState
{
    private bool isGrounded,
        isTouchingWall,
        isTouchingWallBack,
        isJumping,
        jumpInput,
        grabInput,
        jumpInputStop,
        isOnCoyoteTime,
        isOnWallJumpCoyoteTime,
        oldIsTouchinghWall,
        oldIsTouchingWallBack,
        isTouchingLedge,
        hasTakenDamage,
        dashInput;

    public InjuredState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingWall();

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.SetSquishChecksYScaleAndYPosition(playerData.crouchSquishCheckScale, playerData.crouchSquishCheckPosition);
        player.SetColliderHeightAndOffset(playerData.crouchColliderHeight, playerData.crouchColliderYOffset);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeightAndOffset(playerData.standColliderHeight, playerData.standColliderYOffset);
        player.SetSquishChecksYScaleAndYPosition(playerData.standSquishCheckScale, playerData.standSquishCheckPosition);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}

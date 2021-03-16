using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeightAndOffset(playerData.crouchColliderHeight, playerData.crouchColliderYOffset);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeightAndOffset(playerData.standColliderHeight, playerData.standColliderYOffset);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityX(playerData.crouchMovementVelocity * player.FacingDirection);
            player.CheckIfShouldFlip(xInputNormalized);
            if(xInputNormalized == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}

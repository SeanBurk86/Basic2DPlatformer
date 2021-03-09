using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 staticHoldPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        
    }

    public override void Enter()
    {
        base.Enter();
        player.KickState.ResetAmountOfKicksLeft();
        player.RollState.ResetAmountOfRollsLeft();

        staticHoldPosition = player.transform.position;
        if (player.CheckIfTouchingMovingPlatform())
        {
            player.UpdateMovingPlatformPositionOffset();
            MovingHoldPosition();
        }
        else
        {
            staticHoldPosition = player.transform.position;
            StaticHoldPosition();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (player.CheckIfTouchingMovingPlatform())
            {
                MovingHoldPosition();
            }
            else
            {
                StaticHoldPosition();
            }
             
            

            if (yInput > 0)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
    }
       

    private void StaticHoldPosition()
    {
        player.transform.position = staticHoldPosition;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }

    private void MovingHoldPosition()
    {
        player.SetMovingPlatformOffsetPosition();
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

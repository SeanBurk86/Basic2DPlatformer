using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    private int amountOfRollsLeft;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfRollsLeft = playerData.amountOfRolls;
    }

    public override void AnimationFinishTrigger()
    {
        isAbilityDone = true;
        player.EnableFlip();
        player.Anim.SetBool("roll", false);
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        Debug.Log("Roll Animation Trigger called");
        isAbilityDone = false;
        player.DisableFlip();
        player.ApplyRollThrust();
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        player.InputHandler.UseRollInput();
        amountOfRollsLeft--;
        base.Enter();
        player.DisablePlayerDamage();
        player.SetColliderHeightAndOffset(playerData.crouchColliderHeight, playerData.crouchColliderYOffset);
    }

    public override void Exit()
    {
        player.SetColliderHeightAndOffset(playerData.standColliderHeight, playerData.standColliderYOffset);
        player.EnablePlayerDamage();
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (player.CheckIfGrounded() && !player.CheckForCeiling())
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (player.CheckIfGrounded() && player.CheckForCeiling())
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public bool CanRoll()
    {
        if (amountOfRollsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfRollsLeft() => amountOfRollsLeft = playerData.amountOfRolls;
}

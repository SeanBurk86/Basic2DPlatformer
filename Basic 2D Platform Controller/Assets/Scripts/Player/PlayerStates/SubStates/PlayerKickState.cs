using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickState : PlayerAbilityState
{
    private bool isKicking;

    private AttackDetails attackDetails;

    private int amountOfKicksLeft;

    public PlayerKickState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfKicksLeft = playerData.amountOfKicks;
    }

    public override void AnimationFinishTrigger()
    {
        isKicking = false;
        player.EnableFlip();
        player.Anim.SetBool("kick", false);
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        isKicking = true;
        player.DisableFlip();
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        player.InputHandler.UseAttackInput();
        amountOfKicksLeft--;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (player.CheckIfGrounded())
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

        if (isKicking) 
        {
            attackDetails.damageAmount = playerData.kickDamage;
            attackDetails.hitCollisionPosition = player.transform.position;
            attackDetails.stunDamageAmount = 10;
            Collider2D[] detectedObjects = player.CheckIfKickbox();

            foreach(Collider2D collider2D in detectedObjects)
            {
                collider2D.transform.parent.SendMessage("Damage", attackDetails);
            }
        }

        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CanKick()
    {
        if(amountOfKicksLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfKicksLeft() => amountOfKicksLeft = playerData.amountOfKicks;
}

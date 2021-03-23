﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickState : PlayerAbilityState
{

    private AttackDetails attackDetails;

    private int amountOfKicksLeft;

    private float lastKickTime;

    public PlayerKickState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfKicksLeft = playerData.amountOfKicks;
    }

    public override void AnimationFinishTrigger()
    {
        isAbilityDone = true;
        player.EnableFlip();
        player.Anim.SetBool("kick", false);
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        isAbilityDone = false;
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
        lastKickTime = Time.time;
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
        else if (!isAbilityDone) 
        {
            attackDetails.damageAmount = playerData.kickDamage;
            attackDetails.hitCollisionPosition = player.kickCheck.position;
            attackDetails.stunDamageAmount = 10;
            attackDetails.attackerFacingDirection = player.FacingDirection;
            Collider2D[] detectedObjects = player.CheckIfKickbox();

            foreach(Collider2D collider2D in detectedObjects)
            {
                if(collider2D.transform.parent != null)
                {
                    if (collider2D.transform.parent.CompareTag("Enemy")
                                        || collider2D.transform.parent.CompareTag("Kickable"))
                    {
                        collider2D.transform.parent.SendMessage("Damage", attackDetails);
                    }
                }
            }
        }

        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CanKick()
    {
        if(amountOfKicksLeft > 0 && player.CanMelee)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isKickCooledDown()
    {
        return Time.time >= lastKickTime + playerData.kickCooldown;
    }

    public void ResetAmountOfKicksLeft() => amountOfKicksLeft = playerData.amountOfKicks;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideKickState : PlayerAbilityState
{
    private AttackDetails attackDetails;
    private int amountOfKicksLeft,
        yInput;


    private float lastSlideKickTime;

    public PlayerSlideKickState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        isAbilityDone = true;
        player.EnableFlip();
        player.Anim.SetBool("slideKick", false);
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        isAbilityDone = false;
        player.DisableFlip();
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        player.InputHandler.UseAttackInput();
        amountOfKicksLeft--;
        lastSlideKickTime = Time.time;
        base.Enter();
        player.SetSquishChecksYScaleAndYPosition(playerData.crouchSquishCheckScale, playerData.crouchSquishCheckPosition);
        player.SetColliderHeightAndOffset(playerData.crouchColliderHeight, playerData.crouchColliderYOffset);
    }

    public override void Exit()
    {
        player.SetColliderHeightAndOffset(playerData.standColliderHeight, playerData.standColliderYOffset);
        player.SetSquishChecksYScaleAndYPosition(playerData.standSquishCheckScale, playerData.standSquishCheckPosition);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        yInput = player.InputHandler.InputYNormalized;

        if (isAnimationFinished)
        {
            if (player.CheckIfGrounded() && (yInput == -1 || player.CheckForCeiling()))
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if(player.CheckIfGrounded())
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(!player.CheckForCeiling())
            {
                Debug.Log("Check for ceiling: " + player.CheckForCeiling());
                stateMachine.ChangeState(player.InAirState);
            }
        }
        else if (!isAbilityDone)
        {
            attackDetails.damageAmount = playerData.kickDamage;
            attackDetails.hitCollisionPosition = player.kickCheck.position;
            attackDetails.stunDamageAmount = 10;
            attackDetails.attackerFacingDirection = player.FacingDirection;
            Collider2D[] detectedObjects = player.CheckIfSlideKickbox();

            foreach (Collider2D collider2D in detectedObjects)
            {
                if (collider2D.transform.parent.CompareTag("Enemy") || collider2D.transform.parent.CompareTag("Kickable"))
                {
                    collider2D.transform.parent.SendMessage("Damage", attackDetails);
                }

            }
        }
    }

    public bool CheckIfCanSlideKick()
    {
        return Time.time >= lastSlideKickTime + playerData.slideKickCooldown;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 hitCollisionPosition;
    public int damageAmount,
        stunDamageAmount,
        attackerFacingDirection;

    public AttackDetails(Vector2 hitCollisionPosition, int damageAmount, int stunDamageAmount, int attackerFacingDirection)
    {
        this.hitCollisionPosition = hitCollisionPosition;
        this.damageAmount = damageAmount;
        this.stunDamageAmount = stunDamageAmount;
        this.attackerFacingDirection = attackerFacingDirection;
    }
}

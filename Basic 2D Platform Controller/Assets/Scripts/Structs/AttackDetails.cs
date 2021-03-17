using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 hitCollisionPosition;
    public int damageAmount,
        stunDamageAmount,
        attackerFacingDirection;
}

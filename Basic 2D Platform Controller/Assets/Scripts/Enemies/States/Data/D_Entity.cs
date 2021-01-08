using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEntityData", menuName ="Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f,
        ledgeCheckDistance = 0.4f,
        maxAggroDistance = 4f,
        minAggroDistance = 3f,
        closeRangeActionDistance = 1f,
        maxHealth = 30f,
        damageHopSpeed = 3f,
        groundCheckRadius = 0.3f,
        stunResistance = 3f,
        stunRecoveryTime = 2f;

    public LayerMask whatIsGround, whatIsWall, whatIsPlayer;

    public GameObject hitParticle;
}

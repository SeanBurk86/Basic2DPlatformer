using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    public int startingHealth = 5;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1.0f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public GameObject ghost;
    public float ghostDelaySeconds = 0.1f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5.0f;
    public float crouchColliderHeight = 0.65f;
    public float crouchColliderYOffset = 0.35f;
    public float standColliderHeight = 1.3f;
    public float standColliderYOffset = 0.75f;
    public float crouchSquishCheckScale = 0.5f;
    public float crouchSquishCheckPosition = 0.25f;
    public float standSquishCheckScale = 1f;
    public float standSquishCheckPosition = 0f;

    [Header("Check Variables")]
    public float ceilingCheckRadius = 0.3f;
    public float ceilingCheckSizeX = 0.5f;
    public float ceilingCheckSizeY = 0.3f;
    public float groundChecksRadius = 0.2f;
    public float wallCheckDistance = 0.5f;
    public float ledgeCheckDistance = 0.75f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsCeiling;

    [Header("Combat Variables")]
    public float kickCheckRadius = 0.3f;
    public float kickThrustVelocity = 15f;
    public float kickCooldown = 0.75f;
    public int kickDamage = 100;
    public int amountOfKicks = 1;
    public float slideKickCheckRadius = 0.3f;
    public float slideKickThrustVelocity = 15f;
    public float slideKickCooldown = 0.5f;

    [Header("Roll Variables")]
    public float rollThrustVelocity = 15f;
    public int amountOfRolls = 1;
}

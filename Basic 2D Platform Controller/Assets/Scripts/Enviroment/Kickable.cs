﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickable : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D RB;

    [SerializeField]
    private PainfulKickable painfulKickable;

    [SerializeField]
    private int baseHealth = 200;

    private int currentHealth;
    private Vector2 currentVelocity;

    private void Start()
    {
        painfulKickable.canHurt = false;
        currentHealth = baseHealth;
    }

    private void Update()
    {
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        currentVelocity = RB.velocity;
        if(Mathf.Abs(currentVelocity.x) > 14.9f || Mathf.Abs(currentVelocity.y) > 14.9f)
        {
            painfulKickable.canHurt = true;
        }
        else
        {
            painfulKickable.canHurt = false;
        }
        
    }

    public void MakeFriction()
    {
        RB.sharedMaterial.friction = 1;
    }

    public void MakeFrictionless()
    {
        RB.sharedMaterial.friction = 0;
    }

    public void Damage(AttackDetails attackDetails)
    {
        RB.AddForce(new Vector2(attackDetails.attackerFacingDirection,0.25f) * 4, ForceMode2D.Impulse);
        currentHealth -= attackDetails.damageAmount;
    }

}

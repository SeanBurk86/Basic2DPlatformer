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

    public bool indestructible = false;

    private void Start()
    {
        painfulKickable.canHurt = false;
        currentHealth = baseHealth;
    }

    private void Update()
    {
        
        currentVelocity = RB.velocity;
        if(Mathf.Abs(currentVelocity.x) > 14.9f || Mathf.Abs(currentVelocity.y) > 14.9f)
        {
            painfulKickable.canHurt = true;
        }
        else
        {
            painfulKickable.canHurt = false;
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if(transform.parent == null)
        {
            Released();
        }
        
    }

    public void Damage(AttackDetails attackDetails)
    {
        Released();
        RB.AddForce(new Vector2(attackDetails.attackerFacingDirection,0.375f) * 4, ForceMode2D.Impulse);
        if (!indestructible)
        {
            currentHealth -= attackDetails.damageAmount;
        }
        
    }

    public void Released()
    {
        RB.gravityScale = 4.5f;
    }
}

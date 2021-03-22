using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    protected float BulletForce;
    protected float TimeBetweenFiring;
    protected float ProjectileLifetime;
    protected float spread;
    protected int numberOfBullets;
    protected int BulletDamage;
    protected int BulletStunDamage;
    Collider2D DamageCollider;

    public virtual void Shoot()
    {

    }

}

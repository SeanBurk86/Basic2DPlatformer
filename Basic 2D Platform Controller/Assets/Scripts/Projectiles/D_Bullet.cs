using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newBulletData", menuName = "Data/Bullet Data/Base Data")]
public class D_Bullet : ScriptableObject
{
    public bool destroyOnImpact,
        permanentInstantiation;

    public float bulletForce,
        timeBetweenFiring, 
        projectileLifetime,
        spreadAngle,
        damageRadius,
        gravityScale;

    public int numberOfBullets,
        bulletDamage,
        bulletStunDamage;

    public LayerMask whatIsCollidable;

    public string[] damageableTags;
}

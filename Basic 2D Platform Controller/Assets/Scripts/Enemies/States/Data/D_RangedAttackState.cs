using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State Data")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public int projectileDamage = 1;
    public float projectileSpeed = 12f,
        projectileTravelDistance = 5f;

}

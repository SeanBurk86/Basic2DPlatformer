using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State Data")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3f,
        stuneKnockbackTime = 0.2f,
        stunKnockbackSpeed = 20f;

    public Vector2 stunKnockbackAngle;
}

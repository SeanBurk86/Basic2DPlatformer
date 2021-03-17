using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainfulKickable : MonoBehaviour
{
    public bool canHurt;
    private AttackDetails attackDetails;

    private void Start()
    {
        attackDetails.damageAmount = 50;
        attackDetails.stunDamageAmount = 10;
    }

    private void Update()
    {
        attackDetails.hitCollisionPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(canHurt)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.transform.parent.SendMessage("Damage", attackDetails);
            }
        }
        
    }
}

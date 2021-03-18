using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    AttackDetails attackDetails;

    private void Start()
    {
        attackDetails.damageAmount = 5;
        attackDetails.hitCollisionPosition = transform.position;
        attackDetails.stunDamageAmount = 10;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.GetComponent<Player>().CheckIfDamage(attackDetails);
        }
        Debug.Log("Hazard trigger " + collision.ToString());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBullet : MonoBehaviour
{
    public float projectileLifeTime;
    public int bulletDamage = 5;

    private AttackDetails attackDetails;
    public Vector3 moveVector { get; private set; }

    private void Start()
    {
        attackDetails.damageAmount = bulletDamage;
        attackDetails.hitCollisionPosition = transform.position;
        attackDetails.stunDamageAmount = 10;
        StartCoroutine(DestroyProjectile());
    }
    private void FixedUpdate()
    {
        attackDetails.hitCollisionPosition = transform.position;
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.parent.SendMessage("Damage", attackDetails);
        }
        
        Destroy(gameObject);
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(gameObject);
    }
}

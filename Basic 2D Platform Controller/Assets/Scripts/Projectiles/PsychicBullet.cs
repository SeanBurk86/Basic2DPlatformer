using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBullet : MonoBehaviour
{
    public float projectileLifeTime;
    public int bulletDamage = 5;
    public int stunDamage = 10;
    public float BulletForce = 20f;
    public float timeBetweenFiring = .0625f;

    private AttackDetails attackDetails;
    public Vector3 moveVector { get; private set; }

    private void Start()
    {
        attackDetails.damageAmount = bulletDamage;
        attackDetails.hitCollisionPosition = transform.position;
        attackDetails.stunDamageAmount = stunDamage;
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

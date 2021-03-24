using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBullet : MonoBehaviour
{
    public int bulletDamage = 5;
    public int stunDamage = 10;
    public float BulletForce = 20f;
    public float timeBetweenFiring = .0625f;
    public float spread = 30f;
    public float projectileLifeTime = 2f;
    public int numberOfBullets = 7;

    private AttackDetails attackDetails;

    private void Start()
    {
        attackDetails.damageAmount = bulletDamage;
        attackDetails.stunDamageAmount = stunDamage;
        StartCoroutine(DestroyProjectile());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackDetails.hitCollisionPosition = transform.position;
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Kickable"))
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

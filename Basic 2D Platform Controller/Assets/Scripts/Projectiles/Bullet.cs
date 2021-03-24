using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public D_Bullet bulletData;
    public AttackDetails attackDetails;
    public Rigidbody2D RB;

    private void Start()
    {
        attackDetails.stunDamageAmount = bulletData.bulletStunDamage;
        attackDetails.damageAmount = bulletData.bulletDamage;
        RB.gravityScale = bulletData.gravityScale;
        if (!bulletData.permanentInstantiation)
        {
            StartCoroutine(DestroyProjectile());
        }
    }

    public virtual void Impact(Collider2D collider2D)
    {
        attackDetails.hitCollisionPosition = transform.position;
        foreach (string tag in bulletData.damageableTags)
        {
            if (collider2D.gameObject.CompareTag(tag))
            {
                collider2D.transform.parent.SendMessage("Damage", attackDetails);
            }
        }
        if (bulletData.destroyOnImpact)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Impact(collision);
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(bulletData.projectileLifetime);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{

    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    private PhysicsMaterial2D previousMaterial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            collision.transform.parent.SetParent(transform.parent.transform);
            collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            previousMaterial = collision.transform.GetComponent<Rigidbody2D>().sharedMaterial;
            collision.transform.GetComponent<Rigidbody2D>().sharedMaterial = fullFriction;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            collision.transform.GetComponent<Rigidbody2D>().sharedMaterial = previousMaterial;
            collision.transform.parent.SetParent(null);
        }
    }
}

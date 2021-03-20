using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            collision.transform.parent.SetParent(transform.parent.transform);
            collision.transform.GetComponent<Rigidbody2D>().sharedMaterial.friction = 100f;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            collision.transform.parent.SetParent(null);
        }
    }
}

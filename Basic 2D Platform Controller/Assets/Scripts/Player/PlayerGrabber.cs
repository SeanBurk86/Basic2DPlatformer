using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Kickable"))
        {
            collision.transform.parent.SetParent(transform.parent.transform);
            circleCollider2D.radius = 0.0001f;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Kickable"))
        {
            circleCollider2D.radius = 0.3f;
            collision.transform.parent.SetParent(null);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    [SerializeField]
    private Transform boxHoldPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Kickable"))
        {
            collision.transform.parent.SetParent(transform.parent.transform);
            //collision.transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Kickable"))
        {
            //collision.transform.GetComponent<Rigidbody2D>().gravityScale = 4.5f;
            collision.transform.parent.SetParent(null);
        }
        
    }
}

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
            collision.transform.parent.SendMessage("Grabbed");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            Debug.Log("Collision exit name: " + collision.transform.parent.name);
            collision.transform.parent.SendMessage("Released");
            collision.transform.parent.SetParent(null);
        }
    }
}

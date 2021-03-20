using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishCheck : MonoBehaviour
{
    public bool isSquished { get; private set; }

    private void Start()
    {
        isSquished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Moving Platform") || collision.transform.CompareTag("Ground"))
        {
            isSquished = true;
            Debug.Log(gameObject.name + " isSquished: " + isSquished);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Moving Platform") || collision.transform.CompareTag("Ground"))
        {
            isSquished = false;
            Debug.Log(gameObject.name + " isSquished: " + isSquished);
        }
        
    }
}

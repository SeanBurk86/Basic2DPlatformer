using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 position1;
    public Vector3 position2;
    public float platformMovementSpeed = .25f;
    public Rigidbody2D RB { get; private set; }

    private void Start()
    {
        position1 = transform.position;
        RB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * platformMovementSpeed, 1.0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, position2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player entering moving plat collision");
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player exiting moving plat collision");
            collision.collider.transform.SetParent(null);
        }
    }

}

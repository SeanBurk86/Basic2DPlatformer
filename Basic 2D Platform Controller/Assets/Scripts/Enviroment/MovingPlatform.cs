using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 position1,
        position2;
    public float platformMovementSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * platformMovementSpeed, 1.0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1, position2);
    }
}

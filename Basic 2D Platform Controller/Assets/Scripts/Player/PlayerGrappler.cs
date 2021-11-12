using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerGrappler : MonoBehaviour
{
    [SerializeField] public LineRenderer grappleLineRenderer;
    [SerializeField] public SpringJoint2D grappleSpringJoint2D;

    private void OnEnable()
    {
        grappleLineRenderer.enabled = true;
        grappleSpringJoint2D.enabled = true;
    }

    private void OnDisable()
    {
        grappleLineRenderer.enabled = false;
        grappleSpringJoint2D.enabled = false;
    }

    private void Update()
    {
        grappleLineRenderer.SetPosition(0, transform.parent.transform.position);
        grappleLineRenderer.SetPosition(1, grappleSpringJoint2D.connectedAnchor);
    }

    public void SetConnectedAnchor(Vector2 connectedAnchor)
    {
        grappleSpringJoint2D.connectedAnchor = connectedAnchor;
    }

    public void SetAnchor(Vector2 anchor)
    {
        grappleSpringJoint2D.anchor = anchor;
    }
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Switch"))
        {
            collision.transform.SendMessage("UseSwitch");
        }
        else if (collision.transform.CompareTag("BulletPickup"))
        {
            collision.transform.SendMessage("Grab", GetComponentInParent<Player>());
        }


    }
}

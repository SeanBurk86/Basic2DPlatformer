using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public Bullet payload;

    public void Grab(Player player)
    {
        player.SendMessage("Pickup", payload);
        Destroy(gameObject);
    }
}

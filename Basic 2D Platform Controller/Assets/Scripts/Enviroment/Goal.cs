using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private string goalSoundName = "goal";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.GetComponent<Kickable>().isGameBall)
        {
            Destroy(collision.transform.parent.gameObject);
            Debug.Log("Goal");
            FindObjectOfType<AudioManager>().Play(goalSoundName);
        }
    }

}

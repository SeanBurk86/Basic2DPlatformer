using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public void UseSwitch()
    {
        Debug.Log("Switch being used");
        FindObjectOfType<AudioManager>().Play("switch");
    }
}

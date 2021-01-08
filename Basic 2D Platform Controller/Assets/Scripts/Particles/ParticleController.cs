using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private void FinishAnim()
    {
        if(gameObject)
        {
            Destroy(gameObject);
        }
        
    }
}

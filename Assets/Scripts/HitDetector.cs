using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public static string hitName = null;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        
        if (hitObject.tag.Equals("Obstacle"))
        {
            GetComponentInParent<PlayerController>().GetHit();
        }
    }
}

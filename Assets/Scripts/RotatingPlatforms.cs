using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatforms : MonoBehaviour
{
    public float turnSpeed;
    public int turnAxis;

    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * turnAxis * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.rotation = Quaternion.Euler(collision.gameObject.transform.eulerAngles.x, collision.gameObject.transform.eulerAngles.y, 0);
            collision.transform.parent = null;
        }
    }
}

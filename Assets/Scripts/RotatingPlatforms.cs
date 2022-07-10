using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatforms : MonoBehaviour
{
    public float turnSpeed;
    public int turnAxis;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * turnAxis * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        Debug.Log(gameObject);
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.parent = null;
        }
    }
}

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
    private void OnCollisionStay(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        Debug.Log(gameObject);
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * turnSpeed * turnAxis * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}

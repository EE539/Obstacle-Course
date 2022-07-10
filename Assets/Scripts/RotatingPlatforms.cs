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
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<PlayerController>().horizontalMovement = gameObject.GetComponent<PlayerController>().horizontalMovement - (turnSpeed * turnAxis);
        }
    }
}

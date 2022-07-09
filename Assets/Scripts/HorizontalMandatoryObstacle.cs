using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMandatoryObstacle : MonoBehaviour
{
    public float turnSpeed, moveSpeed;
    [SerializeField] private float minBorder = -0.933f, maxBorder = 0.933f;

    void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime, Space.Self);
        if(transform.position.x < minBorder)
        {
            moveSpeed *= -1;
            transform.position = new Vector3(minBorder, transform.position.y, transform.position.z);
        }
            
        if (transform.position.x >= maxBorder)
        {
            moveSpeed *= -1;
            transform.position = new Vector3(maxBorder, transform.position.y, transform.position.z);
        }

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
    }
}

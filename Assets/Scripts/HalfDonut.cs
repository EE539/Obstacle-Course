using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    public float timer = 0f, speedOfDonut = 5f;
    [SerializeField]private float minBorder = -0.11f, maxBorder;

    private void Start()
    {
        maxBorder = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 7)
        {
            MoveDonut();
        }
    }

    void MoveDonut()
    {
        Debug.Log("DONUUUUUUUUUUUUUUUT");
        if (transform.localPosition.x <= minBorder)
        {
            speedOfDonut /=-2f;
            transform.localPosition = new Vector3(minBorder, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x > maxBorder)
        {
            speedOfDonut *= -2f;
            transform.localPosition = new Vector3(maxBorder, transform.localPosition.y, transform.localPosition.z);
            timer = 0;
            return;
        }
            
        transform.Translate(Vector3.right * -speedOfDonut * Time.deltaTime, Space.World);
    }
}

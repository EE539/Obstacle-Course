using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finisher : MonoBehaviour
{
    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision Detected in finisher");
            gameObject.GetComponent<PlayerController>().m_painting = true;
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            myBrain.m_DefaultBlend = someCustomBlend;
            newCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);
        }
    }
}






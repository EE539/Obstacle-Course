using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Success : MonoBehaviour
{
    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;

    private bool success;
    private int winNum = 0;

    // Update is called once per frame
    void Update()
    {
        success = GetComponent<PaintWall>().paintFinish;
        if (success && winNum == 0)
        {
            int winNum = Random.Range(1, 6);
            myBrain.m_DefaultBlend = someCustomBlend;
            newCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_Animator.SetTrigger("win"+winNum);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class Success : MonoBehaviour
{
    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;
    
    public Text congratsTxt;

    private bool success;
    private int winNum = 0;

    void Update()
    {
        success = GetComponent<PaintWall>().paintFinish;
        if (success && winNum == 0)
        {
            congratsTxt.gameObject.SetActive(true);
            int winNum = Random.Range(1, 6);
            myBrain.m_DefaultBlend = someCustomBlend;
            newCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_Animator.SetTrigger("win"+winNum);
            Invoke("RestartLevel", 8);
        }
    }

    void RestartLevel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Get the index of the scene
        SceneManager.LoadScene(currentSceneIndex);
    }

}

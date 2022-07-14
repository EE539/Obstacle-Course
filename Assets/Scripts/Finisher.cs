using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;


public class Finisher : MonoBehaviour
{
    public CinemachineBlendDefinition someCustomBlend;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera newCamera;
    public CinemachineBrain myBrain;

    public Text failTxt;
    [HideInInspector] public static int counter = 0;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            
            Debug.Log("Collision Detected in finisher");
            gameObject.GetComponent<PlayerController>().m_painting = true;
            counter = 0;
            
        }
        if (gameObject.tag.Equals("Enemy"))
        {
            counter++;
        }
        if(counter == 5)
        {
            int failAnimator = Random.Range(1, 3);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_Animator.SetBool("Fail", true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_Animator.SetBool("Finished", true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().m_Animator.SetTrigger("fail" + failAnimator);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
            failTxt.gameObject.SetActive(true);
            Invoke("RestartLevel", 8);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            float playerSpeed = gameObject.GetComponent<PlayerController>().speed * Time.deltaTime;
            gameObject.transform.Translate(Vector3.forward * playerSpeed);

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        Transform paintWall = GameObject.FindGameObjectWithTag("Painter").transform;
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<PlayerController>().m_Finish = true;
            myBrain.m_DefaultBlend = someCustomBlend;
            newCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Painter").GetComponent<PaintWall>().enabled = true;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyMovement>().nav.isStopped = true;
            }
        }
        
    }
    void RestartLevel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        counter = 0;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Get the index of the scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}






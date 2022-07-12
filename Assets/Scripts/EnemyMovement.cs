using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Animator enemy_Animatior;
    private GameObject player, finishLine;
    private NavMeshAgent nav;
    private float moveSpeed, rotateSpeed;

    private void Awake()
    {
        enemy_Animatior = GetComponent<Animator>();
        int chooseAnimation = 0;
        chooseAnimation = Random.Range(1,4);
        if(chooseAnimation != 4)
        {
            enemy_Animatior.SetTrigger("wait"+chooseAnimation);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        finishLine = GameObject.FindWithTag("Finish");
        nav = transform.GetComponent<NavMeshAgent>();
        Debug.Log(nav);
        
        //moveSpeed = player.GetComponent<PlayerController>().speed;
    }
    // Update is called once per frame
    void Update()
    {
        int zAxis = 0;
        float time = 0;
        if (player.GetComponent<PlayerController>().start)
        {
            if (zAxis == Mathf.FloorToInt(transform.position.z))
            {
                time += Time.deltaTime;
                if (time > 3)    
                    enemy_Animatior.SetTrigger("jump");
            }
            else
            {
                zAxis = (int)transform.position.z;
                time = 0;
            }
            enemy_Animatior.SetBool("start running", true);
            nav.SetDestination(finishLine.transform.position);
            zAxis = Mathf.FloorToInt(transform.position.z);
        }
    }
}

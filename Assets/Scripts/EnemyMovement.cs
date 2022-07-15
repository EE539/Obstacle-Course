using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public Animator enemy_Animatior;
    private GameObject player, finishLine;
    [HideInInspector] public NavMeshAgent nav;
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
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        finishLine = GameObject.FindWithTag("Finish");
        nav = transform.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float dist = Vector3.Distance(transform.position, nav.destination);
        if (player.GetComponent<PlayerController>().start)
        {
            nav.SetDestination(finishLine.transform.position);
            if (dist > 0)
            {
                enemy_Animatior.SetBool("start running", true);
            }
            else
            {
                enemy_Animatior.SetBool("start running", false);
                int chooseEnd = Random.Range(1, 4);
                enemy_Animatior.SetTrigger("win" + chooseEnd);

            }

        }
    }
}

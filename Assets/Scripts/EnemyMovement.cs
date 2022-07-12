using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator enemy_Animatior;
    private GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().start)
        {
            enemy_Animatior.SetBool("start running", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject> (); //element 0 is our player ;)
    public Text rankText;
    void Start()
    {
        FindRank();
    }

    // Update is called once per frame
    void Update()
    {
        FindRank();
    }

    void FindRank()
    {
        int currentRank = 1;
        for(int count = 1; count < players.Count; count++)
        {
            if (players[0].transform.position.z <= players[count].transform.position.z)
                currentRank++;
        }
        rankText.text = "Rank: " + currentRank.ToString();
    }
}

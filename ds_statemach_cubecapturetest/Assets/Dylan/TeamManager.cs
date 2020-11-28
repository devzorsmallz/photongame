using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamManager : MonoBehaviour
{
    public GameObject blueTeamBot;
    public GameObject redTeamBot;

    // Start is called before the first frame update
    void Start()
    {
        //Uncomment the next line to spawn players based on how many are connected to the network
        //int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //Delete next line, it's just for testing
        int playerCount = 4;

        for (int i = 0; i < playerCount; i++)
        {
            if (i == 0)
            {
                Instantiate(blueTeamBot, transform.Find("Spawn Points").GetChild(0).position, Quaternion.identity, transform.Find("Blue Team"));
            }
            if (i == 1)
            {
                Instantiate(redTeamBot, transform.Find("Spawn Points").GetChild(1).position, Quaternion.identity, transform.Find("Red Team"));
            }
            if (i == 2)
            {
                Instantiate(blueTeamBot, transform.Find("Spawn Points").GetChild(2).position, Quaternion.identity, transform.Find("Blue Team"));
            }
            if (i == 3)
            {
                Instantiate(redTeamBot, transform.Find("Spawn Points").GetChild(3).position, Quaternion.identity, transform.Find("Red Team"));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

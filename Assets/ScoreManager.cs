using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour
{

    public SyncListString playerIds;
    public SyncListInt playerScores;
    int roundNumber;
    void Awake()
    {
        roundNumber = 0;
        DontDestroyOnLoad(transform.gameObject); // set to dont destroy
    }


    [Command]
    public void CmdIncreaseScoreByOne(GameObject player) {

        player.GetComponent<PlayerStatus>().CmdIncreaseScoreByOne();
        int playerIndex = playerIds.IndexOf(player.GetComponent<NetworkIdentity>().netId.ToString());
        playerScores[playerIndex] = playerScores[playerIndex] + 1;
        ///
    }



    [Command]
    public void CmdRegisterNewPlayer(GameObject player)
    {
        playerIds.Add(player.GetComponent<NetworkIdentity>().netId.ToString());
        playerScores.Add(0);

    }

    public bool isGameCreator(GameObject player) {
        string playerId = player.GetComponent<NetworkIdentity>().netId.ToString();
        return playerIds.IndexOf(playerId) == 0;
    }


    public Dictionary<string, int> GetDict()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Dictionary<string, int> dict = new Dictionary<string, int>();

        foreach (GameObject player in players)
        {
            string id = player.GetComponent<NetworkIdentity>().netId.ToString();
            int score = player.GetComponent<PlayerStatus>().score;
            dict.Add(id,score);
        }
        return dict;
    }

    
}

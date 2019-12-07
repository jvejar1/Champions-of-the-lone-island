using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStatus : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar] public int score = 0;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject); // set to dont destroy

    }
    [Command]
    public void CmdIncreaseScoreByOne()
    {
        score++;
    }

    int getScore()
    {
        return score;
    }

    // Update is called once per frame
    
}

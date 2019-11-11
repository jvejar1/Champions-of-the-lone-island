using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using ScoringSystem;
public class ShowScoring : NetworkBehaviour
{
    // Start is called before the first frame update

    bool showingScoringScene = false;
    void Start()
    {
        CmdAddScoreToTable();
    }


    [Command]
    void CmdAddScoreToTable()
    {

        Score.scores.Add(GetComponent<NetworkIdentity>().netId.ToString(), 0);

    }

    void displayScoreTitle() {
        
       
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool isPressingTriggerHotkey = Input.GetKeyDown(KeyCode.Tab);

        if (isPressingTriggerHotkey)
        {
            Debug.Log("Tab pressed");
            if (showingScoringScene)
            {
                SceneManager.UnloadScene("Scoring");
                showingScoringScene = false;
            }
            else
            {
                SceneManager.LoadScene("Scoring", LoadSceneMode.Additive);
                showingScoringScene = true;

            }


        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneManagement : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject gameController = GameObject.FindWithTag("GameController");
        ScoreManager scoreManager = gameController.GetComponent<ScoreManager>();

        bool isGameCreator = scoreManager.isGameCreator(this.gameObject);
        Debug.Log("is game creator: " + isGameCreator);
        if (isGameCreator && isLocalPlayer)
        {

            //Load the menu for scene selection
            SceneManager.LoadScene("SceneSelection", LoadSceneMode.Additive);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SceneSelectionConfiguration : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject localPlayer;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("Encountered Players: " + players.Length);
        foreach (GameObject player in players)
        {

            if (player.GetComponent<NetworkBehaviour>().isLocalPlayer)
            {
                localPlayer = player;
                break;
            }

        }

        // set the player to inmovil
        localPlayer.SetActive(false);
    }

    void OnDestroy()
    {
        Debug.Log("Destroy SceneSelection");

       //set player to movil
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

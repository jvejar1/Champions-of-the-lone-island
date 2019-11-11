using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ScoringSystem;
public class PlayerResources : NetworkBehaviour
{
    [SyncVar] public float playerHP;
    [SyncVar] public float playerAP;
    [SyncVar] public float playerSP;

    public float playerMaxHP = 10;
    public float playerMaxAP = 10;
    public float playerMaxSP = 10;


    [SyncVar] public bool isDead = false;

    public GameObject PlayerUI;

    public Slider HPBar;
    public Slider APBar;
    public Slider SPBar;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerMaxHP;
        playerAP = playerMaxAP;
        playerSP = playerMaxSP;

        //SceneManager.UnloadScene("Forest");

        if (isLocalPlayer)
        {
            PlayerUI.SetActive(true);

            InvokeRepeating("CmdAPRegen", 1f, 1f);  //1s delay, repeat every 1s
            InvokeRepeating("CmdSPRegen", 3f, 3f);  //3s delay, repeat every 3s

            HPBar.value = CalculateHPPct();
            APBar.value = CalculateAPPct();
            SPBar.value = CalculateSPPct();

        }
    }

    // Update is called once per frame
    void Update()
    {


        

        if (isLocalPlayer)
        {
            //SceneManager.UnloadSceneAsync("Forest");

            if (playerHP <= 0)
            {
                CmdKill();
                SceneManager.LoadScene("Scoring", LoadSceneMode.Single);

            }


            HPBar.value = CalculateHPPct();
            APBar.value = CalculateAPPct();
            SPBar.value = CalculateSPPct();
        }
    }


   [Command]
    void CmdKill() {
        isDead = true;
        //animation to dead
        //get Winner
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> alivePlayers = new List<GameObject>();
        foreach (GameObject playerGameObject in playerGameObjects) {
            bool isAlive = !playerGameObject.GetComponent<PlayerResources>().isDead;
            if (isAlive)
            {
                alivePlayers.Add(playerGameObject);
            }
            Debug.Log(playerGameObject.GetComponent<PlayerResources>().isDead);
        }

        bool oneAliveLeft = alivePlayers.Count == 1;

        if (oneAliveLeft) {
            //manage the winner
            GameObject winner = alivePlayers[0];
            Score.scores[GetComponent<NetworkIdentity>().netId.ToString()] = Score.scores[GetComponent<NetworkIdentity>().netId.ToString()] + 1;

            //NetworkManager.singleton.StopHost();
            Debug.Log("have winner!");
        }

        Debug.Log("There are " + alivePlayers.Count +" alive left");


    }

    public float CalculateHPPct()
    {
        return (playerHP / playerMaxHP);
    }

    public float CalculateAPPct()
    {
        return (playerAP / playerMaxAP);
    }

    public float CalculateSPPct()
    {
        return (playerSP / playerMaxSP);
    }

    public void SpendAP(int ap)
    {
        playerAP -= ap;
    }
    public void SpendSP(int sp)
    {
        playerSP -= sp;
    }

    [Command]
    public void CmdAPRegen()
    {
        if (playerAP < playerMaxAP)
        {
            playerAP += 1;
        }
    }

    [Command]
    public void CmdSPRegen()
    {
        if (playerSP < playerMaxSP)
        {
            playerSP += 1;
        }
    }

    [Command]
    public void CmdRecieveDamage(float damage)
    {
        playerHP -= damage;
    }
}
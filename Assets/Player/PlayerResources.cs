using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerResources : NetworkBehaviour
{
    [SyncVar] public float playerHP;
    [SyncVar] public float playerAP;
    [SyncVar] public float playerSP;

    public float playerMaxHP = 10;
    public float playerMaxAP = 10;
    public float playerMaxSP = 3;

    public bool isDead = false;

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

        if (isLocalPlayer)
        {
            PlayerUI.SetActive(true);

            InvokeRepeating("CmdAPRegen", 1f, 1f);  //1s delay, repeat every 1s

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
            if (playerHP <= 0)
            {
                isDead = true;
            }

            HPBar.value = CalculateHPPct();
            APBar.value = CalculateAPPct();
            SPBar.value = CalculateSPPct();
        }
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
    public void CmdRecieveDamage(float damage)
    {
        playerHP -= damage;
    }
}
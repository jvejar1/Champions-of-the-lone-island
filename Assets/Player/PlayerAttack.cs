using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour
{
    [SyncVar] public bool collided;
    public GameObject enemy;

    [SyncVar] public float cooldown = 1;
    [SyncVar] public float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (collided)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && (cooldownTimer == 0))
                {
                    CmdDoDamage(cooldown);
                }
            }

            CmdCooldown();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided player");
            collided = true;
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Stopped Colliding");
            collided = false;
            enemy = null;
        }
    }

    [Command]
    void CmdDoDamage(float cooldown)
    {
        enemy.GetComponent<PlayerResources>().CmdRecieveDamage(1);
        this.GetComponent<PlayerResources>().SpendAP(4);
        cooldownTimer = cooldown;
    }

    [Command]
    void CmdCooldown()
    {
        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}

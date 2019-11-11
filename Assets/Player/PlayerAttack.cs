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

    [SyncVar] public float cooldown2 = 2f;
    [SyncVar] public float cooldownTimer2;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (cooldownTimer2 == 0 && anim.GetInteger("Condition") == 2)
            {
                Debug.Log("Entro!");
                anim.SetInteger("Condition", 0);
            }

            if (collided)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && (cooldownTimer == 0))
                {
                    if (anim.GetBool("Running") == false)
                    {
                        CmdDoDamage(cooldown);
                        anim.SetInteger("Condition", 2);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && (cooldownTimer == 0))
                {
                    if (anim.GetBool("Running") == false)
                    {
                        anim.SetInteger("Condition", 2);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }

            Debug.Log("Condition: " + anim.GetInteger("Condition"));

            CmdCooldown();
            CmdCooldown2();
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

    [Command]
    void CmdCooldown2()
    {
        if (cooldownTimer2 < 0)
        {
            cooldownTimer2 = 0;
        }

        if (cooldownTimer2 > 0)
        {
            cooldownTimer2 -= Time.deltaTime;
        }
    }
}

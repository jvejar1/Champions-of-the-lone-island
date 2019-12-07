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

    [SyncVar] public float cooldown2 = 2;
    [SyncVar] public float cooldownTimer2;

    Animator anim;

    public GameObject normalArrow;
    public Transform ArrowSpawn;

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
            float sp = GetComponent<PlayerResources>().playerSP;
            float ap = GetComponent<PlayerResources>().playerAP;

            //Ataque Normal
            if (cooldownTimer2 <= 0 && anim.GetInteger("Condition") == 2)
            {
                Debug.Log("Entro!");
                anim.SetInteger("Condition", 0);
            }
            if (collided)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && (cooldownTimer == 0) && ap >= 4)
                {
                    if (anim.GetBool("Running") == false)
                    {
                        CmdDoDamage(1, 1, 4);
                        anim.SetInteger("Condition", 2);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && (cooldownTimer == 0) && ap >= 4)
                {
                    if (anim.GetBool("Running") == false)
                    {
                        CmdDoDamageConsumeAP(1, 4);
                        anim.SetInteger("Condition", 2);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }

            //Ataque Especial 1
            
            if (Input.GetKeyDown(KeyCode.Mouse1) && (cooldownTimer == 0) && ap >= 6)
            {
                if (anim.GetBool("Running") == false)
                {
                    CmdFireArrow(2, 6);
                    anim.SetInteger("Condition", 3);
                    cooldownTimer2 = cooldown2;
                }
            }

            //Ataque Especial 2
            if (collided)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && (cooldownTimer == 0) && sp >= 3)
                {
                    if (anim.GetBool("Running") == false)
                    {
                        CmdDoDamageConsumeSP(0, 3);
                        CmdDoDamage(3, 3, 0);
                        anim.SetInteger("Condition", 4);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && (cooldownTimer == 0) && sp >= 3)
                {
                    if (anim.GetBool("Running") == false)
                    {
                        CmdDoDamageConsumeSP(3, 3);
                        anim.SetInteger("Condition", 4);
                        cooldownTimer2 = cooldown2;
                    }
                }
            }



            Debug.Log("Condition: " + anim.GetInteger("Condition"));

            CmdCooldown();
            CmdCooldown2();

            Debug.Log("Cooldown: " + cooldownTimer);
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
    void CmdDoDamage(float cooldown, float damage, int ap)
    {
        enemy.GetComponent<PlayerResources>().CmdRecieveDamage(damage);
        this.GetComponent<PlayerResources>().SpendAP(ap);
        cooldownTimer = cooldown;
    }

    void CmdDoDamageConsumeAP(float cooldown, int ap)
    {
        this.GetComponent<PlayerResources>().SpendAP(ap);
        cooldownTimer = cooldown;
    }

    void CmdDoDamageConsumeSP(float cooldown, int sp)
    {
        this.GetComponent<PlayerResources>().SpendSP(sp);
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
            cooldownTimer2 -= 0.1f*Time.deltaTime;
        }
    }

    [Command]
    void CmdFireArrow(float cooldown, int ap)
    {
        this.GetComponent<PlayerResources>().SpendAP(ap);
        cooldownTimer = cooldown;

        GameObject bullet = Instantiate(normalArrow, ArrowSpawn.position, ArrowSpawn.rotation);
        bullet.transform.Rotate(0, 90, 0);
        NetworkServer.Spawn(normalArrow);
    }
}

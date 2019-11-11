using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamagePlayer : NetworkBehaviour
{
    [SyncVar] public bool collided;
    [SyncVar] public float timer = 1;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collided && timer == 0)
        {
            enemy.GetComponent<PlayerResources>().CmdRecieveDamage(1);
            timer = 1;
        }

        CmdCooldown();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hazarding player");
            collided = true;
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Stopped Hazarding");
            collided = false;
            enemy = null;
        }
    }

    [Command]
    void CmdCooldown()
    {
        if (timer < 0)
        {
            timer = 0;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}

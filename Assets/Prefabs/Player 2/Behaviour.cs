using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Behaviour : NetworkBehaviour
{
    public int projSpeed = 50;

    public GameObject enemy;

    Vector3 direction;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        direction = -transform.right.normalized;
        movement = direction * projSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (movement)*Time.deltaTime;
    }

    private void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided player");
            enemy = collision.gameObject;
            CmdDoDamage(4);
        }

        Destroy(gameObject);
        CmdDestroy();
    }

    [Command]
    void CmdDoDamage(float damage)
    {
        enemy.GetComponent<PlayerResources>().CmdRecieveDamage(damage);
    }

    [Command]
    void CmdDestroy()
    {
        Destroy(gameObject);
    }
}

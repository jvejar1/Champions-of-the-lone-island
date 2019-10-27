﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Jump : NetworkBehaviour
{
    // Start is called before the first frame update
    public float jumpSpeed = 5f;//or whatever you want it to be
    public Rigidbody rb; //and again, whatever you want to call it
    public bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Collision Enter");
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {

            Debug.Log("Collision with 'Ground' tag");
            isGrounded = true;
        }


    }

  


    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {

            return;
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {

            CmdJump();
        }
        
    }


    [Command]
    void CmdJump() {
        
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        isGrounded = false;
        
    }

  
}
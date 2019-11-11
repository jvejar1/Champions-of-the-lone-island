using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerBehaviour : NetworkBehaviour
{

    public float speed;

    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        //set that the camera must follow the behaviour of the player transform
        if (isLocalPlayer) {
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.transform.position = this.transform.position - this.transform.forward * 6 + this.transform.up * 6;
            Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;

            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
        }

    }

    void Update()
    {
        if (isLocalPlayer) {
            //Movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                anim.SetBool("Running", true);
                anim.SetInteger("Condition", 1);
                CmdMove(moveHorizontal, moveVertical);
            }
            else
            {
                if (anim.GetBool("Running") == true)
                {
                    anim.SetBool("Running", false);
                    anim.SetInteger("Condition", 0);
                }
            }
        }

     
    }

    [Command]
    void CmdMove(float horizontal, float vertical) {
        
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        movement *= speed;
        movement = transform.TransformDirection(movement);
        controller.Move(movement);
    }
}

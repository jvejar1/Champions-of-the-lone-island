using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EvaderMovement : NetworkBehaviour
{
    // Start is called before the first frame update

    float evasorMovementVelocity = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {

            return;
        }
        //check input

        bool shiftPressed = Input.GetKeyDown(KeyCode.LeftShift);
        
        if (shiftPressed)
        {
            Debug.Log("Left shift pressed!");
            CmdEvaderMovement();
        }

    }

    [Command]
    void CmdEvaderMovement()
    {
        Vector3 movement = new Vector3(10.0f, 0f,0f);
        transform.Translate(movement);
    }
}

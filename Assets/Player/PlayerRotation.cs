using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerRotation : NetworkBehaviour
{
    // Start is called before the first frame update

    public float RotationSpeed = 1;
    float mouseX, mouseY=0;

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
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
       
        Debug.Log("Is Local Player " + mouseX.ToString()+","+mouseY.ToString());
        CmdRotatePlayer(mouseX, mouseY);
        
    }


    [Command]
    void CmdRotatePlayer(float mouseX, float mouseY) {

        Debug.Log("Rotating");
        this.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
       

    }
}

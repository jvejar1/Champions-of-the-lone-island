using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThirdPersonCameraController : NetworkBehaviour
{
   
    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {

        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

        CmdCamcontrol();
    }


    
    void CmdCamcontrol()
    {

        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
       

        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(mouseY, mouseX, 0);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerBehaviour : NetworkBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
        //set that the camera must follow the behaviour of the player transform
        if (isLocalPlayer) {
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.transform.position = this.transform.position - this.transform.forward * 6 + this.transform.up * 3;
            Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;
        }

    }

    void Update()
    {
        if (isLocalPlayer) {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            CmdMove(moveHorizontal, moveVertical);
        }

     
    }

    [Command]
    void CmdMove(float horizontal, float vertical) {
        
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        transform.Translate(movement);

    }
}

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

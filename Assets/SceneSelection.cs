using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
  

public class SceneSelection : MonoBehaviour
{


    public void OnClickScene()
    {


        switch (this.gameObject.name)
        {
            case "Forest":
                NetworkManager.singleton.ServerChangeScene("Forest");
                break;
            case "Vulcano":
                NetworkManager.singleton.ServerChangeScene("Vulcano");
                break;
            default:
                break;
        }

        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(currentSceneIndex);

    }
    // Start is called before the first frame update

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

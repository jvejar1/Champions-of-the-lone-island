using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class ShowScoring : NetworkBehaviour
{
    // Start is called before the first frame update

    bool showingScoringScene = false;

    public GameObject scoreCanvas;
    public GameObject cloneScoreCanvas;
    void Start()
    {
    }

    void Hide()
    {


        DestroyImmediate(cloneScoreCanvas,true );
    }
    void Show()
    {

        cloneScoreCanvas = (GameObject)Instantiate(scoreCanvas, transform.position, Quaternion.identity);

    }


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool isPressingTriggerHotkey = Input.GetKeyDown(KeyCode.Tab);

        if (isPressingTriggerHotkey)
        {
            Debug.Log("Tab pressed");
            if (showingScoringScene)
            {
                Hide();
                showingScoringScene = false;
            }
            else
            {
                Show();
                showingScoringScene = true;

            }


        }

    }
}

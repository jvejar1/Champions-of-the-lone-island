using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TableHandler : MonoBehaviour
{
    // Start is called before the first frame update


    private Transform entryContainer;
    private Transform entryTemplate;

    void Awake() {
        List<List<string>> scoringEntries = new List<List<string>>();
        GameObject gameController = GameObject.FindWithTag("GameController") as GameObject;
        if (gameController == null)
        {
            return;
        }
        ScoreManager scoreManager= gameController.GetComponent<ScoreManager>();

      
        foreach (KeyValuePair<string, int> item in scoreManager.GetDict())
        {
            List<string> scoringEntry = new List<string> {item.Key.ToString(), item.Value.ToString() };
            scoringEntries.Add(scoringEntry);
            Debug.Log("Key: "+ item.Key+ " Value: "+ item.Value);
        }
        FillTable(scoringEntries);
    }

   
    List<List<string>> ReadScoringFromFile() {
        string path = "Assets/Resources/scoring.txt";

        StreamReader reader = new StreamReader(path);
        string line = null;
        List<List<string>> scoringEntries = new List<List<string>>();

        while ( (line = reader.ReadLine()) !=null)
        {
            string[] lineSplit = line.Split(' ');
            List<string> scoringEntry = new List<string>{ lineSplit[0], lineSplit[1]};
            scoringEntries.Add(scoringEntry);

            Debug.Log(line);
        }

        return scoringEntries;
    }


    void FillTable(List<List<string>> scoringEntries)
    {
        entryContainer = transform.Find("PlayerScoreEntryContainer");
        entryTemplate = transform.Find("PlayerScoreEntryContainer/PlayerScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        float templateHeigh = 30f;

        for (int i = 0; i < scoringEntries.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeigh * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("PlayerName").GetComponent<Text>().text = scoringEntries[i][0];
            entryTransform.Find("PlayerScore").GetComponent<Text>().text = scoringEntries[i][1];

        }


    }

}

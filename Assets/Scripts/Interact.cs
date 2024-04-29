using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Character;
    [SerializeField]
    private TextMeshProUGUI Body;

    [SerializeField]
    private DialogueBase dialogue;


    private Dialogue[] allLines = null;


    [SerializeField]
    private static string URL = "";

    /*
    private void Awake()
    {
        FileDownloader.DownloadFile(URL, OnDownload);
    }

    private void OnDownload(string CSV)
    {
        var Data = ParsingCSV.ParseCSV<DialogueBase>(CSV);


        foreach (Dialogue dialogue in Resources.FindObjectsOfTypeAll<Dialogue>())
        {
            if (Data.ContainsKey(dialogue.ID))
            {
                dialogue.LoadData(Data[dialogue.ID]);
            }
        }
    }*/


    private void Start()
    {
        
    }
    public void ShowLines()
    {
        randomLine();

    }

    public void randomLine()
    {
        /*
        int RandomIndex = Random.Range(0, allLines.Length);
        Character.text = $"{allLines[RandomIndex].DialogueInfo.GetCharacter()}:";
        Body.text = $"{allLines[RandomIndex].DialogueInfo.GetBody()}";*/
    }
}

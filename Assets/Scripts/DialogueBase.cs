using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueBase: LoadCSV<DialogueBase>
{
    [SerializeField]
    private string Character;
    [SerializeField]
    private string Body;

    public DialogueBase()
    {
        Character = "";
        Body = "";
    }
    public DialogueBase(string character, string body)
    {
        Character = character;
        Body = body;
    }

    public string GetBody() => Body;
    public string GetCharacter() => Character;

    public DialogueBase FormatRow(string[] Headers, string[] Values)
    {
        Dictionary<string, int> headerValues = ParsingCSV.FormatHeader(Headers);
        
        foreach (string value in headerValues.Keys)
        {
            Debug.Log(value);
        }

        string character = Values[headerValues["Character"]];
        string body = Values[headerValues["Body"]];

        return new DialogueBase(character, body);
    }
}

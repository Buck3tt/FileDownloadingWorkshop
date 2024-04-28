File Downloader
Create By Cyrilla Walters.

For any questions or bugs email cyrilla@ladyofspaceandtime.com

How this works. 

The File DownloaderLoader.DownloadFile Pulls the file from the given link and returns the content through the action based on a string.

The ParsingCSV is just some basic ways you can use the file you downloaded.

Note: The type that you want to parse out needs to use the LoadCSV<t> interface. It also needs to have an empty constructor.

Below is an example class that works 

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
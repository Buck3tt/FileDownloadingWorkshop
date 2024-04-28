using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class FileDownloader 
{
    [Tooltip("Basic CSV Downloader, and if there is a failed request, nothing happens")]
    internal static async void DownloadFile(string url, Action<string> OnDownloadCompleted) //Need to use actions cause async funcs can only be void
    {
        Debug.Log("Loading");
        string downloadData = null; //Reference to the data that will be downloaded
        UnityWebRequest webRequest = UnityWebRequest.Get(url); //Creates Web Data

        var opperation = webRequest.SendWebRequest(); //Makes a web pull request

        while (!opperation.isDone) //Waits for the webrequest
        {
            await Task.Yield();
        }
        if (webRequest.result == UnityWebRequest.Result.Success) //Makes sure the webrequest worked
        {
            Debug.Log("Successfully Downloaded");
            
            downloadData = webRequest.downloadHandler.text; //Saves the webrequest data
        }
        else
        {
            Debug.Log("Failed " + webRequest.error);
        }
        OnDownloadCompleted(downloadData); //Loads the completed Data into action
    }

    [Tooltip("CSV Downloader that takes a fail state")]
    internal static async void DownloadFile(string url, Action<string> OnDownloadCompleted, Action OnFailedDownload)
    {
        Debug.Log("Loading");
        string downloadData = null; //Reference to the data that will be downloaded
        UnityWebRequest webRequest = UnityWebRequest.Get(url); //Creates Web Data

        var opperation = webRequest.SendWebRequest(); //Makes a web pull request

        while (!opperation.isDone) //Waits for the webrequest
        {
            await Task.Yield();
        }
        if (webRequest.result == UnityWebRequest.Result.Success) //Makes sure the webrequest worked
        {
            Debug.Log("Successfully Downloaded");

            downloadData = webRequest.downloadHandler.text; //Saves the webrequest data
        }
        else
        {
            Debug.Log("Failed " + webRequest.error);
            OnFailedDownload(); //Optional function for if the data cannot be obtained
            return;
        }
        OnDownloadCompleted(downloadData);
    }
}

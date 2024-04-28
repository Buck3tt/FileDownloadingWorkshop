using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParsingCSV 
{
    //Basic Version of the func
    [Tooltip("Basic Parsing of the CSV, Doesn't do duplice or list<T>, Uses String for key")]
    public static Dictionary<string, T> ParseCSV <T>(string csvFile) where T : LoadCSV<T>, new() 
    {
        string [] rows = csvFile.Split('\n'); //Splits the CSV File into rows
        string[] header = rows[0].Split(','); //Splits Header Row into each header

        Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

        for (int row = 1; row < rows.Length; row++)
        {
            string[] rowValues = rows[row].Split(','); //Splits the row into its values
            if (keyValuePairs.ContainsKey(rowValues[0]))
            {
                Debug.Log("Duplicate Value");
            }
            else
            {
                keyValuePairs.Add(rowValues[0], new T().FormatRow(header, rowValues));
            }
        }

        return keyValuePairs;
    }

    //OnDuplicate takes the dictionary that is being created, the name of the item, and the item itself, and returns the dictionary based on how the user wants
    [Tooltip("Parsing of the CSV, adds options for duplicate values, does not work with list<T>")]
    public static Dictionary<string, T> ParseCSV<T>(string csvFile, Func<Dictionary<string, T>,string, T, Dictionary<string,T>> OnDuplicate) where T : LoadCSV<T>, new()
    {
        string[] rows = csvFile.Split('\n'); //Splits the CSV File into rows
        string[] header = rows[0].Split(','); //Splits Header Row into each header

        Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

        for (int row = 1; row < rows.Length; row++)
        {
            string[] rowValues = rows[row].Split(','); //Splits the row into its values
            if (keyValuePairs.ContainsKey(rowValues[0]))
            {
                //Debug.Log("Duplicate Value");
                keyValuePairs = OnDuplicate(keyValuePairs, rowValues[0], new T().FormatRow(header, rowValues));
            }
            else
            {
                keyValuePairs.Add(rowValues[0], new T().FormatRow(header, rowValues));
            }
        }

        return keyValuePairs;
    }

    //Basic version of the func for a list in a dictionary
    [Tooltip("Parsing of the CSV, with List<T>, and on duplicate value it just adds to the list")]
    public static Dictionary<string, List<T>> ParseCSVList<T>(string csvFile) where T : LoadCSV<T>, new() 
    {
        
        string[] rows = csvFile.Split('\n'); //Splits the CSV File into rows
        string[] header = rows[0].Split(','); //Splits Header Row into each header

        Dictionary<string, List<T>> keyValuePairs = new Dictionary<string, List<T>>();

        for (int row = 1; row < rows.Length; row++)
        {
            string[] rowValues = rows[row].Split(','); //Splits the row into its values
            if (keyValuePairs.ContainsKey(rowValues[0]))
            {
                keyValuePairs[rowValues[0]].Add(new T().FormatRow(header, rowValues));
            }
            else
            {
                keyValuePairs.Add(rowValues[0], new List<T>() { new T().FormatRow(header, rowValues) });
            }
        }

        return keyValuePairs;
    }

    //OnDuplicate takes the current list from the duplicate value, takes the name of the duplicated key, takes the duplicated item, and returns the modified list
    [Tooltip("Parsing of the CSV, with List<T>, and Lets the user change what happens on duplicate")]
    public static Dictionary<string, List<T>> ParseCSVList<T>(string csvFile, Func<List<T>,string,T, List<T>> OnDuplicate) where T : LoadCSV<T>, new() 
    {

        string[] rows = csvFile.Split('\n'); //Splits the CSV File into rows
        string[] header = rows[0].Split(','); //Splits Header Row into each header

        Dictionary<string, List<T>> keyValuePairs = new Dictionary<string, List<T>>();

        for (int row = 1; row < rows.Length; row++)
        {
            string[] rowValues = rows[row].Split(','); //Splits the row into its values
            T value = new T();
            if (keyValuePairs.ContainsKey(rowValues[0]))
            {
                keyValuePairs[rowValues[0]] = OnDuplicate(keyValuePairs[rowValues[0]],rowValues[0],new T().FormatRow(header, rowValues));
            }
            else
            {
                keyValuePairs.Add(rowValues[0], new List<T>() { new T().FormatRow(header, rowValues) });
            }
        }

        return keyValuePairs;
    }

    public static Dictionary<string, int> FormatHeader(string[] header) //Returns the header array as a dictionary to make look up faster.
    {
        Dictionary<string, int> headerValues = new();

        for (int i = 0; i < header.Length; i++)
        {
            
            headerValues.Add(header[i].Trim(), i);
        }

        return headerValues;
    }
}

public interface LoadCSV<T> //If any of the parsing methods from this are used, this interface needs to be used on the scriptable object.
{
    public T FormatRow(string[] Headers, string[] Values);
}


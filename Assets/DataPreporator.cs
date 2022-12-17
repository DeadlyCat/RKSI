using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class DataPreporator
{
    public static string PreparateDataForGet(Dictionary<string, string> data)
    {
        string newData = "?";
        foreach (var d in data)
        {
            newData += d.Key + "=" + d.Value+"&";
        }
        newData.Remove(newData.Length - 1, 1);
        return newData;
    }

    public static List<IMultipartFormSection> PreparateDataToJsonForPost<T>(Dictionary<string,T> data)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        foreach (var d in data)
        {
            var json = JsonUtility.ToJson(d.Value);
            form.Add(new MultipartFormDataSection(d.Key,json));
        }

        return form;
    }
    public static List<IMultipartFormSection> PreparateDataForPost(Dictionary<string,string> data)
    {
        if(data != null)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            foreach (var d in data)
            {
                form.Add(new MultipartFormDataSection(d.Key,d.Value));
            }

            return form;
        }
        else
        {
            return null;
        }
       
    }
}
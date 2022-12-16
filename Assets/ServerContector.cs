using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public enum TypeRequist
{
    Get,
    Post
}

public class ServerContector : MonoBehaviour
{
    [SerializeField] private string url = "http://testserver/";

    private static ServerContector instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Dictionary<string, string> dataSend = new Dictionary<string, string>();
        dataSend.Add("qwe","ebalo");
        dataSend.Add("sa","zavali");
        SendRequist(TypeRequist.Get,dataSend);
    }

    public static void SendRequist(TypeRequist typeRequist, Dictionary<string,string> data)
    {
        switch (typeRequist)
        {
            case TypeRequist.Get:
                var getData =  instance.PreparateDataForRequistGet(data);
                instance.StartCoroutine(instance.SendGet(getData));
                break;
            case TypeRequist.Post:
                var postData = instance.PreparateDataForRequistPost(data);
                instance.StartCoroutine(instance.SendPost(postData));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(typeRequist), typeRequist, null);
        }
    }

    private string PreparateDataForRequistGet(Dictionary<string, string> data)
    {
        string newData = "?";
        foreach (var d in data)
        {
            newData += d.Key + "=" + d.Value+"&";
        }
        newData.Remove(newData.Length - 1, 1);
        return newData;
    }
    
    private List<IMultipartFormSection> PreparateDataForRequistPost(Dictionary<string,string> data)
    {
        List<IMultipartFormSection> formReg = new List<IMultipartFormSection>();
        foreach (var d in data)
        {
            formReg.Add(new MultipartFormDataSection(d.Key,d.Value));
        }

        return formReg;

    }

    private IEnumerator SendGet(string urlRequist)
    {
        UnityWebRequest requist = UnityWebRequest.Get(url+urlRequist);
        requist.SetRequestHeader("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
        yield return requist.SendWebRequest();
        Debug.Log(requist.uri);
        if (!requist.isNetworkError || !requist.isHttpError)
        {
            Debug.Log(DownloadHandlerBuffer.GetContent(requist));
        }
        else
        {
            Debug.LogError(requist.error);
        }
    }
    private IEnumerator SendPost(List<IMultipartFormSection> sendData)
    {
        UnityWebRequest requist = UnityWebRequest.Post(url,sendData);;
        requist.SetRequestHeader("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
        yield return requist.SendWebRequest();
        if (!requist.isNetworkError || !requist.isHttpError)
        {
            Debug.Log(DownloadHandlerBuffer.GetContent(requist));
        }
        else
        {
            Debug.LogError(requist.error);
        }
    }
}


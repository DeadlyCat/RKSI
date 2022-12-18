using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using UnityEngine.Events;

public enum TypeRequist
{
    Get,
    Post
}

public class ServerContector : MonoBehaviour
{
    private string _url = "https://d9f3-46-147-98-93.eu.ngrok.io/";

    private string _standartHeader =
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
    
    private static ServerContector instance;

    public static UnityEvent<UnityWebRequest> GotData = new UnityEvent<UnityWebRequest>();
    private void Awake()
    {
        instance = this;
    }

    public static IEnumerator GetRequest(string route, Action<string> callback)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(instance._url + route);
        uwr.SetRequestHeader("Token", User.JwtToken);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
        Debug.Log("second request");
        callback(uwr.downloadHandler.text);

    }


    public static UnityWebRequest SendRequist<T>(TypeRequist typeRequist, string rouite, Dictionary<string,T> dicUpload,out AnswerServer answer)
    {
        UnityWebRequest webRequest = null;
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        UploadHandler uploadHandler;
        AnswerServer answerServer = new AnswerServer();
        switch (typeRequist)
        {
            case TypeRequist.Post:
                var postData = DataPreporator.PreparateDataToJsonForPost(dicUpload);
                byte[] data = {};
                byte[] boundary = UnityWebRequest.GenerateBoundary();
                if (postData != null && (uint) postData.Count > 0U)
                {
                    data = UnityWebRequest.SerializeFormSections(postData,boundary );
                } else
                {
                    data = Encoding.UTF8.GetBytes(" ");
                }
                uploadHandler = new UploadHandlerRaw(data);
                uploadHandler.contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length);
                webRequest = new UnityWebRequest(instance._url + rouite, "POST", downloadHandler, uploadHandler);
                webRequest.SetRequestHeader("token", User.JwtToken);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(typeRequist), typeRequist, null);
        }
        
        answer = answerServer;
        instance.StartCoroutine(instance.Send(webRequest,answer));
        return webRequest;
    }
    public static UnityWebRequest SendRequist(TypeRequist typeRequist, string rouite,Dictionary<string,string> dicUpload,out AnswerServer answer)
    {
        UnityWebRequest webRequest = null;
        AnswerServer answerServer = new AnswerServer();
        switch (typeRequist)
        {
            case TypeRequist.Get:
                string uploadData = "";
                if (dicUpload != null)
                {
                    uploadData = DataPreporator.PreparateDataForGet(dicUpload);
                }
                
                webRequest = UnityWebRequest.Get(instance._url + rouite+uploadData);
                webRequest = new UnityWebRequest(instance._url + rouite +uploadData, "GET");
                webRequest.SetRequestHeader("token", User.JwtToken);
                break;
            case TypeRequist.Post:
                var postData = DataPreporator.PreparateDataForPost(dicUpload);
                webRequest = UnityWebRequest.Post(instance._url + rouite,postData);
                webRequest.SetRequestHeader("token", User.JwtToken);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(typeRequist), typeRequist, null);
        }

        answer = answerServer;
        instance.StartCoroutine(instance.Send(webRequest,answer));
        return webRequest;
    }
    private IEnumerator Send(UnityWebRequest request,AnswerServer answerServer)
    {
        
        yield return request.SendWebRequest();
        if (!request.isNetworkError || !request.isHttpError)
        {
            Debug.Log(request.url);
            GotData?.Invoke(request);
            answerServer.SetAnswer(DownloadHandlerBuffer.GetContent(request));
            
            
        }
        else
        {
            Debug.LogError(request.error);
        }
        
    }
    
}

public static class Rouites
{
    public  const string login = "";
    public  const string reg = "";
    public  const string getData = "";
    public  const string getEvents = "";
}

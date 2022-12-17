using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public enum TypeRequist
{
    Get,
    Post
}

public class ServerContector : MonoBehaviour
{
    private string _url = "http://testserver";

    private string _standartHeader =
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
    
    private static ServerContector instance;
    
    private void Awake()
    {
        instance = this;
    }

    public static void SendRequist<T>(TypeRequist typeRequist, Dictionary<string,T> dicUpload,out AnswerServer answer)
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
                webRequest = new UnityWebRequest(instance._url, "POST", downloadHandler, uploadHandler);
                webRequest.SetRequestHeader("token", User.jwtToken);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(typeRequist), typeRequist, null);
        }
        
        answer = answerServer;
        instance.StartCoroutine(instance.Send(webRequest,answerServer));
    }
    public static void SendRequist(TypeRequist typeRequist, Dictionary<string,string> dicUpload,out AnswerServer answer)
    {
        UnityWebRequest webRequest = null;
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        UploadHandler uploadHandler;
        AnswerServer answerServer = new AnswerServer();
        switch (typeRequist)
        {
            case TypeRequist.Get:
                var uploadData =  DataPreporator.PreparateDataForGet(dicUpload);
                
                webRequest = new UnityWebRequest(instance._url+uploadData, "GET");
                webRequest.SetRequestHeader("token", User.jwtToken);
                break;
            case TypeRequist.Post:
                var postData = DataPreporator.PreparateDataForPost(dicUpload);
                byte[] data = new byte[]{};
                byte[] boundary = UnityWebRequest.GenerateBoundary();
                if (postData != null && (uint) postData.Count > 0U)
                {
                    data  = UnityWebRequest.SerializeFormSections(postData,boundary );
                }
                else
                {
                    data = Encoding.UTF8.GetBytes("empy");
                }
                uploadHandler = new UploadHandlerRaw(data);
                uploadHandler.contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length);
                webRequest = new UnityWebRequest(instance._url, "POST", downloadHandler, uploadHandler);
                webRequest.SetRequestHeader("token", User.jwtToken);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(typeRequist), typeRequist, null);
        }
        answer = answerServer;
        instance.StartCoroutine(instance.Send(webRequest,answerServer));
    }
    private IEnumerator Send(UnityWebRequest request,AnswerServer answerServer)
    {
        
        yield return request.SendWebRequest();
        if (!request.isNetworkError || !request.isHttpError)
        {
            Debug.Log(request.url);
            answerServer.SetAnswer(request.downloadHandler.text);
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

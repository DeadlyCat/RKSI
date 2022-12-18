using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using AdvancedInputFieldPlugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginPanel : Panel
{

    [SerializeField] private AdvancedInputField loginInput;
    [SerializeField] private AdvancedInputField passwordInput;
    [SerializeField] private Button sendData;
    private AnswerServer AnswerServerLogin = new AnswerServer();
    private AnswerServer AnswerServerGetData = new AnswerServer();

    private void Awake()
    {
        
    }

    private void Start()
    {
        sendData.onClick.AddListener(() =>
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("username",loginInput.Text);
            formData.Add("password",passwordInput.Text);
            ServerContector.SendRequist(TypeRequist.Post,"login",formData,out AnswerServerLogin);
            AnswerServerLogin.Accept.AddListener((result) =>
            {
                string jwt = result;
                User.SetJwtToken(jwt);
            });
            Debug.Log("first request");
            AnswerServerLogin.Accept.AddListener((q)=>GetUserDataFromServer());
        });
    }

    private void GetUserDataFromServer()
    {
        StartCoroutine(ServerContector.GetRequest( "get-user-by-token", (result) =>
        {
           User.SetUserData(JsonConvert.DeserializeObject<UserData>(result));
        }));
        
    }

    
}



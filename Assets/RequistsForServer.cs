using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RequistsForServer
{
    public static void GetUserData()
    {
       
        UserData userDataFromServer = new UserData();
        Dictionary<string, string> data = new Dictionary<string, string>();
        ServerContector.SendRequist(TypeRequist.Post,data,out AnswerServer answerServer);
        answerServer.Accept.AddListener((result) =>
        {
            Debug.Log(result);
            //userDataFromServer = JsonUtility.FromJson<UserData>(result);
            
            
        });
    }
}

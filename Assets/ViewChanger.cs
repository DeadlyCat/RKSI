using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private List<View> _views;
    private  View showScreen = null;
    private  Dictionary<string, View> _ScreenVarians = new Dictionary<string, View>();
    private void Awake()
    {
        foreach (var view in _views)
        {
            _ScreenVarians.Add(view.Id,view);
        }
        User.SetJwtToken(PlayerPrefs.GetString("token",""));

        if (User.JwtToken != "")
        {
            ServerContector.GetRequest("get-user-by-token", (result) =>
            {
                User.SetUserData(JsonConvert.DeserializeObject<UserData>(result));
                ChangeView("events");
                
            });
            
        }
        else
        {
            ChangeView("welcome");
        }
    }

    public  void ChangeView(string id) 
    {
        if (showScreen != null)
        {
            showScreen.Hide();
        }

        if (_ScreenVarians.TryGetValue(id, out showScreen))
        {
            showScreen.Show();
        }
        else
        {
            throw new ArgumentNullException("Not found screenView with id: " + id);
        }

    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

public enum UserType
{
    Organizator,
    Speaker,
    Participant
}
[Serializable]
public struct UserData
{
    public string id;
    public string status;

    public string fio;

    public string post;
    public string organization;
    public string mail;
    [JsonProperty("is_need_sert_by_default")]
    public bool getCertificate;
    public Event[] events;

    public UserData(string id,string userType,string fio,string post,string organization, string mail,bool getCertificate,Event[] events = null )
    {
        this.id = id;
        this.status = userType;
        this.fio = fio;
        this.post = post;
        this.organization = organization;
        this.mail = mail;
        this.getCertificate = getCertificate;
        this.events = events;
    }
}
public static class User
{
    public static string JwtToken { private set; get; }
    private static UserData _data;
    public static UserData GetData()
    {
        return _data;
    }

    public static void SetUserData(UserData data)
    {
        _data = data;
    }

    public static void SetJwtToken(string jwt)
    {
        JwtToken = jwt;
        PlayerPrefs.SetString("token",JwtToken);
    }
}
[Serializable]
public class Event
{
    public string name;
}

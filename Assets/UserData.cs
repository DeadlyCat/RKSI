using System;

public enum UserType
{
    Organizator,
    Speaker,
    Participant
}
[Serializable]
public struct UserData
{
    public int userId;
    
    public UserType userType;
    
    public string name;
    public string surName;
    public string patronymic;

    public string post;
    public string organization;
    public string mail;
    public bool getCertificate;

    public UserData(int userId,UserType userType,string name,string surname,string patronymic,string post,string organization, string mail,bool getCertificate )
    {
        this.userId = userId;
        this.userType = userType;
        this.name = name;
        this.surName = surname;
        this.patronymic = patronymic;

        this.post = post;
        this.organization = organization;
        this.mail = mail;
        this.getCertificate = getCertificate;
    }
}
public static class User
{
    public static string jwtToken = "";
    private static UserData _data;
    public static UserData GetData()
    {
        return _data;
    }

    public static void SetUserData(UserData data)
    {
        _data = data;
    }
}

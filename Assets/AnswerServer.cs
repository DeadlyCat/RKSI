using UnityEngine.Events;

public class  AnswerServer
{
    private string _answer;
    public UnityEvent<string> Accept = new UnityEvent<string>();
    public void  SetAnswer(string answer)
    {
        _answer = answer;
        Accept?.Invoke(_answer);
        Accept.RemoveAllListeners();
       
    }
    public string GetAnswer()
    {
        return _answer;
    }
}
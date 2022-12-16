using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour, IScreenView
{
    [SerializeField] private string _id;
    public string Id => _id;
    
    public virtual void Show()
    {
       gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void UpdateContent()
    {
       
    }
}

public interface IScreenView
{
    public void Show();
    public void Hide();
    public void UpdateContent();
}
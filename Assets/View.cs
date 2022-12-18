using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class View : MonoBehaviour, IScreenView
{
    [SerializeField] private string _id; 
    protected PanelChanger changer;
    public string Id => _id;

    private void Awake()
    {
        changer = GetComponent<PanelChanger>();
    }

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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private List<View> _views;
    private  IScreenView showScreen = null;
    private  Dictionary<string, IScreenView> _ScreenVarians = new Dictionary<string, IScreenView>();
    private void Awake()
    {
        foreach (var view in _views)
        {
            _ScreenVarians.Add(view.Id,view.GetComponent<IScreenView>());
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

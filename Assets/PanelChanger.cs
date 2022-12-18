using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private List<Panel> panels;
    private  Panel showScreen = null;
    private Dictionary<string, Panel> panelsDic = new Dictionary<string, Panel>();

    private void Awake()
    {
        foreach (var view in panels)
        {
            panelsDic.Add(view.Id,view);
        }
    }

    public  void ChangeView(string id) 
    {
        if (showScreen != null)
        {
            showScreen.Hide();
        }

        if (panelsDic.TryGetValue(id, out showScreen))
        {
            showScreen.Show();
        }
        else
        {
            throw new ArgumentNullException("Not found panel with id: " + id);
        }

    }
}

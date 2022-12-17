using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventListItem : MonoBehaviour
{
    [SerializeField] private Image eventIcon;
    [SerializeField] private Text discriptionText;
    [SerializeField] private Text dateText;
    
    private string discription;
    private string date;
    
    private Button btnComponent;
    public RectTransform rectTransform;
    
}

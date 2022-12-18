using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsList : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private EventListItem prefab;
    private List<EventListItem> eventItems = new List<EventListItem>();

    private int countEvents;
    private void Start()
    {
        /*ServerContector.SendRequist(TypeRequist.Post,null,out AnswerServer answerServer);
        answerServer.Accept.AddListener((resault) =>
        {
            countEvents = Convert.ToInt32(resault);
            for (int i = 0; i < countEvents; i++)
            {
                CreateEventItem();
            }
        });*/
    }

    private void CreateEventItem()
    {
        var eventListItem = Instantiate(prefab,contentContainer);
        eventItems.Add(eventListItem);
        contentContainer.sizeDelta  = Vector2.up * grid.cellSize.y * eventItems.Count + Vector2.up * grid.spacing.y * (eventItems.Count - 1);
    }
}

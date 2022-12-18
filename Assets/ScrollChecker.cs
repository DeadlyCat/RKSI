using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollChecker : MonoBehaviour,IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private UnityEvent onScrollStart;
    [SerializeField] private UnityEvent onScrollEnd;

    public void OnBeginDrag(PointerEventData eventData)
    {
        onScrollStart?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onScrollEnd?.Invoke();
    }
}

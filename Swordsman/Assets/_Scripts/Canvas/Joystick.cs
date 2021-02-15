using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour , IPointerDownHandler,IDragHandler ,IPointerUpHandler
{
    private Vector2 _startHedlerPos, _currentHedlerPos;
    public void OnPointerDown(PointerEventData eventData)
    {
        _startHedlerPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currentHedlerPos = eventData.position;
        Debug.Log(_currentHedlerPos-_startHedlerPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}

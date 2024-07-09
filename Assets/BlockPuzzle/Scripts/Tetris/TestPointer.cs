using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestPointer : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }
    private void OnMouseDown()
    {
        Debug.Log("Down");
    }
    private void OnMouseUp()
    {
        Debug.Log("Up");
    }
}

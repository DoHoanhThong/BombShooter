using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UIDragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isDragable = true;
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected float returnDuration = 0.5f;
    private Transform originParent;
    private Vector3 origin;
    private RectTransform rectTransform;
    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originParent = transform.parent;
        isDragable = true;
        origin = rectTransform.anchoredPosition;
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            transform.SetParent(originParent);
            rectTransform.DOAnchorPos(origin, returnDuration);
            image.raycastTarget = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class PetFood : UIDragableObject
{

    public override void Awake()
    {
        base.Awake();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        if (isDragable)
        {           
            PetFeedController.Instance.isDraging = true;
            PetFeedController.Instance.currentFood = this;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        PetFeedController.Instance.isDraging = false;
        base.OnEndDrag(eventData);
        if (isDragable)       {
            PetFeedController.Instance.currentFood = null;
        }
    }
    public virtual void OnUsed(RectTransform mouthPos)
    {       
        //PixelCrushers.MessageSystem.SendMessage(this, "Feed", "Food");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BathShower : BathDragableObject
{
    public override void Awake()
    {
        base.Awake();
        image.rectTransform.anchoredPosition = new Vector2(1200, image.rectTransform.anchoredPosition.y);
        image.gameObject.SetActive(false);
    }
    public void GetToStartPos()
    {
        image.gameObject.SetActive(true);
        isDragable = false;
        image.rectTransform.DOAnchorPosX(600, returnDuration).OnComplete(() => { isDragable = true; });
    }
}

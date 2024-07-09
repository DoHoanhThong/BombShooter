using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class BathBrush : UIDragableObject
{
    public override void Awake()
    {
        base.Awake();
        image.rectTransform.anchoredPosition = new Vector2(1200, image.rectTransform.anchoredPosition.y);
        image.gameObject.SetActive(false);
    }
    public void FadeIn()
    {
        image.gameObject.SetActive(true);
        isDragable = false;
        image.rectTransform.DOAnchorPosX(600, returnDuration).OnComplete(() => { isDragable = true; });
    }
    public void OnTaskDone()
    {
        image.DOFade(0, returnDuration).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }
}

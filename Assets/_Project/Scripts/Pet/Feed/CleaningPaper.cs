using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
public class CleaningPaper : PetFood
{
    [SerializeField]
    private Image paperBox;
    [SerializeField]
    private float delay = 0.5f;
    [SerializeField]
    private float duration = 0.5f;

    private void OnEnable()
    {
        paperBox.transform.SetParent(transform.parent);
        RequestBase.OnRequestComplete += OnCleanDone;
    }
    private void OnDisable()
    {
        RequestBase.OnRequestComplete -= OnCleanDone;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        PetFeedController.Instance.currentFood = null;
        PetFeedController.Instance.isCleaning = false;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        PetFeedController.Instance.isCleaning = false;
               
    }
    public void OnCleanDone()
    {        
        paperBox.DOFade(0, duration).SetDelay(delay);
        image.DOFade(0, duration).SetDelay(delay).OnComplete(() => 
        {
            image.gameObject.SetActive(false);
            paperBox.gameObject.SetActive(false);
            paperBox.transform.SetParent(transform);
        });
    }

}

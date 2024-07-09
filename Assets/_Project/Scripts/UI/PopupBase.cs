using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PopupBase : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup canvas;
    [SerializeField]
    protected RectTransform popup;
    [SerializeField]
    protected float duration = 0.25f;
    [SerializeField]
    private Ease openEase = Ease.OutBack;
    [SerializeField]
    private Ease closeEase = Ease.InBack;

    protected bool isProcessing = false;

    public virtual void OpenPopup()
    {
        if (!isProcessing)
        {
            isProcessing = true;
            canvas.blocksRaycasts = false;
            popup.localScale = Vector3.zero;
            canvas.gameObject.SetActive(true);
            canvas.DOFade(1, duration).OnComplete(() => 
            {
                isProcessing = false;
                canvas.blocksRaycasts = true;
            });
            popup.DOScale(1, duration).SetEase(openEase);
        }
        
    }
    public virtual void ClosePopup()
    {
        if (!isProcessing)
        {
            isProcessing = true;
            canvas.blocksRaycasts = false;
            popup.DOScale(0, duration).SetEase(closeEase);
            canvas.DOFade(0, duration).OnComplete(() =>
            {
                canvas.gameObject.SetActive(false);
                isProcessing = false;
                canvas.blocksRaycasts = true;
            });
        }
        
    }
    public virtual void SetDefault()
    {
        canvas.gameObject.SetActive(false);
        canvas.alpha = 0;
        popup.localScale = Vector3.zero;
    }
}

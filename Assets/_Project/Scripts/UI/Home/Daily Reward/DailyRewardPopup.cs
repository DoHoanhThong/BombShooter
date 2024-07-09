using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DailyRewardPopup : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvas;
    [SerializeField]
    private float duration = 0.25f;
    public void OpenPopup()
    {
        canvas.gameObject.SetActive(true);
        canvas.DOFade(1, duration);
    }
    public void ClosePopup()
    {
        canvas.DOFade(0, duration).OnComplete(() =>
        {
            canvas.gameObject.SetActive(false);
        });
    }
}

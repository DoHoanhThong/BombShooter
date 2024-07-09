using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BathTrash : MonoBehaviour
{
    public RectTransform brushHolder;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Image[] trashs;
    [SerializeField]
    private float clearDistance = 30;
    [SerializeField]
    private float clearDuration = .5f;

    public void StartClean()
    {
        animator.SetTrigger("clean");
    }
    public void ClearTrash(int index)
    {
        trashs[index].DOFade(0, clearDuration);
        trashs[index].rectTransform.DOAnchorPosY(trashs[index].rectTransform.anchoredPosition.y - clearDistance, clearDuration).OnComplete(() => 
        {
            trashs[index].gameObject.SetActive(false);
        });
    }
    public void OnCleanDone()
    {
        //PixelCrushers.MessageSystem.SendMessage(this, "Clean", "Trash");
    }
}

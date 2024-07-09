using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BathAnimController : MonoBehaviour
{
    public RectTransform holder;
    public RectTransform holderStartPos;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ParticleSystem bubbleFX;

    [SerializeField]
    private CanvasGroup bubbleHolder;
    [SerializeField]
    private Image[] bubbleBaths;
    [SerializeField]
    private float clearDuration = .5f;
    public void SetAnimTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void OnShowerDone()
    {
        //PixelCrushers.MessageSystem.SendMessage(this, "Shower", "");
    }
    public void OnClearBubbleDone()
    {
        //PixelCrushers.MessageSystem.SendMessage(this, "Clean", "Bubble");
    }
    public void ClearBubble(int index)
    {
        foreach (var bubble in bubbleBaths[index].GetComponentsInChildren<Image>())
        {
            bubble.DOFade(0, clearDuration).OnComplete(() =>
            {
                bubble.gameObject.SetActive(false);
            });
        }
        bubbleBaths[index].DOFade(0, clearDuration).OnComplete(() =>
        {
            bubbleBaths[index].gameObject.SetActive(false);
        });

    }
    public void ShowBubble()
    {
        bubbleHolder.alpha = 0;
        bubbleHolder.gameObject.SetActive(true);
        bubbleHolder.DOFade(1, clearDuration / 2f);
        holder.anchoredPosition = holderStartPos.anchoredPosition;
    }
    public void OnShowerFX()
    {
        bubbleFX.Play();
    }
    public void OnHideShowerFX()
    {
        bubbleFX.loop = false;
    }
}

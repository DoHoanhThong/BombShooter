using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedWipeAnim : MonoBehaviour
{
    public RectTransform holder;
    public CanvasGroup crumbHolder;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int cleanCount = 3;
    private int counter = 0;
    private float currentFade = 1;

    private float duration = 0.25f;
    public void OnWipe()
    {
        animator.SetTrigger("wipe");
    }
    public void FoodCrumbsFadeOut()
    {
        counter++;
        currentFade -= 1.0f / cleanCount;   
        crumbHolder.DOFade( currentFade, duration).OnComplete(() =>
        {
            if (counter == cleanCount)
            {
                //PixelCrushers.MessageSystem.SendMessage(this, "Wipe", "Face");
                PetFeedController.OnRequestComplete?.Invoke();
                //crumbImage.gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        });
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class FoodCrumbs : MonoBehaviour
{
    [SerializeField]
    private Image crumbImage;
    [SerializeField]
    private int cleanCount = 3;
    private int counter = 0;
    private float currentFade = 1;

    private float duration = 0.25f;

    private bool isFading = false;

    public void OnSpawn()
    {
        crumbImage.color = new Color(1, 1, 1, 0);
        crumbImage.gameObject.SetActive(true);
        crumbImage.DOFade(1, duration);
    } 

    public void FadeOut()
    {
        counter++;
        currentFade -= 1.0f / cleanCount;
        isFading = true;
        crumbImage.DOColor(new Color(1, 1, 1, currentFade), duration).OnComplete(() =>
        {
            isFading = false;
            if (counter == cleanCount)
            {
               // PixelCrushers.MessageSystem.SendMessage(this, "Wipe", "Face");
                PetFeedController.OnRequestComplete?.Invoke();
                crumbImage.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        });
    }
}

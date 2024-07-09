using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TeraJet;
using UnityEngine.SceneManagement;

public class SceneTransitionHandle : Singleton<SceneTransitionHandle>
{
    [SerializeField]
    private CanvasGroup canvas;
    [SerializeField]
    private Image catImage;
    //[SerializeField]
    //private Image fillImage_1;
    //[SerializeField]
    //private Image fillImage_2;
    //[SerializeField]
    //private float fillAmount = 0.9f;
    [SerializeField]
    private float duration = 0.25f;
    [SerializeField]
    private float imageDuration = 1f;
    [SerializeField]
    private float fakeDelay = 1;
    [SerializeField]
    private Ease inEase = Ease.OutBack;
    [SerializeField]
    private Ease outEase = Ease.InBack;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(RunFade(sceneName));
    }
    IEnumerator RunFade(string sceneName)
    {
        FadeIn();
        yield return new WaitForSeconds(2);
        AsyncOperation operation = new AsyncOperation();
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        //yield return new WaitUntil(() => operation.isDone);
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(fakeDelay);
                operation.allowSceneActivation = true;
                FadeOut();
                //yield return new WaitForSeconds(imageDuration );
                
            }
            yield return null;
        }
        
        //operation.allowSceneActivation = true;
        
    }
    public void FadeIn()
    {
        canvas.alpha = 0;
        //fillImage_1.fillAmount = 0;
        //fillImage_2.fillAmount = 0;
        catImage.rectTransform.anchoredPosition = new Vector2(0, 1500);
        //fillImage_1.rectTransform.anchoredPosition = new Vector2(0, 0);
        //fillImage_2.rectTransform.anchoredPosition = new Vector2(0, 0);
        canvas.gameObject.SetActive(true);
        canvas.DOFade(1, duration).OnComplete(() => 
        {
            catImage.rectTransform.DOAnchorPosY(0, duration).SetEase(inEase).OnComplete(()=> 
            {
                //fillImage_1.DOFillAmount(fillAmount, imageDuration);
                //fillImage_2.DOFillAmount(fillAmount, imageDuration);
            });
        });
    }
    public void FadeOut()
    {
        canvas.DOFade(0, imageDuration).OnComplete(() => 
        {
            canvas.gameObject.SetActive(false);
        }).SetDelay(duration);
        catImage.rectTransform.DOAnchorPosY(1500, duration).SetEase(outEase);
        //fillImage_1.rectTransform.DOAnchorPosY(-2500, imageDuration);
        //fillImage_2.rectTransform.DOAnchorPosY(-2500, imageDuration);

    }
}

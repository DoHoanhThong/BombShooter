using System.Collections;
using System.Collections.Generic;
using TeraJet;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasSplashController : Singleton<CanvasSplashController>
{
    public bool isCanvasLoadDone;
    [SerializeField] float loadingFakeSpeed;
    [SerializeField] Image loadingFillImage;
    [SerializeField] TMP_Text progressText;
    float currentProgress = 0;
    float maxProgress = 0;
    private void Start()
    {

    }
    public IEnumerator StartLoading()
    {
        loadingFillImage.fillAmount = currentProgress;
        isCanvasLoadDone = false;
        while (currentProgress < 1)
        {
            if (currentProgress <= maxProgress)
            {
                currentProgress += Time.deltaTime * loadingFakeSpeed;
                //currentProgress = Mathf.Clamp(currentProgress, 0, maxProgress);
                loadingFillImage.fillAmount = currentProgress;
                progressText.text = ((int)(currentProgress * 100)).ToString() + "%";
            }
            yield return new WaitForEndOfFrame();
        }
        isCanvasLoadDone = true;
    }
    public void SetMaxProgress(float value)
    {
        maxProgress = value;

    }
    public void SetProgress(float value)
    {
        loadingFillImage.fillAmount = value;
        progressText.text = ((int)(value * 100)).ToString() + "%";
    }
}

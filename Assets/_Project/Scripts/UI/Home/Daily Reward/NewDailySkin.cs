using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
public class NewDailySkin : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvas;
    [SerializeField]
    private SkeletonGraphic fishAnim;
    [SerializeField]
    private Button collectBtn;
    [SerializeField]
    private float duration = 1;
    //[SerializeField]
    //private LevelMergeListData listData;

    public void OpenPanel()
    {
        //StatusBar.Instance.FadeOut();
        canvas.gameObject.SetActive(true);
        canvas.DOFade(1, duration / 4.0f);
        fishAnim.AnimationState.SetAnimation(0, "attack", true);
        fishAnim.rectTransform.DOAnchorPosX(0, duration * 1).OnComplete(()=> 
        {
            fishAnim.AnimationState.SetAnimation(0, "idle", true);
            collectBtn.transform.DOScale(1, duration / 4).SetEase(Ease.OutBack);
        });
    }
    public void ClosePanel()
    {
        //StatusBar.Instance.FadeIn();
        fishAnim.AnimationState.SetAnimation(0, "attack", true);
        fishAnim.rectTransform.DOAnchorPosX(1500, duration * 1f);
        collectBtn.transform.DOScale(0, duration / 4).SetEase(Ease.InBack);
        canvas.DOFade(0, duration).OnComplete(() => { canvas.gameObject.SetActive(false); });
    }
    public void OnCollect()
    {
        //ChooseCharacter.OnGetNewSkin?.Invoke(listData.listLevelData.Length + 1);
        ClosePanel();
    } 

}

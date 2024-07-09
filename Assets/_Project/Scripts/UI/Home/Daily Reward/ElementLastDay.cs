using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ElementLastDay : RewardElementView
{
    [SerializeField]
    private RectTransform priceObject;
    [SerializeField]
    private RectTransform newSkin;
    [SerializeField]
    private NewDailySkin newSkinPopup;

    public override void InitializeView(RewardElementData elementData, ViewState state, int index)
    {
        base.InitializeView(elementData, state, index);
        DailyRewardManager.OnCloseDaily += OnClose;
        DailyRewardManager.OnOpenDaily += OnOpen;
        if (elementData.isSkinReward)
        {
            //if (GameManager.Instance.userData.HasSkin("12"))
            //{
            //    newSkin.gameObject.SetActive(false);
            //    priceObject.gameObject.SetActive(true);
            //}
            //else
            //{
            //    priceObject.gameObject.SetActive(false);
            //    newSkin.gameObject.SetActive(true);
            //}
        }       
    }
    private void OnDestroy()
    {
        DailyRewardManager.OnCloseDaily -= OnClose;
        DailyRewardManager.OnOpenDaily -= OnOpen;
    }
    public override void OnClaimReward()
    {
        //if (_elementData.isSkinReward)
        //{
        //    if (GameManager.Instance.userData.HasSkin("12"))
        //    {
        //        base.OnClaimReward();                
        //    }
        //    else
        //    {

        //        GameManager.Instance.userData.AddSkin("12");
        //        newSkinPopup.OpenPanel();
        //    }
        //}             
    }
    private void OnClose()
    {
        newSkin.DOScale(0, 0.25f);
    }
    public void OnOpen()
    {
        newSkin.DOScale(0.45f, 0.25f);
    }
}

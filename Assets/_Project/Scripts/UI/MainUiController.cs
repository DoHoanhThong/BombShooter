using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TeraJet;
using TMPro;
using Ricimi;

public class MainUIController : Singleton<MainUIController>
{
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text heartText;
    [SerializeField]
    private PopupOpener adsCoinPopup;
    [SerializeField]
    private PopupOpener adsHeartPopup;
    [SerializeField]
    private PopupOpener minigamePopup;
    [SerializeField]
    private float textScale = 1.2f;
    [SerializeField]
    private float duration = .25f;
    private void Start()
    {
        
        coinText.text = GameManager.Instance.userData.currentCoin.ToString();
        heartText.text = GameManager.Instance.userData.currentHeart.ToString();
        GameManager.Instance.OnCoinChange += UpdateCoin;
        GameManager.Instance.OnHeartChange += UpdateHeart;
        GameManager.Instance.OnNotEnoughCoin += ShowAdsCoinPopup;
        GameManager.Instance.OnNotEnoughHeart += ShowAdsHeartPopup;
        if (DataContainer.Instance && DataContainer.Instance.playMinigame && minigamePopup)
        {
            DataContainer.Instance.playMinigame = false;
            minigamePopup.OpenPopup();
        }
        else
        {
            BackgroundMusic.Instance?.PlayLivingRoom();
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnCoinChange -= UpdateCoin;
        GameManager.Instance.OnHeartChange -= UpdateHeart;
        GameManager.Instance.OnNotEnoughCoin -= ShowAdsCoinPopup;
        GameManager.Instance.OnNotEnoughHeart -= ShowAdsHeartPopup;
    }
    private void UpdateCoin()
    {
        coinText.transform.localScale = Vector3.one * textScale;
        coinText.transform.DOScale(1, duration);
        coinText.text = GameManager.Instance.userData.currentCoin.ToString();  
    }
    private void UpdateHeart()
    {
        heartText.transform.localScale = Vector3.one * textScale;
        heartText.transform.DOScale(1, duration);
        heartText.text = GameManager.Instance.userData.currentHeart.ToString();
    }
    public void ShowAdsCoinPopup()
    {
        adsCoinPopup?.OpenPopup();
    }
    public void ShowAdsHeartPopup()
    {
        adsHeartPopup?.OpenPopup();
    }
}

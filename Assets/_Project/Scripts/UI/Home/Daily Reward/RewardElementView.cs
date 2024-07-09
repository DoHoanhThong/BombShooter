using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using HwiTera;
public class RewardElementView : MonoBehaviour //,IPointerClickHandler
{
    //huy
    [SerializeField]
    protected Text dayText;
    [SerializeField]
    protected Text coinValueText;
    [SerializeField]
    protected Image claimImage;
    [SerializeField]
    protected Image unClaimImage;
    [SerializeField]
    protected Image lockImage;
    //huy

    [SerializeField] RectTransform _claimedView;
    [SerializeField] RectTransform _unclaimedView;
    [SerializeField] RectTransform _lockView;

    RectTransform _currentView;
    protected RewardElementData _elementData;

    protected int _elemIndex;

    //[SerializeField] GameObject skinBonus;
    //[SerializeField] GameObject bagCoin;
    

    public enum ViewState
    {
        Claimed,
        UnClaimed,
        Lock,
    }

    ViewState _state = ViewState.Claimed;

    public System.Action<int> OnClaim = delegate { };

    public virtual void InitializeView(RewardElementData elementData, ViewState state, int index)
    {
        _elementData = elementData;
        _elemIndex = index;
        SetState(state);
        //huy
        dayText.text = "Day " + _elementData.day;
        //switch (_elementData.type)
        //{
        //    case RewardElementData.Type.GOLD:
        //        claimImage.sprite = _elementData.icon;
        //        unClaimImage.sprite = _elementData.icon;
        //        lockImage.sprite = _elementData.icon;
        //        coinValueText.color = new Color(1f, 192f / 255f, 5f / 255f, 1);
        //        coinValueText.text = "+" + ((double)(_elementData.coinBonus)).ToMoney();
        //        break;
        //    case RewardElementData.Type.PEARL:
        //        claimImage.sprite = _elementData.icon;
        //        unClaimImage.sprite = _elementData.icon;
        //        lockImage.sprite = _elementData.icon;
        //        coinValueText.color = new Color(241f / 255f, 82f / 255f, 190f / 255f, 1);
        //        coinValueText.text = "+" + (_elementData.coinBonus).ToString();
        //        break;
        //    case RewardElementData.Type.GIFTBOX:
        //        claimImage.sprite = _elementData.icon;
        //        unClaimImage.sprite = _elementData.icon;
        //        lockImage.sprite = _elementData.icon;
        //        coinValueText.color = Color.white;
        //        coinValueText.text = "+" + (_elementData.coinBonus).ToString();
        //        break;

        //}
        //coinValueText.text = "+" + _elementData.coinBonus.ToString();
              
        //if (_elementData.isSkinReward && GameManager.Instance.UserData.SkinBonus == "")
        //{
        //    coinValueText.text = "+" + _elementData.coinBonus.ToString();

        //    //skinBonus.Skeleton.SetSkin();
        //    skinBonus.SetActive(false);
        //    bagCoin.SetActive(true);

        //    //skinBonus.skeletonDataAsset.
        //}

    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (Vector2.Distance(eventData.pressPosition, eventData.position) < 10)
    //    {
    //        OnClick();
    //    }

    //}
    public void OnClick()
    {
        switch (_state)
        {
            case ViewState.Lock:

            case ViewState.Claimed:
                return;
            case ViewState.UnClaimed:
                ClaimReward();
                break;
        }
    }
    public void ClaimReward()
    {
        //AudioManager.Instance.PlayUIClick();

        OnClaim.Invoke(_elemIndex);

        SetState(ViewState.Claimed);

        OnClaimReward();

        //FirebaseManager.Instance?.MergeDailyFree();

    }
    public void ClaimX2Reward()
    {
        //AudioManager.Instance.PlayUIClick();
        //FirebaseManager.Instance?.MergeDailyReward();
        //GameManager.Instance.ShowRewardAds(() =>
        //{
        //    OnClaim.Invoke(_elemIndex);

        //    SetState(ViewState.Claimed);

        //    OnClaimX2Reward();

        //    FirebaseManager.Instance?.MergeDailyRewardDone();
        //}, null, "Dailyx2reward");
    }
    public virtual void OnClaimReward()
    {
        //Reward logic code here
        //huy
        if (_elementData.isSkinReward == false)
        {
            //switch (_elementData.type)
            //{
            //    case RewardElementData.Type.GOLD:
            //        //GameManager.Instance.AddDiamond(_elementData.coinBonus);
            //        CoinAnim.Instance.AddCoins(transform.position, 10, _elementData.coinBonus);
            //        break;
            //    case RewardElementData.Type.PEARL:
            //        //GameManager.Instance.AddDiamond(_elementData.coinBonus);
            //        //CoinAnim.Instance.AddDiamonds(transform.position, 10, 0);
            //        break;
            //    case RewardElementData.Type.GIFTBOX:
            //        //MergeController.Instance.AddSpinGift(_elementData.coinBonus);
            //        break;
            //}            
        }
            
        //else
        //{
        //    //Skin Bonus Popup
        //    if (GameManager.Instance.UserData.SkinBonus != "")
        //    {
        //        GameManager.Instance.AddSkin(GameManager.Instance.UserData.SkinBonus);
        //        GameManager.Instance.UserData.SkinBonus = "";
        //        GameManager.Instance.QuickSave();
        //        ShopController.Instance.UpdateShopGUI();

        //        newSkinPopup.gameObject.SetActive(true);
        //        newSkinPopup.DOFade(1, 0.25f);
        //    }
        //    else
        //    {
        //        LevelController.Instance.AddCoinToStock(_claimedView.transform.position, _elementData.coinBonus);
        //    }
        //}
        //huy
    }
    public void OnClaimX2Reward()
    {
        //Reward logic code here
        //huy
        if (_elementData.isSkinReward == false)
        {
            //switch (_elementData.type)
            //{
            //    case RewardElementData.Type.GOLD:
            //        //GameManager.Instance.AddDiamond(_elementData.coinBonus);
            //        CoinAnim.Instance.AddCoins(transform.position, 10, _elementData.coinBonus * 2);
            //        break;
            //    case RewardElementData.Type.PEARL:
            //        //GameManager.Instance.AddDiamond(_elementData.coinBonus * 2);
            //        //CoinAnim.Instance.AddDiamonds(transform.position, 10, 0);
            //        break;
            //    case RewardElementData.Type.GIFTBOX:
            //        //MergeController.Instance.AddSpinGift(_elementData.coinBonus * 2);
            //        break;
            //}
        }
            
        //if (_elementData.isSkinReward == false)
        //    LevelController.Instance.AddCoinToStock(_claimedView.transform.position, _elementData.coinBonus * 2);
        //else
        //{
        //    //Skin Bonus Popup
        //    if (GameManager.Instance.UserData.SkinBonus != "")
        //    {
        //        GameManager.Instance.AddSkin(GameManager.Instance.UserData.SkinBonus);
        //        GameManager.Instance.UserData.SkinBonus = "";
        //        GameManager.Instance.QuickSave();
        //        ShopController.Instance.UpdateShopGUI();

        //        newSkinPopup.gameObject.SetActive(true);
        //        newSkinPopup.DOFade(1, 0.25f);
        //    }
        //    else
        //    {
        //        LevelController.Instance.AddCoinToStock(_claimedView.transform.position, _elementData.coinBonus * 2);
        //    }
        //}
        //huy
    }
    public void SetState(ViewState state)
    {
        if (_state == state) return;

        _state = state;

        _currentView?.gameObject.SetActive(false);

        switch (_state)
        {
            case ViewState.Claimed:
                _currentView = _claimedView;

                break;
            case ViewState.Lock:
                _currentView = _lockView;

                break;
            case ViewState.UnClaimed:
                _currentView = _unclaimedView;

                break;
        }
        _currentView?.gameObject.SetActive(true);
    }

    //huy 
    //public void CloseNewSkinBonusPopup()
    //{
    //    //SFXManager.Instance.PlayUIClick();
    //    newSkinPopup.DOFade(0, 0.25f).OnComplete(() =>
    //    {
    //        newSkinPopup.gameObject.SetActive(false);
    //    });
    //}
}

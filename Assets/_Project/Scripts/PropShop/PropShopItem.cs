using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ricimi;

public class PropShopItem : MonoBehaviour
{
    private PropData propData;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private GameObject coinIcon;
    [SerializeField]
    private GameObject adsIcon;
    [SerializeField]
    private Image avatar;
    [SerializeField]
    private Image avatarShadow;
    [SerializeField]
    private SpriteSwapper buyBtnSwapper;
    [SerializeField]
    private PopupOpener adsPopup;
    [SerializeField]
    private PopupOpener purchasePopup;
    [SerializeField]
    private Button buyBtn;
    
    public void Initialize(PropData propData)
    {
        this.propData = propData;
        //nameText.text = propData.itemName;
        //descriptionText.text = propData.description;
        priceText.text = propData.price.ToString();
        avatar.sprite = propData.avatar;
        avatarShadow.sprite = propData.avatar;

        GameManager.Instance.OnCoinChange += UpdateBuyBtn;

    }
    private void OnDestroy()
    {
        GameManager.Instance.OnCoinChange -= UpdateBuyBtn;
    }
    public void SetLocked()
    {
        priceText.text = propData.price.ToString();
        coinIcon.SetActive(true);
        adsIcon.SetActive(false);
        buyBtn.gameObject.SetActive(true);
        UpdateBuyBtn();
    }
    public void SetUnlocked()
    {
        priceText.text = "Owned";
        coinIcon.SetActive(false);
        buyBtn.gameObject.SetActive(false);
    }
    public void SetUnlockByAds()
    {
        priceText.text = "Watch";
        coinIcon.SetActive(false);
        adsIcon.SetActive(true);
        buyBtn.gameObject.SetActive(true);
        UpdateBuyBtn();
    }
    public void OnBuyItem()
    {
        ShopCatController.Instance.currentProp = propData;
        if (propData.unlockWithAds)
        {
            // TO DO Show ads reward here
            //GameController.Instance.BuyNewProp(propData.id);
            //GameManager.Instance.AddNewProp(propData.id);
            //SetUnlocked();

            adsPopup.OpenPopup();
        }
        else
        {
            if (GameManager.Instance.userData.currentCoin >= propData.price)
            {
                //GameManager.Instance.AddCoin(-propData.price);
                //GameManager.Instance.AddNewProp(propData.id);
                //GameController.Instance.BuyNewProp(propData.id);
                //SetUnlocked();

                purchasePopup.OpenPopup();
            }
            else
            {
                //// TO DO Not enough money popup here
                GameManager.Instance.OnNotEnoughCoin?.Invoke();                
            }
        }

    }
    private void UpdateBuyBtn()
    {
        if (GameManager.Instance.userData.currentCoin >= propData.price || (propData.unlockWithAds && !GameManager.Instance.userData.HasProp(propData.id)))
        {
            buyBtnSwapper.Enable();
        }
        else
        {
            buyBtnSwapper.Disable();
        }
    }
}

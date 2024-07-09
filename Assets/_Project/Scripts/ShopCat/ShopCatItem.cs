using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ricimi;

public class ShopCatItem : MonoBehaviour
{
    private PetData petData;

    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private GameObject coinIcon;
    [SerializeField]
    private GameObject adsIcon;
    [SerializeField]
    private Image avatar;
    [SerializeField]
    private Material grayMaterial;
    [SerializeField]
    private SpriteSwapper buyBtnSwapper;
    [SerializeField]
    private PopupOpener purchasePopup;
    [SerializeField]
    private PopupOpener adsPopup;
    [SerializeField]
    private Button buyBtn;
    [SerializeField]
    private GameObject ownedBtn;
    [SerializeField]
    private GameObject lockIcon;
    public void Initialize(PetData petData)
    {
        this.petData = petData;
        priceText.text = petData.price.ToString();
        avatar.sprite = petData.avatar;

        GameManager.Instance.OnHeartChange += UpdateBuyBtn;
        
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnHeartChange -= UpdateBuyBtn;
    }
    public void SetLocked()
    {       
        
        priceText.text = petData.price.ToString();
        coinIcon.SetActive(true);
        adsIcon.SetActive(false);
        //buyBtn.interactable = true;
        avatar.material = grayMaterial;
        lockIcon.SetActive(true);
        buyBtn.gameObject.SetActive(true);
        ownedBtn.SetActive(false);
        UpdateBuyBtn();
    }
    public void SetUnlockWithAds()
    {
        priceText.text = "Watch";
        coinIcon.SetActive(false);
        adsIcon.SetActive(true);
        avatar.material = grayMaterial;
        lockIcon.SetActive(true);
        buyBtn.gameObject.SetActive(true);
        ownedBtn.SetActive(false);
    }
    public void SetUnlocked()
    {
        //buyBtnSwapper.Disable();
        priceText.text = "Owned";
        coinIcon.SetActive(false);
        adsIcon.SetActive(false);
        avatar.material = null;
        lockIcon.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        ownedBtn.SetActive(true);
    }
    
    public void OnBuyItem()
    {
        ShopCatController.Instance.currentPet = petData;

        if (petData.unlockWithAds)
        {
            // TO DO Show ads reward here
            //GameController.Instance.BuyNewCat(petData.id);
            //GameManager.Instance.AddNewPet(petData.id);
            //SetUnlocked();
            
            adsPopup.OpenPopup();
        }
        else
        {
            if (GameManager.Instance.userData.currentHeart >= petData.price)
            {
                //GameManager.Instance.AddHeart(-petData.price);
                //GameManager.Instance.AddNewPet(petData.id);
                //GameController.Instance.BuyNewCat(petData.id);
                //SetUnlocked();
                purchasePopup.OpenPopup();
            }
            else
            {
                GameManager.Instance.OnNotEnoughHeart();
            }
        }
        
    }
    private void UpdateBuyBtn()
    {
        if (GameManager.Instance.userData.currentHeart >= petData.price || petData.unlockWithAds)
        {
            buyBtnSwapper.Enable();
        }
        else
        {
            buyBtnSwapper.Disable();
        }
    }
}

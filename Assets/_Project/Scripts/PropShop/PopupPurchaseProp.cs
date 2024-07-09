using Ricimi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPurchaseProp : MonoBehaviour
{
    private PropData currentProp;
    [SerializeField]
    private TMP_Text valueText;
    [SerializeField]
    private TMP_Text heartLeftText;
    [SerializeField]
    private Popup popup;
    [SerializeField]
    private Image petImage;
    [SerializeField]
    private Image shadow;
    [SerializeField]
    private PopupOpener unlockedPopup;
    //TO DO Update Ads btn when reward not ready
    private void Awake()
    {
        currentProp = ShopCatController.Instance.currentProp;
        valueText.text = currentProp.price.ToString();
        petImage.sprite = currentProp.avatar;
        shadow.sprite = currentProp.avatar;
        heartLeftText.text = "Coins left: " + (GameManager.Instance.userData.currentCoin - currentProp.price).ToString();
    }
    public void OnPurchase()
    {
        popup.Close();
        unlockedPopup.OpenPopup();
        GameManager.Instance.AddCoin(-currentProp.price);
        GameManager.Instance.AddNewProp(currentProp.id);
        GameController.Instance.BuyNewProp(currentProp.id);
    }
    public void OnPurchaseAds()
    {
        //TO DO Show reward here
        GameController.Instance.BuyNewProp(currentProp.id);
        GameManager.Instance.AddNewProp(currentProp.id);
        popup.Close();
        unlockedPopup.OpenPopup();
    }
}

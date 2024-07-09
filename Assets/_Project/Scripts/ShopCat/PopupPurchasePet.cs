using Ricimi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopupPurchasePet : MonoBehaviour
{
    private PetData currentPet;
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
        currentPet = ShopCatController.Instance.currentPet;
        valueText.text = currentPet.price.ToString();
        petImage.sprite = currentPet.avatar;
        shadow.sprite = currentPet.avatar;
        heartLeftText.text = "Hearts left: " + (GameManager.Instance.userData.currentHeart - currentPet.price).ToString();
    }
    public void OnGetRewardHeart()
    {
        
        //CoinAnim.Instance?.AddHeart(iconTrans.transform.position, 10, value);
        popup.Close();
    }
    public void OnPurchase()
    {
        popup.Close();
        unlockedPopup.OpenPopup();
        GameManager.Instance.AddHeart(-currentPet.price);
        GameManager.Instance.AddNewPet(currentPet.id);
        GameController.Instance.BuyNewCat(currentPet.id);
    }
    public void OnPurchaseAds()
    {
        //TO DO Show reward here
        GameController.Instance.BuyNewCat(currentPet.id);
        GameManager.Instance.AddNewPet(currentPet.id);
        popup.Close();
        unlockedPopup.OpenPopup();
    }
}

using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPetUnlocked : MonoBehaviour
{
    [SerializeField]
    private Popup popup;
    [SerializeField]
    private Image petImage;
    private void Awake()
    {
        petImage.sprite = ShopCatController.Instance.currentPet.avatar;
    }
    public void OnClose()
    {
        ShopCatController.OnItemUnlocked?.Invoke();
        popup.Close();
    }
}

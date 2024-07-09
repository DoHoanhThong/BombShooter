using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPropUnlocked : MonoBehaviour
{
    [SerializeField]
    private Popup popup;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Image shadowImage;
    private void Awake()
    {
        iconImage.sprite = ShopCatController.Instance.currentProp.avatar;
        shadowImage.sprite = ShopCatController.Instance.currentProp.avatar;
    }
    public void OnClose()
    {
        ShopCatController.OnItemUnlocked?.Invoke();
        popup.Close();
    }
}

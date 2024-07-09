using Ricimi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdsCoinPopup : MonoBehaviour
{
    [SerializeField]
    private int value = 20;
    [SerializeField]
    private TMP_Text valueText;
    [SerializeField]
    private RectTransform iconTrans;
    [SerializeField]
    private Popup popup;
    //TO DO Update Ads btn when reward not ready
    private void Awake()
    {
        valueText.text = value.ToString();
    }
    public void OnGetRewardCoin()
    {
        //TO DO Show reward here
        CoinAnim.Instance?.AddCoins(iconTrans.transform.position, 10, value);
        popup.Close();
    }
}

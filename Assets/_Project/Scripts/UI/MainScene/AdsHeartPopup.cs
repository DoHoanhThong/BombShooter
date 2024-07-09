using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AdsHeartPopup : MonoBehaviour
{
    [SerializeField]
    private int value = 20;
    [SerializeField]
    private TMP_Text valueText;
    [SerializeField]
    private Popup popup;
    [SerializeField]
    private RectTransform iconTrans;
    //TO DO Update Ads btn when reward not ready
    private void Awake()
    {
        valueText.text = value.ToString();
    }
    public void OnGetRewardHeart()
    {
        //TO DO Show reward here
        CoinAnim.Instance?.AddHeart(iconTrans.transform.position, 10, value);
        popup.Close();
    }
}

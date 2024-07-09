using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class DailyRewardElement : MonoBehaviour
{
    public RewardElementData data;
    [SerializeField]
    private Image frame;
    [SerializeField]
    private Image itemBoder;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Image itemShadow;
    [SerializeField]
    private TMP_Text dayText;
    [SerializeField]
    private GameObject checkMark;
    [SerializeField]
    private Material grayMaterial;
    [SerializeField]
    private TMP_Text amountText;
    [SerializeField]
    private DailyItemColor frameColor;
    [System.Serializable]
    public class DailyItemColor
    {
        public Color normalFrame;
        public Color normalBorder;
        public Color selectFrame;
        public Color selectBorder;
    }
    public void InitializeElement(RewardElementData data, bool isCurrentDay = false)
    {
        this.data = data;
        
        itemImage.sprite = data.icon;
        itemShadow.sprite = data.icon;
        amountText.text = data.amount.ToString();

        frame.color = isCurrentDay ? frameColor.selectFrame : frameColor.normalFrame;
        itemBoder.color = isCurrentDay ? frameColor.selectBorder : frameColor.normalBorder;
        dayText.text = isCurrentDay ? "Today" : "Day " + data.day.ToString();
    }
    public void SetClaimed()
    {
        checkMark.SetActive(true);
        itemImage.material = grayMaterial;
    }
    public void SetCanClaim()
    {
        checkMark.SetActive(false);
        itemImage.material = null;
    }
    public void SetLock()
    {
        checkMark.SetActive(false);
        itemImage.material = null;
    }
}

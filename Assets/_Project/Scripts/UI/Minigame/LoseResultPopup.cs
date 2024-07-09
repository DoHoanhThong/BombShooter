using Ricimi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseResultPopup : MonoBehaviour
{
    [SerializeField]
    private TMP_Text amountText;
    [SerializeField]
    private TMP_Text heartRewardText;
    [SerializeField]
    private TMP_Text coinRewardText;
    [SerializeField]
    private TMP_Dropdown toolTip;
    [SerializeField]
    private SpriteSwapper[] stars;
    private void Awake()
    {
        //Get result data
        //var result = ResultContainer.Instance.GetResult();
        //toolTip.options[0].text = ResultContainer.Instance.toolTip;
        //amountText.text = result.score.ToString();
        //heartRewardText.text = result.heartReward.ToString();
        //coinRewardText.text = result.coinReward.ToString();
        //SetStar(result.numStar);
        // TO DO save minigame reward
    }
    public void SetStar(int numStar)
    {
        if (stars.Length == 0)
            return;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].Disable();
        }
        for (int i = 0; i < numStar; i++)
        {
            stars[i].Enable();
        }
    }
}

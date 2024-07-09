using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ricimi;

public class WinResultPopup : MonoBehaviour
{
    public static System.Action OnNextLevel;
    [SerializeField]
    private TMP_Text scoreText;
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
        var result = ResultContainer.Instance.GetResult();
        toolTip.options[0].text = ResultContainer.Instance.toolTip;
        scoreText.text = result.score.ToString();
        heartRewardText.text = result.heartReward.ToString();
        coinRewardText.text = result.coinReward.ToString();
        //SetStar(result.numStar);
    }
    public void SetStar(int numStar)
    {        
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].Disable();
        }
        for (int i = 0; i < numStar; i++)
        {
            stars[i].Enable();
        }
    }
    public void NextLevel()
    {
        OnNextLevel?.Invoke();
    }
    
}

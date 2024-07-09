using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ricimi;

public class MinigameResultPopup : MonoBehaviour
{
    public static System.Action OnAnimDone;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text hightScoreText;
    [SerializeField]
    private NumberCounter collectHeartCounter;
    [SerializeField]
    private NumberCounter collectCoinCounter;

    [SerializeField]
    private CanvasGroup buttonGroup;
    [SerializeField]
    private SceneTransition homeTransition;

    [Header("Coin anim")]
    [SerializeField]
    private HeartCoinAnim coinAnim;
    [SerializeField]
    private RectTransform coinIcon;
    [SerializeField]
    private RectTransform heartIcon;

    [SerializeField]
    private SpriteSwapper doubleBtn;

    private MinigameResult result;
    private int coinNum;
    private int heartNum;
    private bool canDouble = true;    
    private void Awake()
    {
        //Get result data
        result = ResultContainer.Instance.GetResult();
        scoreText.text = result.score.ToString();
        hightScoreText.text = result.hightScore.ToString();
        collectHeartCounter.Value = result.heartReward;
        collectCoinCounter.Value = result.coinReward;
        coinNum = result.coinReward;
        heartNum = result.heartReward;
        canDouble = true;
    }
    public void OnDoubleReward()
    {
        
        if (canDouble)
        {
            canDouble = false;
            doubleBtn.Disable();
            //TO DO Reward Ads here

            coinNum = result.coinReward * 2;
            heartNum = result.heartReward * 2;
            collectHeartCounter.Value = heartNum;
            collectCoinCounter.Value = coinNum;
        }
        
    }
    public void OnPlayAgain()
    {
        coinAnim.AddHeart(heartIcon.position, heartNum < 10 ? heartNum : 10, 0);
        if (coinNum > 0)
        {
            coinAnim.AddCoins(coinIcon.position, coinNum < 10 ? coinNum : 10, 0, () =>
            {
                homeTransition.ReloadCurrentScene();
            });
        }
        else
        {
            homeTransition.ReloadCurrentScene();
        }
        
        
    }
    public void OnReturnHome()
    {
        coinAnim.AddHeart(heartIcon.position, heartNum < 10 ? heartNum : 10, 0);
        if (coinNum > 0)
        {
            coinAnim.AddCoins(coinIcon.position, coinNum < 10 ? coinNum : 10, 0, () =>
            {
                homeTransition.PerformTransition();
            });
        }
        else
        {
            homeTransition.PerformTransition();
        }
    }
    public void NextLevel()
    {
        //OnNextLevel?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
public class ResultContainer : Singleton<ResultContainer>
{
    public string toolTip = "Score = Item * 100 + Timeleft * 10";
    public MinigameResult result;
    public MinigameResult GetResult()
    {
        return result;
    }
    public void SetResult(int score,int hightScore, int heart, int coin)
    {
        result = new MinigameResult(score, hightScore, heart, coin);
    }
}
public class MinigameResult
{
    public int score;
    public int hightScore;
    public int heartReward;
    public int coinReward;

    public MinigameResult()
    {
        score = 0;
        hightScore = 0;
        heartReward = 0;
        coinReward = 0;
    }
    public MinigameResult(int score, int hightScore, int heart, int coin, bool isHightScore = false)
    {
        this.score = score;
        this.hightScore = hightScore;
        heartReward = heart;
        coinReward = coin;
    }
}

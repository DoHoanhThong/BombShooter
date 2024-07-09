using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Ease easeType = Ease.OutBack;
    [SerializeField]
    private float duration = 0.5f;
    public void OnSpawn(int value)
    {
        scoreText.text = "+" + value.ToString();
        scoreText.rectTransform.DOAnchorPosY(scoreText.rectTransform.anchoredPosition.y + 150, duration).SetEase(easeType).OnComplete(()=> 
        {
            Destroy(gameObject);
        });
    }
}

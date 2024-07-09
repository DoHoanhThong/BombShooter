using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FeedCounter : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 60;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Material grayScale;
    [SerializeField]
    private Image[] stars;
    [SerializeField]
    private float scaleDuration = .25f;

    private float counter = 0;
    private bool isCompeleted = false;

    private void Start()
    {
        counter = 0;
        slider.value = 1;
        RequestBase.OnRequestComplete += OnRequestComplete;
    }
    private void OnDestroy()
    {
        RequestBase.OnRequestComplete -= OnRequestComplete;
    }

    private void Update()
    {
        if (!isCompeleted)
        {
            if (slider.value > 0)
            {
                counter += Time.deltaTime;
                slider.value = (maxTime - counter) * 1.0f / maxTime;
                if (slider.value < 2 / 3f)
                {
                    stars[0].material = grayScale;
                }
                if (slider.value < 1 / 3f)
                {
                    stars[1].material = grayScale;
                }
            }
        }
        
        
    }
    private void OnRequestComplete()
    {
        isCompeleted = true;
        float time = .25f;
        if (slider.value >= 2f / 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Invoke("SpawnCoin_" + i, time * i);
                CoinAnim.Instance.AddCoins(Vector3.zero, 3, 3);
            }
        }
        else
        {
            if (slider.value >= 1f / 3)
            {
                for (int i = 1; i < 3; i++)
                {
                    Invoke("SpawnCoin_" + i, time * i);
                    CoinAnim.Instance.AddCoins(Vector3.zero, 2, 2);
                }
            }
            else
            {
                for (int i = 2; i < 3; i++)
                {
                    Invoke("SpawnCoin_" + i, time * i);
                    CoinAnim.Instance.AddCoins(Vector3.zero, 1, 1);
                }
            }
        }
    }
    void SpawnCoin_0()
    {
        stars[0].transform.localScale = Vector3.one * 1.5f;
        stars[0].transform.DOScale(1, scaleDuration).SetEase(Ease.InBack);
        CoinAnim.Instance.AddHeart(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(stars[0].transform.position)), 1, 1);       
    }
    void SpawnCoin_1()
    {
        stars[1].transform.localScale = Vector3.one * 1.5f;
        stars[1].transform.DOScale(1, scaleDuration).SetEase(Ease.InBack);
        CoinAnim.Instance.AddHeart(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(stars[1].transform.position)), 1, 1);
    }
    void SpawnCoin_2()
    {
        stars[2].transform.localScale = Vector3.one * 1.5f;
        stars[2].transform.DOScale(1, scaleDuration).SetEase(Ease.InBack);
        CoinAnim.Instance.AddHeart(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(stars[2].transform.position)), 1, 1);
    }

}

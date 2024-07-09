using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TeraJet;
using System;
using Ricimi;
using DG.Tweening;
using UnityEngine.UI;

public class BombShooterController : TeraJet.Singleton<BombShooterController>
{
    Coroutine a;
    public static System.Action End;
    public bool isOnClickUpgrade;
    public System.Action getCoin;
    public bool isStart;
    public bool Left;
    public bool isEnd;
    public bool isDrag;
    public bool isTele;
    public bool bom_exist;
    public Transform[] listTransform = new Transform[2];
    public int HPbegin;
    [SerializeField]
    PopupOpener _losePopUp;
    [SerializeField] GameObject _hand;
    [SerializeField] int _currentCoin;
    [SerializeField] Text _coinText;
    public bool isExistRocket;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        isTele = false;
        isExistRocket = false;
        isOnClickUpgrade = false;
        if (!PlayerPrefs.HasKey("coin_of_PLAYER"))
        {
            PlayerPrefs.SetInt("coin_of_PLAYER", 0);
        }
        _currentCoin = PlayerPrefs.GetInt("coin_of_PLAYER");
        _coinText.text = _currentCoin.ToString();
        Application.targetFrameRate = 60;
        bom_exist = false;
        HPbegin = 20;
        isStart = false;
        a = StartCoroutine(HandSwipe());
        End += GameOver;
    }
    private void OnDestroy()
    {
        End -= GameOver;
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }

    }
    public void AddCoinOfPlayer(int coin)
    {
        _currentCoin += coin;
        PlayerPrefs.SetInt("coin_of_PLAYER", _currentCoin);
        _coinText.text = _currentCoin.ToString();
    }
    public void ReduceCoinOfPlayer(int coin)
    {
        _currentCoin -= coin;
        PlayerPrefs.SetInt("coin_of_PLAYER", _currentCoin);
        _coinText.text = _currentCoin.ToString();
    }
    public void GameOver()
    {
        _losePopUp.OpenPopup();
    }

    IEnumerator HandSwipe()
    {
        while (!isStart)
        {
            _hand.transform.DOMoveX(-0.7f, 1).OnComplete(() =>
            {
                _hand.transform.DOMoveX(0.7f, 1);
            });
            yield return new WaitForSeconds(2f);

        }
    }
    public int getCurrentCoin()
    {
        return this._currentCoin;
    }
    void UpdateCoin(int coin)
    {
        _currentCoin += coin;
        PlayerPrefs.SetInt("coin_of_PLAYER", _currentCoin);
    }
    void UpdateCoinRender(Text _text)
    {
        _text.text = _currentCoin.ToString();
    }

}

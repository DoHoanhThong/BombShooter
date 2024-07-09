using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeATKspeed : MonoBehaviour
{
    public TypeUpdate _type;
    CanvasGroup _canvas;
    [SerializeField] Text _coinUpgradeText;
    [SerializeField] CanvasGroup _another;
    [SerializeField] Text _typeUpgrade;
    [SerializeField] Image _anotherBg;
    [SerializeField] Image _thisBg;
    [SerializeField] int _currentCoinOfUpgrade;
    [SerializeField] GameObject _anotherText;
    [SerializeField] float _currentAtkSpeed;
    [SerializeField] AudioClip _upgradeSound;
    [SerializeField] AudioClip _changeSound;
    string _upgradeKey;
    void Start()
    {
        _currentAtkSpeed = PlayerPrefs.GetFloat("Player_speedATK");
        _canvas = this.GetComponent<CanvasGroup>();
        _upgradeKey = "upgrade_atkspeed";
        _canvas.alpha = 1;

        if (!PlayerPrefs.HasKey(_upgradeKey))
        {
            PlayerPrefs.SetInt(_upgradeKey, 20);
        }
        _currentCoinOfUpgrade = PlayerPrefs.GetInt(_upgradeKey);
        _coinUpgradeText.text = "Upgrade" + "\n" + _currentCoinOfUpgrade;
        _typeUpgrade.text = "ATK Speed" + "\n" + _currentAtkSpeed.ToString("F2");
    }
    public enum TypeUpdate
    {
        Atk_speed,
        Damage
    }
    public void OnClick()
    {
        AudioController.Instant.PlaySound(_changeSound);
        _anotherText.transform.parent.gameObject.SetActive(false);
        _coinUpgradeText.transform.parent.gameObject.SetActive(true);
        _canvas.alpha = 1;
        _another.alpha = 0.65f;
        _coinUpgradeText.text = "Upgrade" + "\n" + PlayerPrefs.GetInt(_upgradeKey).ToString();
        _typeUpgrade.text = "ATK Speed" + "\n" + _currentAtkSpeed.ToString("F2");
        _thisBg.enabled = true;
        _anotherBg.enabled = false;
    }
    public void OnClickUpgradeButton()
    {
        AudioController.Instant.PlaySound(_upgradeSound);
        if (BombShooterController.Instance.getCurrentCoin() < _currentCoinOfUpgrade)
            return;
        BombShooterController.Instance.ReduceCoinOfPlayer(PlayerPrefs.GetInt(_upgradeKey));
        _currentCoinOfUpgrade += 40;
        PlayerPrefs.SetInt(_upgradeKey, _currentCoinOfUpgrade);
        _coinUpgradeText.text = "Upgrade" + "\n" + _currentCoinOfUpgrade.ToString();
        UpgradeATKSpeed(0.01f);
        _typeUpgrade.text = "ATK Speed" + "\n" + _currentAtkSpeed.ToString("F2");
        _coinUpgradeText.transform.parent.DOScale(Vector3.one * 1.1f, 0.3f).OnComplete(() =>
        {
            _coinUpgradeText.transform.parent.DOScale(Vector3.one, 0.3f);
        });
    }
    void UpgradeATKSpeed(float s)
    {
        AudioController.Instant.PlaySound(_upgradeSound);
        if (_currentAtkSpeed <= 0.2f)
            return;
        _currentAtkSpeed -= s;
        PlayerPrefs.SetFloat("Player_speedATK", _currentAtkSpeed);
    }
}

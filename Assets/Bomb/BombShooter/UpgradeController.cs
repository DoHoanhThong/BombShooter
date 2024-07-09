using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public TypeUpdate _type;
    CanvasGroup _canvas;
    [SerializeField] Text _coinUpgradeText;
    [SerializeField] CanvasGroup _another;
    [SerializeField] Text _typeUpgrade;
    [SerializeField] Image _anotherBg;
    [SerializeField] Image _thisBg;
    [SerializeField] Text _textUpgradeButton;
    [SerializeField] int _currentCoinOfUpgrade;
    [SerializeField] GameObject _anotherText;
    int _currentDmg;
    float _currentAtkSpeed;
    string _upgradeKey;
    public enum TypeUpdate
    {
        Atk_speed,
        Damage
    }
    public void OnClick()
    {
        _anotherText.gameObject.SetActive(true);
        _textUpgradeButton.gameObject.SetActive(false);
        _canvas.alpha = 1;
        _another.alpha = 0.65f;
        _coinUpgradeText.text = "Upgrade" + "\n" + PlayerPrefs.GetInt(_upgradeKey).ToString();
        _typeUpgrade.text = (_upgradeKey == "upgrade_atkspeed") ? "ATK Speed" + "\n" + _currentAtkSpeed : "Damage" + "\n" + _currentDmg;
        _thisBg.enabled = true;
        _anotherBg.enabled = false;
    }
    public void OnClickUpgrade()
    {   if (BombShooterController.Instance.getCurrentCoin() < _currentCoinOfUpgrade)
        {
            Debug.LogError("<");
            return;
        }
        Debug.LogError("currentCoin" + BombShooterController.Instance.getCurrentCoin() + " " + _type + ": " + _currentCoinOfUpgrade);
        BombShooterController.Instance.ReduceCoinOfPlayer(PlayerPrefs.GetInt(_upgradeKey));
        _currentCoinOfUpgrade += 40;
        PlayerPrefs.SetInt(_upgradeKey, _currentCoinOfUpgrade);
        _textUpgradeButton.text = "Upgrade" + "\n" + _currentCoinOfUpgrade.ToString();
        if(_type== TypeUpdate.Damage)
        {
            //UpgradeDMG(2);
            Debug.LogError(_type);
        }
        else
        {
            Debug.LogError(_type);
            //UpgradeATKSpeed(0.01f);
        }
        _typeUpgrade.text = (_upgradeKey == "upgrade_atkspeed") ? "ATK Speed" + "\n" + _currentAtkSpeed : "Damage" + "\n" + _currentDmg;
        _textUpgradeButton.transform.parent.DOScale(Vector3.one * 1.1f, 0.3f).OnComplete(() =>
        {
            _textUpgradeButton.transform.parent.DOScale(Vector3.one, 0.3f);
        });
    }
    void UpgradeATKSpeed(float s)
    {
        if (_currentAtkSpeed <= 0.2f)
            return;
        _currentAtkSpeed -= s;
        PlayerPrefs.SetFloat("Player_dmg", _currentAtkSpeed);
    }
    void UpgradeDMG(int d)
    {
        _currentDmg += 2;
        PlayerPrefs.SetInt("Player_dmg", _currentDmg);
    }
}

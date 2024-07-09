using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDMG : MonoBehaviour
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
    [SerializeField] AudioClip _upgradeSound;
    [SerializeField] AudioClip _changeSound;
    int _currentDmg;
    string _upgradeKey;
    // Start is called before the first frame update
    void Start()
    {
        _currentDmg = PlayerPrefs.GetInt("Player_dmg");
        _canvas = this.GetComponent<CanvasGroup>();
        _upgradeKey = "upgrade_dmg";
        _canvas.alpha = 0.65f;
        _coinUpgradeText.transform.parent.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey(_upgradeKey))
        {
            PlayerPrefs.SetInt(_upgradeKey, 20);
        }
        _currentCoinOfUpgrade = PlayerPrefs.GetInt(_upgradeKey);

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
        _typeUpgrade.text = "Damage" + "\n" + _currentDmg;
        _thisBg.enabled = true;
        _anotherBg.enabled = false;
    }
    public void OnClickUpgrade()
    {
        AudioController.Instant.PlaySound(_upgradeSound);
        if (BombShooterController.Instance.getCurrentCoin() < _currentCoinOfUpgrade)
            return;
        BombShooterController.Instance.ReduceCoinOfPlayer(PlayerPrefs.GetInt(_upgradeKey));
        _currentCoinOfUpgrade += 40;
        PlayerPrefs.SetInt(_upgradeKey, _currentCoinOfUpgrade);
        _coinUpgradeText.text = "Upgrade" + "\n" + _currentCoinOfUpgrade.ToString();
        UpgradeDamage(2);
        _typeUpgrade.text ="Damage" + "\n" + _currentDmg;
        _coinUpgradeText.transform.parent.DOScale(Vector3.one * 1.1f, 0.3f).OnComplete(() =>
        {
            _coinUpgradeText.transform.parent.DOScale(Vector3.one, 0.3f);
        });
    }
    void UpgradeDamage(int d)
    {
        _currentDmg += 2;
        PlayerPrefs.SetInt("Player_dmg", _currentDmg);
    }
}

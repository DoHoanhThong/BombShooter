using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BombCTL;

public class HpBomb : MonoBehaviour
{
    Coroutine a;
    BombCTL _parent;
    public static System.Action ZeroHP;
    Text _text;
    public int HP;
    int _currentHP;
    [SerializeField] AudioClip _explosion;
    [SerializeField] AudioClip _detectionSound;
    void Start()
    {
        _parent=this.transform.parent.GetComponent<BombCTL>();
        _text = this.GetComponent<Text>();
        _currentHP = HP;
        _text.text = HP.ToString();
        if (_parent._type != TypeBom.rocket)
            return;
        BombShooterController.Instance.isExistRocket = true;
    }
    public void SetHP(int hp)
    {
        this.HP = hp;
        _text.text = _currentHP.ToString();
    }
    public void getDMG(int damage, Vector3 pos)
    {
        if (_currentHP <= damage)
        {
            BombShooterController.Instance.isExistRocket = false;
            CameraShake.ShakeScreen.Invoke();
            //Vibrator.Vibrate(50);
            AudioController.Instant.PlaySound(_explosion);
            SpawnBomb.instance.SpawnX2(_parent._type, _parent.transform.position);
            SpawnSmokeVFX.Instance.Spawn(pos);
            Destroy(_parent.gameObject);
            return;
        }
        AudioController.Instant.PlaySound(_detectionSound);
        _currentHP = Mathf.Clamp(_currentHP - damage, 0, HP);
        _text.text = _currentHP.ToString();
    }
    IEnumerator Turnoff()
    {
        yield return new WaitForSeconds(0.1f);
        
    }
}

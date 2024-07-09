using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public static System.Action ClickButton;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _timeCD;
    [SerializeField] GameObject _listBullet;
    [SerializeField] GameObject _tutorial;
    [SerializeField] GameObject _upgrade;
    [SerializeField] AudioClip _fireSound;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Player_speedATK"))
        {
            PlayerPrefs.SetFloat("Player_speedATK", 0.3f);
        }
        if (!PlayerPrefs.HasKey("Player_dmg"))
        {
            PlayerPrefs.SetInt("Player_dmg", 4);
        }
    }

    private void Start()
    {
        _timeCD = 0;
        ClickButton += Spawn;

    }
    private void OnDestroy()
    {
        ClickButton -= Spawn;
    }
    private void Update()
    {
        if (BombShooterController.Instance.isEnd)
            return;
        if (_timeCD > 0)
        {
            _timeCD -= Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Check();
        }
    }
    public void Spawn()
    {
        AudioController.Instant.PlaySound(_fireSound);
        GameObject a = ObjectPooling.instance.GetObject(_bulletPrefab);
        a.transform.position = this.transform.position;
        a.transform.rotation = Quaternion.identity;
        a.transform.SetParent(_listBullet.transform);
        a.SetActive(true);
    }
    void Check()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider != null && hitCollider.gameObject.tag == "upgrade")
            return;
        _timeCD = PlayerPrefs.GetFloat("Player_speedATK");
        ClickButton.Invoke();
        PlayerController.StopTween.Invoke();
        if (BombShooterController.Instance.isStart)
            return;
        _tutorial.SetActive(false);
        _upgrade.SetActive(false);
        BombShooterController.Instance.isStart = true;
    }
}

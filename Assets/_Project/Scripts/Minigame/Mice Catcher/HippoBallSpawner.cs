using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HippoBallSpawner : MonoBehaviour
{
    public static System.Action<bool> OnDestroyHippoBall;
    [SerializeField]
    private int _redNumMax = 5;
    [SerializeField]
    private int _blueNumMax = 5;
    [SerializeField]
    private float _spawnThreshold = 0.25f;

    [SerializeField]
    private HappyHippoBall _redBallPrefab;
    [SerializeField]
    private HappyHippoBall _blueBallPrefab;

    [SerializeField] GameObject _heartPrefab, _coinPrefab;
    [SerializeField] HappyHippoController _hipController;
    [SerializeField]
    Vector2 _spawnPos;
    //[SerializeField]
    //private Transform _rightSpawnPos
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] int _numRedSpawn;
    private int _numBlueSpawn;
    Vector2 _oldPos;

    private void Start()
    {
        OnDestroyHippoBall += OnBallDestroy;
        HappyHippoController.OnGameComplete += Reset;
        HappyHippoController.OnNextLevel += Reset;
        RevivePopup.OnPlayerRevive += OnRevive;

    }
    public void Reset()
    {
        StopAllCoroutines();
        foreach (var ball in GetComponentsInChildren<HappyHippoBall>())
        {
            Destroy(ball.gameObject);
        }
    }
    public void Initialize()
    {
        _redNumMax = _hipController.numFish2;
        _blueNumMax = _hipController.numFish1;

        _numRedSpawn = _redNumMax;
        _numBlueSpawn = _blueNumMax;
        StartCoroutine(SpawnBall());
        StartCoroutine(SpawnBall());
    }
    private void OnDestroy()
    {
        RevivePopup.OnPlayerRevive -= OnRevive;
        OnDestroyHippoBall -= OnBallDestroy;
        HappyHippoController.OnGameComplete -= Reset;
        HappyHippoController.OnNextLevel -= Reset;
    }
    IEnumerator SpawnBall()
    {

        do
        {
            float x = Random.Range(-_spawnPos.x, _spawnPos.x);
            if (x > -2 && x < 2)
            {
                spawnPosition = new Vector2(x, Random.Range(0, _spawnPos.y));
            }
            else
            {
                spawnPosition = new Vector2(x, Random.Range(_spawnPos.y, _spawnPos.y));
            }
        }
        while (Vector2.Distance(_oldPos, spawnPosition) < 1);
        _oldPos = spawnPosition;
        
        WaitForSeconds wait = new WaitForSeconds(_spawnThreshold);
        while (true)
        {
            if (Random.value > 0.5f)
            {
                if (_numRedSpawn > 0)
                {

                    HappyHippoBall a = Instantiate(_redBallPrefab, spawnPosition, Quaternion.identity, transform);
                    //InstanceItem(a);
                    _numRedSpawn -= 1;

                }
            }
            else
            {
                if (_numBlueSpawn > 0)
                {

                    Instantiate(_blueBallPrefab, spawnPosition, Quaternion.identity, transform);
                    _numBlueSpawn -= 1;
                }

            }

            yield return wait;
        }
    }
    private void OnBallDestroy(bool isRedBall)
    {
        if (isRedBall)
        {
            _numRedSpawn += 1;
        }
        else
        {
            _numBlueSpawn += 1;
        }
    }
    void InstanceItem(HappyHippoBall a)
    {
        if (Mathf.Abs(_hipController.currentTime - Random.Range(8, 13)) <= 1)
        {
            _hipController.currentTime = 0;
            GameObject tmp = (Random.Range(0, 2) == 0) ? _heartPrefab : _coinPrefab;
            GameObject t = Instantiate(tmp, a.transform.position, Quaternion.identity, a.transform);
            t.transform.localScale = Vector3.one * 0.8f;
        }
    }
    IEnumerator StartAgain()
    {
        yield return new WaitForSeconds(1.5f);
        Initialize();
    }
    void OnRevive()
    {
        Initialize();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPipeContronller : MonoBehaviour
{
    [SerializeField] GameObject _coinPrefab, _heartPrefab;
    [SerializeField] GameObject _listPipeSpawn;
    [SerializeField] GameObject _pipePrefab;
    [SerializeField] float _cdTime;
    [SerializeField] float _timeDuration;
    [SerializeField] float _oldPosY;
    [SerializeField] float _minRangePos, _maxRangePos, _chooseRangePos;
    [SerializeField] float _minRangeItem, _maxRangeItem, _chooseRangeItem;
    [SerializeField] float _posYIncrease;
    void Start()
    {
        _cdTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.isStart)
            return;
        if (LevelManager.instance.isEnd)
            return;
        _cdTime -= Time.deltaTime;
        if (_cdTime >= 0)
            return;
        
        _cdTime = _timeDuration;
        Spawn();
        //instant coin or heart
        //if (Mathf.Abs(Random.Range(_minRangeItem, _maxRangeItem) - 5) <= _chooseRangeItem)
        //{
        //    GameObject coin = ObjectPooling.instance.GetObject(_coinPrefab);
        //    coin.SetActive(true);
        //    coin.transform.SetParent(pipe.transform);
        //    coin.transform.position = new Vector2(pipe.transform.position.x, pipe.transform.position.y + _posYIncrease);
        //    coin.transform.localScale = Vector3.one * 0.45f;
        //    coin.transform.rotation = Quaternion.Euler(0, 0, -180);
            
        //    return;
        //}
        //if (Mathf.Abs(Random.Range(_minRangeItem, _maxRangeItem) - 6) <= _chooseRangeItem)
        //{
            
        //    GameObject heart = ObjectPooling.instance.GetObject(_heartPrefab);
        //    heart.SetActive(true);
        //    heart.transform.SetParent(pipe.transform);
        //    heart.transform.position = new Vector2(pipe.transform.position.x, pipe.transform.position.y + _posYIncrease);
        //    heart.transform.localScale = Vector3.one * 0.45f;
        //    heart.transform.rotation = Quaternion.Euler(0, 0, -180);
        //}

    }
    
    void Spawn()
    {
        GameObject pipe = ObjectPooling.instance.GetObject(_pipePrefab);
        LevelManager.instance.listPipe.Add(pipe);
        pipe.SetActive(true);
        pipe.transform.SetParent(_listPipeSpawn.transform);
        float t = Random.Range(_minRangePos, _maxRangePos);
        while (Mathf.Abs(_oldPosY - t) <= _chooseRangePos)
        {
            t = Random.Range(_minRangePos, _maxRangePos);
        }
        Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pipe.transform.position = new Vector2(screen.x + 1, t);
        _oldPosY = t;
    }
}

using System.Collections;
using System.Collections.Generic;
using TeraJet;
using UnityEngine;

public class SpawnCoin : Singleton<SpawnCoin>
{
    [SerializeField] GameObject _coin;
    [SerializeField] GameObject _listCoin;
    public void spawnCoin(Vector3 pos, int number)
    {
        if (number == 0)
        {
            Debug.LogError("ErrorSpawnCoin!");
            return;
        }
        GameObject g1 = null;
        GameObject g = ObjectPooling.instance.GetObject(_coin);
        g.transform.position = pos;
        g.transform.SetParent(_listCoin.transform);
        g.transform.localScale = Vector3.one;
        g.transform.rotation = Quaternion.identity;
        g.transform.GetComponent<CoinController>().jumpLeft = true;
        g.SetActive(true);
        if (number == 1)
            return;
        g1 = ObjectPooling.instance.GetObject(_coin);
        g1.transform.position = pos +new Vector3(0.1f,0,0);
        g1.transform.SetParent(_listCoin.transform);
        g1.transform.localScale = Vector3.one;
        g1.transform.rotation = Quaternion.identity;
        g1.transform.GetComponent<CoinController>().jumpLeft = false;
        g1.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject heartPrefab;
    //[SerializeField]
    //private Transform holder;
    public void SpawnCoin(Vector2 spawnPos, int numCoin = 1)
    {
        for (int i = 0; i < numCoin; i++)
        {
            Instantiate(coinPrefab, spawnPos, Quaternion.identity, HouseItemHolder.Instance.holder);
        }
    }
    public void SpawnHeart(Vector2 spawnPos, int numHeart = 1)
    {
        for (int i = 0; i < numHeart; i++)
        {
            Instantiate(heartPrefab, spawnPos, Quaternion.identity, HouseItemHolder.Instance.holder);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class PropData : ScriptableObject
{
    public string id;
    public string itemName;
    public bool unlockWithAds;
    public int price;
    public string description;
    public Sprite avatar;
    public GameObject itemPrefab;
}

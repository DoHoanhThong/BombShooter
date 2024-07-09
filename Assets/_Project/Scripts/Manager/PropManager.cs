using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    [SerializeField]
    private PropData[] propsData;
    [SerializeField]
    private Transform propHolder;
    [SerializeField]
    private UnlockGiftbox giftboxPrefab;
    private void Awake()
    {
        Invoke("Init", .5f);
    }
    private void Init()
    {
        foreach(var propID in GameManager.Instance.userData.unlockedProps)
        {
            var data = GetProp(propID);
            if (data)
            {
                Instantiate(data.itemPrefab, propHolder);
            }
        }
    }
    public PropData GetProp(string propID)
    {
        for (int i = 0; i < propsData.Length; i++)
        {
            if (propsData[i].id == propID)
            {
                return propsData[i];
            }
        }
        return null;
    }
    public void UnlockNewProp(string propID)
    {
        var data = GetProp(propID);
        var newProp = Instantiate(data.itemPrefab, propHolder);
        //newPet.OnUnlocked();
        var giftBox = Instantiate(giftboxPrefab, newProp.transform.position, Quaternion.identity);
        giftBox.SetUnlockObj(newProp.gameObject);
    }
}

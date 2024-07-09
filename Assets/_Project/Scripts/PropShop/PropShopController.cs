using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropShopController : MonoBehaviour
{
    [SerializeField]
    private PropData[] propsData;
    [SerializeField]
    private PropShopItem shopItemPrefab;
    [SerializeField]
    private RectTransform itemHolder;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (var data in propsData)
        {
            var item = Instantiate(shopItemPrefab, itemHolder);
            item.Initialize(data);
            item.gameObject.SetActive(true);
            if (GameManager.Instance.userData.HasProp(data.id))
            {
                item.SetUnlocked();
            }
            else
            {
                if (data.unlockWithAds)
                {
                    item.SetUnlockByAds();
                }
                else
                {
                    item.SetLocked();
                }
            }
        }
    }
}

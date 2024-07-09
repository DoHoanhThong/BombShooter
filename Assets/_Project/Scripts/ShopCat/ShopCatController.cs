using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;

public class ShopCatController : Singleton<ShopCatController>
{
    public static System.Action OnItemUnlocked;

    [SerializeField]
    private Ricimi.Popup popup;
    [Space]
    [Header("Pet shop")]
    public PetData currentPet;

    [SerializeField]
    private PetData[] petsData;
    [SerializeField]
    private ShopCatItem itemPrefab;
    [SerializeField]
    private RectTransform itemHolder;
    private List<ShopCatItem> listItems = new List<ShopCatItem>();

    [Space]
    [Header("Props shop")]
    public PropData currentProp;

    [SerializeField]
    private PropData[] propsData;
    [SerializeField]
    private PropShopItem propPrefab;
    [SerializeField]
    private RectTransform propHolder;
    private List<PropShopItem> listProps = new List<PropShopItem>();
    public override void Awake()
    {
        base.Awake();
        Init();
        OnItemUnlocked += ClosePopup;
    }
    private void OnDestroy()
    {
        OnItemUnlocked -= ClosePopup;
    }

    private void Init()
    {
        foreach (var data in petsData)
        {
            var item = Instantiate(itemPrefab, itemHolder);
            item.Initialize(data);
            item.gameObject.SetActive(true);
            if (GameManager.Instance.userData.HasPet(data.id))
            {
                item.SetUnlocked();
            }
            else
            {
                if (data.unlockWithAds)
                {
                    item.SetUnlockWithAds();
                }
                else
                {
                    item.SetLocked();
                }               
            }
        }

        foreach (var data in propsData)
        {
            var item = Instantiate(propPrefab, propHolder);
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
    public void SpawnCat()
    {
        //GameController.Instance.SpawnNewCat();
    }
    public void ClosePopup()
    {
        popup.Close();
    }
}

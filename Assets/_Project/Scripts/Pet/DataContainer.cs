using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
public class DataContainer : Singleton<DataContainer>
{
    public bool playMinigame;

    public RuntimeData currentCatData;

    [SerializeField]
    private PetData[] listCatData;

    public List<RuntimeData> runtimeDatas = new List<RuntimeData>();
    public override void Awake()
    {
        Invoke("Initilize", .2f);

        base.Awake();
    }
    private void Initilize()
    {
        //TO DO Remove this line
        GameManager.Instance.LoadData();
        GameManager.Instance.OnCoinChange?.Invoke();
        GameManager.Instance.OnHeartChange?.Invoke();
        foreach (var petID in GameManager.Instance.userData.unlockedPets)
        {
            runtimeDatas.Add(new RuntimeData(GetData(petID)));
        }
    }
    public void OnRequestDone()
    {
        if (currentCatData != null)
        {
            currentCatData.currentState = CharacterState.Normal;
            currentCatData = null;
        }
    }
    public PetData GetData(string petID)
    {
        foreach (var data in listCatData)
        {
            if (data.id == petID)
            {
                return data;
            }
        }
        return null;
    }
}

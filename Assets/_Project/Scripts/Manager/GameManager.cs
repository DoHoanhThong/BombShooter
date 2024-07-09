using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
public class GameManager : Singleton<GameManager>
{
    #region USERDATA

    public UserData userData;

    public System.Action OnCoinChange;
    public System.Action OnHeartChange;
    public System.Action OnNotEnoughCoin;
    public System.Action OnNotEnoughHeart;

    public void QuickSave()
    {
        GameTool.SaveUserData(userData);
    }
    public void LoadData()
    {
        //userData = GameTool.LoadUserData();
        userData.CheckNullableObject();
    }
    public void AddCoin(int value)
    {
        if (userData.currentCoin + value >= 0)
        {
            userData.currentCoin += value;
            if (value != 0)
            {
                OnCoinChange?.Invoke();
                QuickSave();
            }
        }
        else
        {
            OnNotEnoughCoin?.Invoke();
        }
    }
    public void AddHeart(int value)
    {
        if (userData.currentHeart + value >= 0)
        {
            userData.currentHeart += value;
            if (value != 0)
            {
                OnHeartChange?.Invoke();
                QuickSave();
            }
        }
        else
        {
            OnNotEnoughHeart?.Invoke();
        }
    }
    public void AddNewPet(string petID)
    {
        if (!userData.HasPet(petID))
        {
            userData.unlockedPets = GameUtils.AddItemToArray(userData.unlockedPets, petID);
            QuickSave();
        }
    }
    public void AddNewProp(string propID)
    {
        if (!userData.HasProp(propID))
        {
            userData.unlockedProps = GameUtils.AddItemToArray(userData.unlockedProps, propID);
            QuickSave();
        }
    }
    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
        LoadData();
    }
}

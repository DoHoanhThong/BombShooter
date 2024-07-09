using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    /*-------------PLAYER DATA-------------*/
    public int currentLevel = 1;

    public string userName;

    public int userAge;

    public int currentCoin;

    public int currentHeart;
    
    public bool isFirstTimeOpen = true;

    public bool isRated = false;

    /*-------------NULLABLE DATA-------------*/
    //public string[] unlockedHats = new string[] { };

    //public UserPetData[] unlockedPets = new UserPetData[] { };

    public string[] unlockedPets = new string[] { };

    public string[] unlockedProps = new string[] { };

    /*-------------PLAYER SETTINGS DATA-------------*/
    public bool isMusicOn = true;

    public bool isSoundOn = true;

    public bool isVibrateOn = true;

    public void CheckNullableObject()
    {
        
        //if (unlockedPets == null)
        //{
        //    unlockedPets = new UserPetData[] { };
        //}
        if (unlockedPets == null)
        {
            unlockedPets = new string[] { };
        }
        if (unlockedProps == null)
        {
            unlockedProps = new string[] { };
        }
    }
    public bool HasPet(string petID)
    {
        for (int i = 0; i < unlockedPets.Length; i++)
        {
            if (petID == unlockedPets[i])
            {
                return true;
            }
        }
        return false;
    }
    public bool HasProp(string propID)
    {
        for (int i = 0; i < unlockedProps.Length; i++)
        {
            if (propID == unlockedProps[i])
            {
                return true;
            }
        }
        return false;
    }
    public UserData(UserData userData)
    {
        currentLevel = userData.currentLevel;
        userName = userData.userName;
        userAge = userData.userAge;
        currentCoin = userData.currentCoin;
        currentHeart = userData.currentHeart;
        unlockedPets = userData.unlockedPets;
        isMusicOn = userData.isMusicOn;
        isSoundOn = userData.isSoundOn;
        isVibrateOn = userData.isVibrateOn;
        isFirstTimeOpen = userData.isFirstTimeOpen;
        isRated = userData.isRated;
    }
    public UserData()
    {
        currentLevel = 1;
        userName = "Player";
        userAge = 10;
        currentCoin = 0;
        currentHeart = 0;
        unlockedPets = new string[] {};
        isMusicOn = true;
        isSoundOn = true;
        isVibrateOn = true;
        isFirstTimeOpen = true;
        isRated = false;

    }
}
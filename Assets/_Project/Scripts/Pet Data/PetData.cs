using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu]
public class PetData : ScriptableObject
{
    public string id;
    public string petName;
    public string skinID;
    public bool unlockWithAds;
    public string description;
    public Type type;
    [Header("Shop")]
    public int price;
    public Sprite avatar;
    
    public enum Type
    {
        CAT,
        DOG,
    }
    public PetData()
    {
        id = "1";
        petName = "";
        skinID = "1";
        description = "";
        type = Type.CAT;
    }
}

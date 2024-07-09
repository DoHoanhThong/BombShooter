using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RewardElementData : ScriptableObject
{
    public int day;
    public Sprite icon;
    public int amount;
    public bool isSkinReward;
    public Type type;
    public enum Type
    {
        HEART,
        COIN,
        PET,
        GIFT,
    }
}

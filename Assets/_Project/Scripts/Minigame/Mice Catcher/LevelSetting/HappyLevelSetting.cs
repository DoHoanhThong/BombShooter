using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSetting", menuName = "HappyHippo/Level Setting", order = 1)]
[System.Serializable]
public class HappyLevelSetting :ScriptableObject
{
    public int Level;
    [Space]
    [Header("Level setting: ")]
    public int Target;
    public int Difficult;
    public int Time;
    public int Timeup;
    public int Fish_1_count;
    public int Fish_2_count;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeConnect", menuName = "Pipe Connect/Level Setting", order = 1)]
public class PipeLevelSetting : ScriptableObject
{
    public int rows;
    public int columns;
    //public Color[] cellColors;
    public int[] cellNumbers;
    public int[] cellRotations;


}
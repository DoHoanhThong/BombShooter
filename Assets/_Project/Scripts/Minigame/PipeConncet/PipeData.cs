using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "PipeConnect", menuName = "Pipe Connect/Pipe Data", order = 1)]
public class PipeData : ScriptableObject
{
    public string id;
    public bool hasTwoDirection;
    public GameObject pipePrefab;
}

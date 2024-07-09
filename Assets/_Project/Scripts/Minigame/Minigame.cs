using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    
    public virtual void Awake()
    {
        if (DataContainer.Instance)
        {
            DataContainer.Instance.playMinigame = true;
        }
        if (BackgroundMusic.Instance)
        {
            BackgroundMusic.Instance.PlayMiniGame();
        }
    }
}

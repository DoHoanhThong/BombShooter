using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ricimi;
public class MinigameChoose : MonoBehaviour
{
    [SerializeField]
    private Popup popup;
    public void Awake()
    {
        if (BackgroundMusic.Instance)
        {
            BackgroundMusic.Instance.PlayMinigameChoose();
        }
    }
    public void OnClose()
    {
        if (BackgroundMusic.Instance)
        {
            BackgroundMusic.Instance.PlayLivingRoom();
        }
        popup.Close();
    }

}

using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePopup : MonoBehaviour
{
    public static System.Action OnPlayerRevive;
    public static System.Action OnPlayerExit;

    [SerializeField]
    private Popup popup;
    public void OnRevive()
    {
        // TO DO Show reward ads here
        OnPlayerRevive?.Invoke();
        popup.Close();
    }
    public void OnExit()
    {
        OnPlayerExit?.Invoke();
        popup.Close();
    }
}

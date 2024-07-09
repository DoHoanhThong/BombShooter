using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestBase : MonoBehaviour
{
    public static System.Action OnRequestStart;
    public static System.Action OnRequestComplete;
    public virtual void OnStart()
    {
        //GameController.Instance.isHomeScene = false;
    }
    public virtual void OnExcute()
    {

    }
    public virtual void OnComplete()
    {
        OnRequestComplete?.Invoke();
        DataContainer.Instance?.OnRequestDone();
    }
    public virtual void OnClose()
    {
        //GameController.Instance.isHomeScene = true;
    }
}

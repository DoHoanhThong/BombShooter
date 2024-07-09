using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestConfenti : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] effects;

    private void Start()
    {
        foreach (var fx in effects)
        {
            fx.playOnAwake = false;
        }
        RequestBase.OnRequestComplete += Show;
       
    }
    private void OnDestroy()
    {
        RequestBase.OnRequestComplete -= Show;
    }
    private void Show()
    {
        foreach (var fx in effects)
        {
            fx.Play();
        }
    }
}

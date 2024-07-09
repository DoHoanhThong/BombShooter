using System;
using System.Collections;
using System.Collections.Generic;
using TeraJet;
using UnityEngine;

public class SpawnSmokeVFX : Singleton<SpawnSmokeVFX>
{
    [SerializeField] GameObject _listVFX;
    [SerializeField] GameObject _smokeVFX;
    [SerializeField] int _scale;
    public void Spawn(Vector3 pos)
    {
        GameObject g = ObjectPooling.instance.GetObject(_smokeVFX);
        g.transform.SetParent(_listVFX.transform);
        g.transform.position = pos;
        g.transform.localScale = Vector3.one * _scale;
        g.transform.rotation = Quaternion.identity;
        g.SetActive(true);
    }
}

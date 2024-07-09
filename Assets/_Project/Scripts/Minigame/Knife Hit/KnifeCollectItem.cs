using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectItem : MonoBehaviour
{
    public Type type;
    [SerializeField]
    private ParticleSystem collectFX;

    public enum Type
    {
        Coin,
        Heart,
    }
    public void OnCollect()
    {
        
        Destroy(Instantiate(collectFX, transform.position, collectFX.transform.rotation).gameObject, 2);
        Destroy(gameObject);
    }
}

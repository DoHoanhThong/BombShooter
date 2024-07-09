using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : CollectObject
{
    [SerializeField]
    private int heartValue = 1;
    [SerializeField]
    private ParticleSystem collectFX;

    private bool canCollect = true;
    private void Awake()
    {
        canCollect = true;
    }
    public override void OnCollect()
    {
        if (canCollect)
        {
            canCollect = false;
            //AudioManager.Instance.PlayCoinCollect();
            CoinAnim.Instance.AddHeart(Camera.main.WorldToScreenPoint(transform.position), 1, heartValue);
            Destroy(Instantiate(collectFX, transform.position, Quaternion.identity).gameObject, 2);
            Destroy(gameObject);
        }
        
    }
}

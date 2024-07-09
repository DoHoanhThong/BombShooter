using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectObject
{
    [SerializeField]
    private int coinValue = 1;

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
            CoinAnim.Instance.AddCoins(Camera.main.WorldToScreenPoint(transform.position), 1, coinValue);
            Destroy(Instantiate(collectFX, transform.position, Quaternion.identity).gameObject, 2);
            Destroy(gameObject);
        }
        
    }
}

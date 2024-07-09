using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TeraJet;
public class CoinAnim : Singleton<CoinAnim>
{
    public System.Action OnAnimationComplete;

    //References
    [Header("UI references")]
    //[SerializeField] TMP_Text coinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] GameObject animatedDiamondPrefab;
    [SerializeField] Transform coinTarget;
    [SerializeField] Transform diamondTarget;

    [Space]
    [Header("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();
    [SerializeField] private Transform coinHolder;
    [Space]
    [Header("Available diamonds : (diamonds to pool)")]
    [SerializeField] int maxDiamonds = 10;
    Queue<GameObject> diamondsQueue = new Queue<GameObject>();
    [SerializeField] private Transform diamondsHolder;
    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.2f, 0.5f)] float minAnimDuration;
    [SerializeField] [Range(0.5f, 1f)] float maxAnimDuration;

    Ease easeType = Ease.Unset;
    [SerializeField] float spread;
    [SerializeField]
    private float speed = 1;

    private void Start()
    {
        PrepareCoins();
    }
    private void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab,transform);
            coin.transform.SetParent(coinHolder, false);
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
        GameObject diamond;
        for (int i = 0; i < maxCoins; i++)
        {
            diamond = Instantiate(animatedDiamondPrefab, transform);
            diamond.transform.SetParent(diamondsHolder, false);
            diamond.SetActive(false);
            diamondsQueue.Enqueue(diamond);
        }
    }


    public IEnumerator Animate(Vector3 collectedCoinPosition, int amount, double addCoin, System.Action OnAnimationComplete)
    {
        Transform[] coinTrans = new Transform[amount];

        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.transform.SetParent(coinHolder.root);
                coin.SetActive(true);
                
                coinTrans[i] = coin.transform;

                //Set up position and scale 
                coinTrans[i].localScale = Vector3.zero;
                coinTrans[i].position = collectedCoinPosition;
                Vector3 randomSpread = new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), 0f);


                //Phase 1
                coin.transform.DOScale(new Vector3(1, 1, 1), 0.1f * speed).OnComplete(() =>
                {
                    //SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.POC_KEY, 0.75f);
                    //AudioManager.Instance.PlayCoinSell();
                    coin.transform.DOMove(collectedCoinPosition + randomSpread, 0.9f * speed);
                });

                yield return new WaitForSeconds((amount < 10 ? 1f / (amount * 2) : 1f / amount) * speed);

            }

        }

        //animate coin to target position
        StartCoroutine(Anim(coinTrans, addCoin, OnAnimationComplete));

    }


    private IEnumerator Anim(Transform[] coinTrans, double addCoin, System.Action OnAnimationComplete)
    {
        int numCoin = (int) addCoin / coinTrans.Length;
        
        foreach (Transform coin in coinTrans)
        {

            float duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);
            coin.DOMove(coinTarget.position, 0.7f * speed).SetEase(easeType).OnComplete(() =>
            {
                //executes whenever coin reach target position
                coin.gameObject.SetActive(false);
                coin.transform.SetParent(coinHolder);
                coinsQueue.Enqueue(coin.gameObject);
                //SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.COLLECTMONEY, 0.75f);
                //AudioManager.Instance.PlayCoinUp();
                GameManager.Instance.AddCoin (numCoin);
                //addCoin -= numCoin;

            });
            yield return new WaitForSeconds(0.5f * speed / coinTrans.Length);
        }
        yield return new WaitForSeconds(0.2f*speed);        
        GameManager.Instance.AddCoin((int)(addCoin - numCoin * coinTrans.Length > 0 ? addCoin - numCoin * coinTrans.Length : 0));

        OnAnimationComplete?.Invoke();
    }

    public void Test()
    {
        CoinAnim.Instance.AddCoins(Vector2.zero, 10, 1000);
    }

    public void AddCoins(Vector3 collectedCoinPosition, int amount, double addCoin, System.Action OnAnimationComplete = null)
    {
        StartCoroutine(Animate(collectedCoinPosition, amount, addCoin, OnAnimationComplete));
    }
    public void AddHeart(Vector3 collectedDiamondPosition, int amount, double addDiamond, System.Action OnAnimationComplete = null)
    {
        StartCoroutine(AnimateDiamond(collectedDiamondPosition, amount, addDiamond, OnAnimationComplete));
    }






    public IEnumerator AnimateDiamond(Vector3 collectedDiamondPosition, int amount, double addDiamond, System.Action OnAnimationComplete)
    {
        Transform[] diamondTrans = new Transform[amount];

        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (diamondsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject diamond = diamondsQueue.Dequeue();
                diamond.transform.SetParent(diamondsHolder.root);
                diamond.SetActive(true);
                diamondTrans[i] = diamond.transform;

                //Set up position and scale 
                diamondTrans[i].localScale = Vector3.zero;
                diamondTrans[i].position = collectedDiamondPosition;
                Vector3 randomSpread = new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), 0f);
                //Phase 1
                diamond.transform.DOScale(new Vector3(1, 1, 1), 0.1f * speed).OnComplete(() =>
                {
                    //SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.POC_KEY, 0.75f);
                    //AudioManager.Instance.PlayCoinSell();
                    diamond.transform.DOMove(collectedDiamondPosition + randomSpread, 0.9f * speed);
                });

                yield return new WaitForSeconds(amount < 10 ? 1f / (amount * 2) : 1f / amount);

            }

        }

        //animate coin to target position
        StartCoroutine(AnimDiamond(diamondTrans, addDiamond, OnAnimationComplete));

    }


    private IEnumerator AnimDiamond(Transform[] diamondTrans, double addDiamond, System.Action OnAnimationComplete)
    {
        int numDiamond = (int)addDiamond / diamondTrans.Length;

        foreach (Transform diamond in diamondTrans)
        {

            float duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);
            diamond.DOMove(diamondTarget.position, 0.7f * speed).SetEase(easeType).OnComplete(() =>
            {
                //executes whenever coin reach target position
                diamond.gameObject.SetActive(false);
                diamond.SetParent(diamondsHolder);
                diamondsQueue.Enqueue(diamond.gameObject);
                //SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.COLLECTMONEY, 0.75f);
                //AudioManager.Instance.PlayCoinUp();
                GameManager.Instance.AddHeart(numDiamond);
                //addCoin -= numCoin;

            });
            yield return new WaitForSeconds(0.5f * speed / diamondTrans.Length);
        }
        yield return new WaitForSeconds(0.2f * speed);
        GameManager.Instance.AddHeart((int)addDiamond - numDiamond * diamondTrans.Length > 0 ? (int)addDiamond - numDiamond * diamondTrans.Length : 0);

        OnAnimationComplete?.Invoke();
    }
}

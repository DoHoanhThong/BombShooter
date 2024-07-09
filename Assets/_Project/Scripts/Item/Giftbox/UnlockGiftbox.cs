using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class UnlockGiftbox : CollectObject
{
   
    public GameObject objectCarrying;
    [SerializeField]
    private ParticleSystem openFX;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float duration = .25f;
    [SerializeField]
    private Ease easeType = Ease.OutBack;
    [SerializeField]
    private string triggerString = "Open";
    [SerializeField]
    private int requiredPresses = 1;
    [SerializeField]
    private float timeToOpen = 10.0f;
    [SerializeField]
    private float delayTime = 0.5f;

    private bool isEnable;
    private int currentPresses = 0;
    private float currentTime = 0.0f;

    public void SetUnlockObj(GameObject carryObject)
    {
        objectCarrying = carryObject;
        objectCarrying.transform.position = transform.position;
        objectCarrying.SetActive(false);
    }

    private void Awake()
    {
        isEnable = false;
        animator.enabled = false;
        transform.localScale = Vector3.zero;
        ShopCatController.OnItemUnlocked += EnableGift;
        
    }
    private void OnDestroy()
    {
        ShopCatController.OnItemUnlocked -= EnableGift;
    }
    public void EnableGift()
    {
        transform.DOScale(1, duration).SetEase(easeType).OnComplete(() => animator.enabled = true);
        isEnable = true;
    }

    private void Update()
    {
        if (isEnable)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToOpen)
            {
                OpenBox();
            }
        }
        
    }

    public override void OnCollect()
    {
        if (isEnable)
        {
            currentPresses++;
            if (currentPresses >= requiredPresses)
            {
                OpenBox();
            }
            else
            {
                if (!animator.enabled)
                    animator.enabled = true;
                animator.SetTrigger(triggerString);
            }
        }
        
    }

    private void OpenBox()
    {
        isEnable = false;
        //animator.SetTrigger("Open");
        animator.enabled = false;
        transform.DOScale(1.2f, duration).OnComplete(() => 
        {
            objectCarrying.SetActive(true);
            Destroy(Instantiate(openFX, transform.position, Quaternion.identity).gameObject, 2);
            Destroy(gameObject);
        }).SetEase(Ease.InBack);
        
    }
    IEnumerator DelayOpen(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
    }
}

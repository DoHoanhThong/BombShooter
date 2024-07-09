using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;
using DG.Tweening;
public class PetToFeed : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isActing = false;
    [SerializeField]
    private RectTransform mouthPos;
    [SerializeField]
    private CatAnim anim;
    [SerializeField]
    private SkeletonUtilityBone eyesBone;
    [SerializeField]
    private float duration = .5f;
    [Header("Walk Task")]
    [SerializeField]
    private float walkDuration = 5;
    [Header("Wipe Task")]
    [SerializeField]
    private FeedWipeAnim feedWipeAnim;
    private void Awake()
    {
        WalkToCenter();
        if (DataContainer.Instance?.currentCatData != null)
        {
            anim.SetSkin(DataContainer.Instance.currentCatData.petData.skinID);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (PetFeedController.Instance.isCleaning || isActing)
            return;
        isActing = true;
        GameObject dropped = eventData.pointerDrag;
        var food = dropped.GetComponent<PetFood>();
        food.isDragable = false;
        var foodTrans = dropped.GetComponent<RectTransform>();
        if (dropped.GetComponent<CleaningPaper>() == null)
        {            
            foodTrans.SetParent(mouthPos);
            foodTrans.DOMove(mouthPos.position, duration).OnComplete(() =>
            {
                anim.PlayEat_2();
                
                foodTrans.DOScale(0, duration).OnComplete(() =>
                {
                    foodTrans.gameObject.SetActive(false);
                    //Destroy(foodTrans.gameObject);
                    food.OnUsed(mouthPos);
                    //UnPauseAnim();
                    //isActing = false;
                    anim.PlayEat_3();
                    SoundFXManager.Instance?.PlayCatEat();
                    Invoke("SetFeedAble", 2);
                });
            });
        }
        else
        {
            foodTrans.SetParent(feedWipeAnim.holder);
            foodTrans.DOMove(feedWipeAnim.holder.position, duration).OnComplete(() =>
            {
                food.OnUsed(feedWipeAnim.crumbHolder.GetComponent<RectTransform>());
                feedWipeAnim.OnWipe();               
            });
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (PetFeedController.Instance.isCleaning)
        //    return;
        //if (PetFeedController.Instance.isDraging)
        //{
        //    anim.PlayEat_1();
        //    //StartCoroutine(PauseAtMiddle());
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (PetFeedController.Instance.isCleaning)
        //    return;
        //if (PetFeedController.Instance.isDraging)
        //{
        //    //Focus FX
        //}
    }
    public void SetFeedAble()
    {
        isActing = false;
    }
    private void Update()
    {
        if (PetFeedController.Instance.currentFood != null)
        {
            LookToTarget(PetFeedController.Instance.currentFood.transform);
        }
        else
        {
            eyesBone.mode = SkeletonUtilityBone.Mode.Follow;
        }
    }
    public void LookToTarget(Transform target)
    {
        eyesBone.mode = SkeletonUtilityBone.Mode.Override;
        eyesBone.transform.position = target.position;
    }
    public void WalkToCenter()
    {
        anim.FlipX(true);
        anim.PlayWalk();
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-1200, rect.anchoredPosition.y);
        rect.DOAnchorPosX(0, walkDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            RequestBase.OnRequestStart?.Invoke();
            anim.FlipX(false);
            anim.PlayIdle();
            Invoke("PlayEatIdle", 2f);
            //anim.PlayEat_1();
        });
    }
    void PlayEatIdle()
    {
        anim.PlayEat_1();
    }
}

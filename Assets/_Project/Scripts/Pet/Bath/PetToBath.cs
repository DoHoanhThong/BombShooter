using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;
using DG.Tweening;
public class PetToBath : MonoBehaviour, IDropHandler
{
    public bool isActing = false;
    [SerializeField]
    private RectTransform startPos;
    [SerializeField]
    private CatAnim anim;
    [Header("Trash Task")]
    [SerializeField]
    private BathTrash bathTrash;
    [SerializeField]
    private BathBrush bathBrush;
    [SerializeField]
    private float duration = .5f;
    [Header("Walk Task")]
    [SerializeField]
    private float walkDuration = 5;
    [Header("Bathtub Task")]
    [SerializeField]
    private RectTransform bathPos;
    [SerializeField]
    private RectTransform bathTub;
    [SerializeField]
    private float jumpDuration = 1;
    [Header("Shower Task")]
    [SerializeField]
    private BathAnimController bathAnim;

    private Vector2 centerPos;
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
        isActing = true;
        GameObject dropped = eventData.pointerDrag;
        var brush = dropped.GetComponent<BathBrush>();
        if (brush != null)
        {
            brush.isDragable = false;
            var brushTrans = dropped.GetComponent<RectTransform>();
            brushTrans.SetParent(bathTrash.brushHolder.transform);
            brushTrans.DOMove(startPos.position, duration).OnComplete(() =>
            {
                //anim.PlayEat_2();
                bathTrash.StartClean();

            });
            return;
        }
        var bathShower = dropped.GetComponent<BathDragableObject>();
        if (bathShower != null)
        {
            bathShower.isDragable = false;
            var brushTrans = dropped.GetComponent<RectTransform>();                      
            brushTrans.DOMove(bathAnim.holderStartPos.transform.position, duration).OnComplete(() =>
            {
                brushTrans.SetParent(bathAnim.holder.transform);
                brushTrans.anchoredPosition = Vector2.zero;
                //anim.PlayEat_2();
                anim.PlayHappy_2();
                bathAnim.SetAnimTrigger(bathShower.triggerName);
                SoundFXManager.Instance?.PlayWateringBubble();
            });
        }
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
            Invoke("PlayIdle", 2f);
            centerPos = transform.position;
            //anim.PlayEat_1();
        });
    }
    public void JumpToBathtub()
    {
        isActing = true;

        bathTub.gameObject.SetActive(true);
        bathTub.SetAsFirstSibling();
        bathTub.DOAnchorPosX(0, duration * 2).SetEase(Ease.OutBack);

        var startPos = transform.position;
        var endPos = bathPos.position;
        var midPoint = (startPos + endPos) / 2;
        midPoint.y = 2;

        transform.DOScale(0.8f, duration * 2).SetDelay(duration * 2).OnComplete(() =>
        {
            bathTub.SetAsLastSibling();
        });

        var rect = GetComponent<RectTransform>();
        rect.DOPath(new[] { startPos, midPoint, endPos }, jumpDuration, PathType.CatmullRom)
           .SetEase(Ease.Linear)
           .OnWaypointChange(waypointIndex =>
           {
               if (waypointIndex == 0)
               {
                   anim.PlayJumpUp();
               }
               else if (waypointIndex == 1)
               {
                   // Action at mid point
                   anim.PlayJumpDown();
               }
           })
           .SetDelay(duration * 2)
           .OnComplete(() => 
            { 
                isActing = false;
                anim.PlayIdle_3();
               // PixelCrushers.MessageSystem.SendMessage(this, "Jumpin", "Bathtub");
            });
    }
    public void JumpOutBathtub()
    {
        isActing = true;

        transform.DOScale(1, duration * 2).OnComplete(() =>
        {
            bathTub.SetAsFirstSibling();
        });
        
        var startPos =  transform.position;
        var endPos = (Vector3) centerPos;
        var midPoint = (startPos + endPos) / 2;
        midPoint.y = 2;
       
        var rect = GetComponent<RectTransform>();
        rect.DOPath(new[] { startPos, midPoint, endPos }, jumpDuration, PathType.CatmullRom)
           .SetEase(Ease.Linear)
           .OnWaypointChange(waypointIndex =>
           {
               if (waypointIndex == 0)
               {
                   anim.PlayJumpUp();
               }
               else if (waypointIndex == 1)
               {
                   // Action at mid point
                   anim.PlayJumpDown();
               }
           })
            .OnComplete(() =>
            {
                isActing = false;
                anim.PlayIdle_3();
                //PixelCrushers.MessageSystem.SendMessage(this, "Jumpout", "Bathtub");
            });
     
        //bathTub.SetAsFirstSibling();
        bathTub.DOAnchorPosX(-1200, duration * 2).SetEase(Ease.InBack).SetDelay(duration * 2).OnComplete(()=> { bathTub.gameObject.SetActive(false); });
    }
    public void PlayIdle()
    {
        anim.PlayIdle_1();
        bathBrush.FadeIn();
    }
    
}

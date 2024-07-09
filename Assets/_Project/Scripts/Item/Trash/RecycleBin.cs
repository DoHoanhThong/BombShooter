using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TeraJet;
public class RecycleBin : Singleton<RecycleBin>, IPointerEnterHandler, IPointerExitHandler
{
    public HouseTrash currentTrash;

    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image trashbinImage;
    [SerializeField]
    private RectTransform trashbinHolder;
    [SerializeField]
    private Image highlight;
    [SerializeField]
    private float duration = 0.25f;
    [SerializeField]
    private Vector2 startPos = new Vector2(0, -350);
    [SerializeField]
    private Ease inEase = Ease.OutBounce;
    [SerializeField]
    private Ease outEase = Ease.InBounce;

    private bool canCollect;
   
    public override void Awake()
    {
        base.Awake();
        rectTransform.anchoredPosition = startPos;

        highlight.color = new Color(1, 1, 1, 0);
        trashbinImage.color = new Color(1, 1, 1, 0);
        //trashbinShadow.color = new Color(1, 1, 1, 0);
        highlight.gameObject.SetActive(false);
        trashbinImage.gameObject.SetActive(false);
        trashbinHolder.gameObject.SetActive(false);
        canCollect = false;
    }
    public void OnStartHoldTrash(HouseTrash trash)
    {
        if (currentTrash == null)
        {
            currentTrash = trash;
            rectTransform.anchoredPosition = startPos;

            highlight.color = new Color(1, 1, 1, 0);
            trashbinImage.color = new Color(1, 1, 1, 0);
            //trashbinShadow.color = new Color(1, 1, 1, 0);
            highlight.gameObject.SetActive(true);
            trashbinImage.gameObject.SetActive(true);
            trashbinHolder.gameObject.SetActive(true);
            trashbinImage.DOColor(Color.white, duration);
            //trashbinShadow.DOColor(Color.white, duration);

            rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(inEase);
        }
        
    }
    public void OnStopHoldTrash()
    {
        highlight.color = Color.white;
        trashbinImage.color = Color.white;
        //trashbinShadow.color = Color.white;
        highlight.gameObject.SetActive(false);
        //trashbinShadow.DOColor(new Color(1, 1, 1, 0), duration);
        trashbinImage.DOColor(new Color(1, 1, 1, 0), duration).OnComplete(()=> 
        {
            trashbinImage.gameObject.SetActive(false);
            trashbinHolder.gameObject.SetActive(false);
        });

        rectTransform.DOAnchorPos(startPos, duration).SetEase(outEase);
        if (canCollect)
        {
            currentTrash.OnCollect();
            canCollect = false;
            //Destroy trash
        }
        currentTrash = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentTrash != null)
        {
            highlight.DOColor(Color.white, duration);
            canCollect = true;
            //transform.DOScale(1.2f, duration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTrash != null)
        {
            highlight.DOColor(new Color(1, 1, 1, 0), duration);
            canCollect = false;
            //transform.DOScale(1f, duration);
        }
    }    
}

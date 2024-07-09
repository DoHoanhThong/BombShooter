using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetBathController : RequestBase
{
    public static PetBathController Instance;

    public CatAnim petAnim;

    [SerializeField]
    private PopupBase popup;

    [SerializeField]
    private RectTransform counter;
    [SerializeField]
    private Image frame;
    [SerializeField]
    private float duration = 0.25f;

    private void Awake()
    {
        OnRequestStart += OnFeedPet;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //OnStart();
        //PixelCrushers.MessageSystem.SendMessage(this, "Start", "");
    }
    private void OnDestroy()
    {
        OnRequestStart -= OnFeedPet;
    }
    public override void OnStart()
    {
        base.OnStart();
        Initialize();

    }
    private void OnFeedPet()
    {
        //PixelCrushers.MessageSystem.SendMessage(this, "Request", "Feed");
        OnStart();
    }
    public void Initialize()
    {
        //foodSpawner.SpawnFood();
        counter.anchoredPosition = new Vector2(counter.anchoredPosition.x, 900);
        counter.gameObject.SetActive(true);
        counter.DOAnchorPosY(0, duration).SetEase(Ease.OutBack);

        frame.gameObject.SetActive(true);
        frame.color = new Color(1, 1, 1, 0);
        frame.DOColor(new Color(1, 1, 1, 100f / 255f), duration).SetDelay(duration);
    }
    public override void OnExcute()
    {
        base.OnExcute();
    }
    public override void OnComplete()
    {
        base.OnComplete();
        //TO DO Pet happy + Reward
        petAnim.PlayHappy();      
    }
    public override void OnClose()
    {
        base.OnClose();
        //PixelCrushers.MessageSystem.SendMessage(this, "Close", "Feed Pet");

        //foodSpawner.DisableFood();
        //popup.ClosePopup();
    }
}

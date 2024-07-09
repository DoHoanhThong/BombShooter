using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using TeraJet;
public class PipeConnectController : Minigame
{
    public static PipeConnectController Instance;
    public static System.Action OnCompleteLevel;

    public bool isPaused = false;
    [Header("Setting")]
    private float maxTime = 45;
    [Space]
    [Header("Reference")]
    [SerializeField]
    private PipeBoard board;
    [Space]
    [Header("UI Reference:")]
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text level_text;
    [SerializeField]
    private PopupOpener winResultPopup;
    [SerializeField]
    private PopupOpener nextLevelPopup;
    [SerializeField]
    private PopupOpener revivePopup;
    [SerializeField]
    private PopupOpener loseResultPopup;
    [SerializeField]
    private float duration = 0.25f;
    [Space]
    [Header("Level Setting: ")]
    [SerializeField]
    private PipeLevelSetting[] levelSettings;

    private int currentLevel = 0;
    
    private float timeCounter;
    private bool canRevive = true;
    public override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        base.Awake();
    }
    private void Start()
    {
        currentLevel = 0;
        OnCompleteLevel += ShowNextLevelPopup;
        WinResultPopup.OnNextLevel += NextLevel;
        RevivePopup.OnPlayerRevive += OnRevive;
        RevivePopup.OnPlayerExit += OnGameOver;

        board.Init(levelSettings[currentLevel]);
        Init();
        Popup.OnOpenPopup += Pause;
        Popup.OnClosePopup += UnPause;
        canRevive = true;
    }
    private void OnDestroy()
    {
        OnCompleteLevel -= ShowNextLevelPopup;
        WinResultPopup.OnNextLevel -= NextLevel;
        Popup.OnOpenPopup -= Pause;
        Popup.OnClosePopup -= UnPause;
        RevivePopup.OnPlayerRevive -= OnRevive;
        RevivePopup.OnPlayerExit -= OnGameOver;
    }
    private void Init()
    {        
        StopAllCoroutines();
        timeCounter = maxTime;
        level_text.text = "Level " + currentLevel;
        level_text.transform.localScale = Vector3.one * 1.2f;
        level_text.transform.DOScale(1, duration).SetEase(Ease.InBack);
        StartCoroutine(TimeCounting());
    }
    private void ShowNextLevelPopup()
    {
        Pause();
        MemoryPet.OnGetPoint?.Invoke();
        ResultContainer.Instance.SetResult(3, 3, 3, 3);
        //Fire work here
        TeraJet.GameUtils.ExcuteFunction(() =>
        {
            nextLevelPopup.OpenPopup();
        }, 1);
        
    }
    private void NextLevel()
    {        
        currentLevel = currentLevel + 1 >= levelSettings.Length ? 0 : currentLevel + 1;
        board.NextLevel(levelSettings[currentLevel]);
        Init();
        UnPause();
    }
    public void Pause()
    {
        isPaused = true;
    }
    public void UnPause()
    {
        isPaused = false;
    }
    IEnumerator TimeCounting()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (timeCounter > 0)
        {
            timeCounter--;
            timeText.text = timeCounter + "s";
            timeText.transform.localScale = Vector3.one * 1.2f;
            timeText.transform.DOScale(1, duration).SetEase(Ease.InBack);
            yield return wait;
            yield return new WaitWhile(() => isPaused == true);
        }
        // Game over here
        OnGameOver();
    }
    private void OnRevive()
    {
        timeCounter += 30;
        StopAllCoroutines();
        StartCoroutine(TimeCounting());
    }
    private void OnGameOver()
    {
        ResultContainer.Instance.SetResult(0, 3, 3, 3);
        if (canRevive)
        {
            canRevive = false;
            revivePopup.OpenPopup();
        }
        else
        {
            loseResultPopup.OpenPopup();
        }
    }
}

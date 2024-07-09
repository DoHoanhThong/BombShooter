using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ricimi;

public class DailyRewardManager : MonoBehaviour
{
    public static Action OnCloseDaily;
    public static Action OnOpenDaily;

    [SerializeField] private List<RewardElementData> _listElementData = new List<RewardElementData>();
    private List<DailyRewardElement> _listElementView = new List<DailyRewardElement>();

    [SerializeField] DailyRewardElement _elementViewPreb;

    [SerializeField] Button claimBtn;
    [SerializeField] Button claimx2Btn;
    [SerializeField]
    private SpriteSwapper claimSwapper;
    [SerializeField]
    private SpriteSwapper claimx2Swapper;
    //[SerializeField] Button claimAgainBtn;

    [SerializeField] Popup _popup;
    [SerializeField] RectTransform _contentHolder;

    [SerializeField] TMP_Text _countDownText;
    DailyRewardController _rewardController;

    Coroutine c_countdownView;

    int TotalDays => _listElementData.Count;

    private DailyRewardElement currentElement;

    void Start()
    {
        Initialize();
        //if (_rewardController.LastClaimDay.Day == DateTime.Now.Day)
        //    OpenPanel();
        //Debug.Log("Last" + _rewardController.LastClaimDay.Day);
        //Debug.Log("Now" + DateTime.Now.Day);
        OnOpenDaily?.Invoke();
        c_countdownView = StartCoroutine(IE_CountDown());
    }
    public void ClosePanel()
    {
        OnCloseDaily?.Invoke();
        if (c_countdownView != null) StopCoroutine(c_countdownView);
        _popup.Close();

    }

    IEnumerator IE_CountDown()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            //Debug.Log(_rewardController.NextClaimDay.DayOfYear - DateTime.Now.DayOfYear);
            if (_rewardController.NextClaimDay.DayOfYear - DateTime.Now.DayOfYear <= 0)
            {
                CheckStatus();
            }
            else
            {
                if (_rewardController.GetDayClaimStatus(DateTime.Now.DayOfYear - _rewardController.FirstDayDate.DayOfYear) == 0)
                {
                    _countDownText.text = "Claim Now";
                }
                else
                {
                    DateTime convertedDate = _rewardController.NextClaimDay;
                    convertedDate = new DateTime(convertedDate.Year, convertedDate.Month, convertedDate.Day);
                    TimeSpan time = convertedDate - DateTime.Now;

                    if (_countDownText != null)
                    {
                        //Huy
                        _countDownText.text =  time.Hours + ":" + time.Minutes + ":" + time.Seconds;
                    }
                }


            }

            yield return wait;
        }
        //_countDownText.text = "Ready";
    }

    private bool canClaim;
    private int currentDay;
    public void Initialize()
    {
        _rewardController = new DailyRewardController();

        //DateTime lastClaim = ES3.Load("LastClaim", DateTime.Now.AddDays(-1));
        //currentDay = ES3.Load("LoginDayOrder", 0);

        //canClaim = DateTime.Now.DayOfYear - lastClaim.DayOfYear >= 1;

        for (int i = 0; i < _listElementData.Count; i++)
        {
            var item = Instantiate(_elementViewPreb, _contentHolder);
            item.gameObject.SetActive(true);
            _listElementView.Add(item);

            if (i == currentDay)
            {
                item.InitializeElement(_listElementData[i], true);
            }
            else
            {
                item.InitializeElement(_listElementData[i]);
            }
            
            if (i < currentDay)
            {
                item.SetClaimed();
            }
            else
            {
                if (i == currentDay && canClaim)
                {
                    item.SetCanClaim();
                }
                else
                {
                    item.SetLock();
                }
            }
        }
       
        
        if (canClaim)
        {
            claimSwapper.Enable();
            claimx2Swapper.Enable();
        }
        else
        {
            claimSwapper.Disable();
            claimx2Swapper.Disable();
        }

    }
    public void OnClaim()
    {
        if (canClaim)
        {
            currentDay++;
            //ES3.Save("LoginDayOrder", currentDay);
            //ES3.Save("LastClaim", DateTime.Now);
            claimSwapper.Disable();
            claimx2Swapper.Disable();
            UpdateAllView();
        }
        
    }
    public void OnDoubleClaim()
    {
        if (canClaim)
        {
            //TO DO Show reward ads here
            currentDay++;
            //ES3.Save("LoginDayOrder", currentDay);
           // ES3.Save("LastClaim", DateTime.Now);
            claimSwapper.Disable();
            claimx2Swapper.Disable();
            UpdateAllView();
        }
        
    }

    void CheckStatus()
    {
        DateTime firstDay = _rewardController.FirstDayDate;

        DateTime currentDay = DateTime.Now;
        //DateTime nextClaimDay = _rewardController.NextClaimDay;
        //DateTime lastClaim = _rewardController.LastClaimDay;

        int claimIndex = currentDay.DayOfYear - firstDay.DayOfYear;
        claimIndex = Mathf.Max(0, claimIndex);

        if (claimIndex == 0)
        {

        }
        else
        {
            if (claimIndex >= TotalDays || _rewardController.GetDayClaimStatus(claimIndex - 1) == 0)
            {
                ResetDaily();
                return;
            }
        }

        if (_rewardController.GetDayClaimStatus(claimIndex) == 0)
        {
            //_listElementView[claimIndex].SetState(RewardElementView.ViewState.UnClaimed);
            claimBtn.gameObject.SetActive(true);


            //claimBtn.onClick.RemoveAllListeners();
            //claimBtn.onClick.AddListener(_listElementView[claimIndex].ClaimReward);

            //claimx2Btn.onClick.RemoveAllListeners();
            //claimx2Btn.onClick.AddListener(_listElementView[claimIndex].ClaimX2Reward);

            currentElement = _listElementView[claimIndex];

            if (_listElementData[claimIndex].isSkinReward)
            {
                claimx2Btn.gameObject.SetActive(false);
            }
            else
            {
                claimx2Btn.gameObject.SetActive(true);
            }


        }
        else
        {
            claimBtn.onClick.RemoveAllListeners();
            claimx2Btn.onClick.RemoveAllListeners();

            claimBtn.gameObject.SetActive(false);
            claimx2Btn.gameObject.SetActive(false);
        }

        _rewardController.NextClaimDay = DateTime.Now.AddDays(1);
    }
    void UpdateAllView()
    {
        //canClaim = DateTime.Now.DayOfYear - ES3.Load("LastClaim", DateTime.Now.AddDays(-1)).DayOfYear >= 1;

        for (int i = 0; i < _listElementView.Count; i++)
        {
            var item = _listElementView[i];
            if (i < currentDay)
            {
                item.SetClaimed();
            }
            else
            {
                if (i == currentDay && canClaim)
                {
                    item.SetCanClaim();
                }
                else
                {
                    item.SetLock();
                }
            }
        }
    }

    public void OnItemClaim(int index)
    {
        _rewardController.SetDayClaimed(index, 1);
        claimBtn.gameObject.SetActive(false);
        claimx2Btn.gameObject.SetActive(false);
        claimBtn.onClick.RemoveAllListeners();
        claimx2Btn.onClick.RemoveAllListeners();
        //pingIcon.SetActive(false);
        //claimAgainBtn.gameObject.SetActive(true);
        //SFXManager.Instance.PlayUIClick();
    }

    void ResetDaily()
    {
        _rewardController.FirstDayDate = DateTime.Now;
        _rewardController.NextClaimDay = DateTime.Now;
        _rewardController.LastClaimDay = DateTime.Now;


        for (int i = 0; i < _listElementData.Count; i++)
        {
            _rewardController.SetDayClaimed(i, 0);
        }

        UpdateAllView();
        CheckStatus();
    }
    public void OnRewardDailyAgain()
    {
        //GameManager.Instance.ShowRewardAds(() => 
        //{
        //    currentElement.ClaimReward();
        //    pingIcon.SetActive(false);
        //}, null, "ClaimDailyAgain");
    }

}

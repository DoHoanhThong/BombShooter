using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ricimi;
using TeraJet;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;

public class HappyHippoController : Minigame
{
    bool canRevive;
    public static HappyHippoController Instance;

    public static System.Action OnGameComplete;
    public static System.Action OnPlayerGetPoint;
    public static System.Action OnNextLevel;
    public static System.Action Over;
    [SerializeField]
    private HippoHead _catHead;
    public HappyLevelSetting[] happyLevelsSettings;
    [SerializeField] int _currentLevel;
    public int _targetpoint = 20;
    [SerializeField]
    private RectTransform timeHand;
    [SerializeField]
    private HippoBallSpawner _spawner;
    [Space]
    [Header("UI Reference")]
    [SerializeField]
    private PopupOpener winOpener;
    [SerializeField]
    private PopupOpener _losePopUp;
    [SerializeField]
    PopupOpener outOfTimePopup;
    [SerializeField] Text _levelText;
    [SerializeField] Slider _progressSlider;
    public TMP_Text timeCounterText;
    [SerializeField] TMP_Text _textCoin, _textHeart;
    [SerializeField]
    private float _duration = .25f;
    public int currentTime;
    private int _point = 0;
    public int timeCounter = 0;
    int _timeLimit;
    public int heart, coin;
    public bool isPause = false;
    public int numFish1, numFish2;
    bool isNewLevel;
    private float timeHandZ = 0;
    public override void Awake()
    {
        timeHandZ = 0;
        _progressSlider.value = 0;
        isNewLevel = true;
        PlayerPrefs.SetInt("Time_up", happyLevelsSettings[0].Timeup);
        canRevive = true;
        _levelText.text = "Level 0";
        _currentLevel = 0;
        currentTime = 0;
        numFish1 = happyLevelsSettings[0].Fish_1_count;
        numFish2 = happyLevelsSettings[0].Fish_2_count;
        _targetpoint = happyLevelsSettings[0].Target;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        base.Awake();
        OnPlayerGetPoint += UpdatePoint;
        OnNextLevel += NextLevel;
        Over += GameOver;
        RevivePopup.OnPlayerRevive += OnRevive;
        RevivePopup.OnPlayerExit += GameOver;
        Initialize();
    }
    public void InitLevel(int Level)
    {
        _currentLevel = Level;
        _levelText.text = "Level " + _currentLevel.ToString();
        RectTransform g = _levelText.transform.GetComponent<RectTransform>();
        g.DOAnchorPosY(160, 0.6f).OnComplete(() =>
        {
            g.DOAnchorPosY(-40, 0.6f);
            _levelText.text = "Level " + _currentLevel.ToString();
            StartCoroutine(Wait());
        });
        _targetpoint = happyLevelsSettings[Level].Target;
        //_targetText.text = _targetpoint.ToString();
        PlayerPrefs.SetInt("Time_up", happyLevelsSettings[Level].Timeup);
        numFish1 = happyLevelsSettings[Level].Fish_1_count;
        numFish2 = happyLevelsSettings[Level].Fish_2_count;
        Initialize();
    }
    public void Initialize()
    {
        if (isNewLevel)
        {
            _point = 0;
            _catHead.point = 0;
            timeCounter = happyLevelsSettings[_currentLevel].Time;
            timeCounterText.text = timeCounter.ToString();
            _timeLimit = timeCounter;
            isNewLevel = false;
        }

        _spawner.Initialize();
        //StopAllCoroutines();
        StartCoroutine(TimeCounting());
    }
    private void OnDestroy()
    {
        RevivePopup.OnPlayerExit -= GameOver;
        OnPlayerGetPoint -= UpdatePoint;
        OnNextLevel -= NextLevel;
        RevivePopup.OnPlayerRevive -= OnRevive;
        Over -= GameOver;
    }
    private void OnRedPlayerTap()
    {
        _catHead.Attack();
    }

    private void UpdatePoint()
    {
        if (_point != _catHead.point)
        {
            _point = _catHead.point;

            _progressSlider.value = (float)_catHead.point / _targetpoint;
            if (_progressSlider.value >= _progressSlider.maxValue)
            {
                StopAllCoroutines();
                OnNextLevel?.Invoke();

            }
            //{
            //    //Stop game
            //    StopAllCoroutines();

            //    ResultContainer.Instance.SetResult(_timeCounter * 1f / _time > 2f / 3 ? 3 : _timeCounter * 1f / _time > 1f / 3 ? 2 : 1, _timeCounter * 10 + _point * 100, _point, _point);

            //    winOpener.OpenPopup();
            //    OnGameComplete?.Invoke();
            //    return;
            //}
        }
    }
    IEnumerator TimeCounting()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (timeCounter > 0)
        {
            timeCounter--;
            timeCounterText.transform.localScale = Vector3.one * 1.5f;
            timeCounterText.text = timeCounter.ToString();
            timeCounterText.transform.DOScale(1, _duration).SetEase(Ease.InBack);
            timeHandZ += 360f / _timeLimit;
            timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
            int newTime = Mathf.Clamp(currentTime + 1, 0, 10);
            currentTime = newTime;
            yield return wait;

        }

        GameOver();
        OnGameComplete?.Invoke();
    }
    void OnRevive()
    {
        timeHandZ = 0;
        timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
        timeCounter += happyLevelsSettings[_currentLevel].Timeup;
        timeCounterText.text = timeCounter.ToString();
        _timeLimit = timeCounter;
    }

    void NextLevel()
    {
        isNewLevel = true;
        _currentLevel += 1;
        RectTransform g = _levelText.transform.GetComponent<RectTransform>();
        g.DOAnchorPosY(160, 0.6f).OnComplete(() =>
        {
            g.DOAnchorPosY(-40, 0.6f);
            _levelText.text = "Level " + _currentLevel.ToString();
            StartCoroutine(Wait());
        });

        timeCounter = happyLevelsSettings[_currentLevel].Time;
        timeCounterText.text = timeCounter.ToString();
        timeHandZ = 0;
        timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
        _point = 0;
        _targetpoint = happyLevelsSettings[_currentLevel].Target;
        _progressSlider.value = 0;
        numFish1 = happyLevelsSettings[_currentLevel].Fish_1_count;
        numFish2 = happyLevelsSettings[_currentLevel].Fish_2_count;
        PlayerPrefs.SetInt("Time_up", happyLevelsSettings[_currentLevel].Timeup);
        PlayerPrefs.SetInt("LevelComplete", _currentLevel);
    }
    public void GameOver()
    {
        ResultContainer.Instance.SetResult(_point, _point, heart, coin);
        if (canRevive)
        {
            outOfTimePopup.OpenPopup();
            GameObject a = GameObject.FindGameObjectWithTag("Timeup");
            a.transform.GetComponent<TMP_Text>().text = "+" + PlayerPrefs.GetInt("Time_up") + "s";
            canRevive = false;
        }
        else
        {
            _losePopUp.OpenPopup();
        }

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        Initialize();
    }
    public void AddItem()
    {
        if (_textCoin.text != coin.ToString())
        {
            _textCoin.text = coin.ToString();
            _textCoin.transform.DOScale(Vector3.one * 1.5f, 0.3f).OnComplete(() =>
            {
                _textCoin.transform.DOScale(Vector3.one * 1, 0.3f);
            });
        }
        if (_textHeart.text != heart.ToString())
        {
            _textHeart.text = heart.ToString();
            _textHeart.transform.DOScale(Vector3.one * 1.5f, 0.3f).OnComplete(() =>
            {
                _textHeart.transform.DOScale(Vector3.one * 1, 0.3f);
            });
        }
    }
}

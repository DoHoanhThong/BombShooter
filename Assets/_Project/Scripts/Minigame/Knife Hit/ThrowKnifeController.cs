using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Ricimi;

public class ThrowKnifeController : Minigame
{
    public static System.Action OnKnifeHitSuccess;
    public static System.Action OnKnifeHitFail;
    public static System.Action OnHitHeart;
    public static System.Action OnHitCoin;

    private bool canRevive = true;

    [SerializeField]
    private int maxLife = 3;
    [SerializeField]
    private float _threshold = 0.1f;
    [SerializeField]
    private float _spawnTime = 0.25f;
    [SerializeField]
    private KnifeScript _knifePrefab;
    [SerializeField]
    private Transform _spawnPos;

    private KnifeScript _currentKnife;

    [SerializeField]
    private float _duration = 1f;
    [Header("Circle FX Setting")]
    [SerializeField]
    private GameObject _circleFX;
    [SerializeField]
    private float _circleStartScale = 0.15f;
    [SerializeField]
    private float _circleEndScale = 2f;

    [Space]
    [Header("UI Reference")]
    [SerializeField]
    private LifeCounter lifeCount;
    [SerializeField]
    private TMP_Text pointCountText;
    [SerializeField]
    private TMP_Text heartCollectText;
    [SerializeField]
    private TMP_Text coinCollectText;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private PopupOpener resultPopup;
    [SerializeField]
    private PopupOpener revivePopup;
    //To do show revive

    [SerializeField]
    private KnifeBoard board;
    [SerializeField]
    private KnifeCount knifeCounter;
    [Space]
    [SerializeField]
    private KnifeLevelSetting[] levelSettings;
    //[SerializeField]
    //private KnifePooler knifePooler;

    private bool _startAble;

    private int _currentLevel = 0;
    private int lifeCounter = 0;
    private int knifeCount;
    private int _point;
    private float _timeCounter = 0;
    private int heartCount;
    private int coinCount;

    private Vector2 startPos;
    public override void Awake()
    {
        base.Awake();
        startPos = board.transform.position;
        Init();
        canRevive = true;
        Screen.orientation = ScreenOrientation.Portrait;
        OnKnifeHitSuccess += OnHitSuccess;
        OnKnifeHitFail += OnHitFail;
        OnHitHeart += OnCollectHeart;
        OnHitCoin += OnCollectCoin;
        RevivePopup.OnPlayerRevive += OnPlayerRevive;
        RevivePopup.OnPlayerExit += OnGameOver;
    }

    private void Init()
    {
        _currentLevel = 0;
        //knifePooler.Init();
        board.Initialize(levelSettings[_currentLevel]);
        knifeCounter.Initalize(levelSettings[_currentLevel].numKnife);
        knifeCount = levelSettings[_currentLevel].numKnife;
        _currentKnife = Instantiate(_knifePrefab, _spawnPos.position, Quaternion.identity, transform);

        _point = 0;
        pointCountText.text = _point.ToString();



        lifeCounter = maxLife;
        lifeCount.Init(maxLife);

        heartCount = 0;
        coinCount = 0;
        heartCollectText.text = heartCount.ToString();
        coinCollectText.text = coinCount.ToString();

        _currentLevel = 0;
        levelText.text = "Level " + levelSettings[_currentLevel].level.ToString();
        _startAble = true;

        _point = 0;
    }
    private void OnDestroy()
    {
        OnKnifeHitSuccess -= OnHitSuccess;
        OnKnifeHitFail -= OnHitFail;
        OnHitHeart -= OnCollectHeart;
        OnHitCoin -= OnCollectCoin;
        RevivePopup.OnPlayerRevive -= OnPlayerRevive;
        RevivePopup.OnPlayerExit -= OnGameOver;
    }
    private float lastTime = 0;
    public void OnTap()
    {
        if (!_startAble)
            return;
        if (_currentKnife != null && _timeCounter > _threshold)
        {
            _currentKnife.transform.position = _spawnPos.position;
            _currentKnife.Throw();
            _currentKnife = null;
            _timeCounter = 0;
            knifeCount--;
            knifeCounter.OnThrowKnife(knifeCount, true);
            if (knifeCount > 0)
            {
                _currentKnife = Instantiate(_knifePrefab, new Vector2(_spawnPos.position.x, _spawnPos.position.y - 2), Quaternion.identity, transform);
                _currentKnife.transform.DOMoveY(_spawnPos.position.y, _spawnTime);
            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnTap();
        }
        _timeCounter += Time.deltaTime;
    }
    private void OnHitSuccess()
    {
        _point++;
        pointCountText.text = _point.ToString();
        pointCountText.transform.localScale = Vector3.one * 1.2f;
        pointCountText.transform.DOScale(1, _duration / 4).SetEase(Ease.InBack);
        if (knifeCount <= 0)
        {
            NextLevel();
        }
    }
    private void OnHitFail()
    {
        lifeCounter--;
        lifeCount.LoseLife();
        if (lifeCounter <= 0)
        {
            //Game Over
            OnGameOver();
        }
        if (knifeCount <= 0)
        {
            NextLevel();
        }
    }
    private void OnGameOver()
    {
        _startAble = false;
        ResultContainer.Instance.SetResult(_point > 20 ? 3 : _point > 10 ? 2 : 1, _point * 10, heartCount, coinCount);
        TeraJet.GameUtils.ExcuteFunction(() =>
        {
            if (canRevive)
            {
                revivePopup.OpenPopup();
                canRevive = false;
            }
            else
            {
                resultPopup.OpenPopup();
            }
            // TO DO Add coin and heart to the board  
        }, 1f);

    }
    private void OnPlayerRevive()
    {
        lifeCounter = maxLife;
        lifeCount.Reset();
        _startAble = true;
    }
    public void OnCollectHeart()
    {
        heartCount++;
        heartCollectText.text = heartCount.ToString();
    }
    public void OnCollectCoin()
    {
        coinCount++;
        coinCollectText.text = coinCount.ToString();
    }
    public void NextLevel()
    {
        _startAble = false;

        // TODO add fade fx
        TeraJet.GameUtils.ExcuteFunction(() =>
        {
            SoundFXManager.Instance?.PlayKnifeComplete();
            if (_circleFX == null)
                return;
            _circleFX.SetActive(true);
            _circleFX.transform.DOScale(_circleEndScale, _duration).OnComplete(() =>
            {
                _circleFX.SetActive(false);
                _circleFX.transform.localScale = Vector3.one * _circleStartScale;
                knifeCounter.Reset();
            });
        }, _duration);


        board.transform.DOMove(new Vector2(0, startPos.y + 10), _duration).SetDelay(_duration * 2).OnComplete(() =>
        {
            board.Reset();

            _currentLevel = _currentLevel + 1 >= levelSettings.Length ? 0 : _currentLevel + 1;

            levelText.text = "Level " + levelSettings[_currentLevel].level.ToString();
            levelText.transform.localScale = Vector3.one * 1.2f;
            levelText.transform.DOScale(1, _duration / 4).SetEase(Ease.InBack);

            board.Initialize(levelSettings[_currentLevel]);
            knifeCounter.Initalize(levelSettings[_currentLevel].numKnife);
            knifeCount = levelSettings[_currentLevel].numKnife;

            if (_currentKnife != null)
            {
                Destroy(_currentKnife.gameObject);
            }
            _currentKnife = Instantiate(_knifePrefab, new Vector2(_spawnPos.position.x, _spawnPos.position.y - 2), Quaternion.identity, transform);
            _currentKnife.transform.DOMoveY(_spawnPos.position.y, _duration);
        
            board.transform.DOMove(startPos, _duration).OnComplete(() =>
            {
                _startAble = true;
            });

        });
    }
}

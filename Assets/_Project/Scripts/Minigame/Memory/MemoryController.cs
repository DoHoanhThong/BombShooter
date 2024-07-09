using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ricimi;

public class MemoryController : Minigame
{
    public static System.Action<MemoryCard> OnFlipCard;

    public bool isTapable = false;
    [SerializeField] List<int> _cardused;
    [SerializeField] int _totalRandomCoinHeart;
    [SerializeField] GameObject _heartPrefab, _coinPrefab;
    [SerializeField]
    private int _targetPoint = 8;
    [SerializeField]
    private int _timeLimit =10;

    [SerializeField]
    private MemoryCard _cardPrefab;
    //data
    [SerializeField]
    private MemoryCardData[] _listData;

    [SerializeField]
    public Transform _spawnPoint; 
    [SerializeField]
    public float columnSpacing = 1f;
    [SerializeField]
    public float rowSpacing = 1f;

    [SerializeField]
    private float _waitTime = 0.75f;

    [SerializeField]
    private int numRows = 6;
    [SerializeField]
    private int numColumns = 5;

    //-------------UI-----------------
    [Header("UI")]
    [SerializeField]
    private RectTransform timeHand;
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField] TMP_Text _increaseTime;
    [SerializeField]
    private PopupOpener outOfTimePopup;
    [SerializeField]
    private PopupOpener winPopup;
    [SerializeField]
    private PopupOpener losePopup;
    
    [SerializeField]
    private float _duration = 0.25f;

    [SerializeField]
    TMP_Text _textCoin, _textHeart;
    //-------------UI-----------------

    [SerializeField]
    private float _distanceFromCenter = 1f;
    [SerializeField]
    private Transform _collectPoint;
    Coroutine _timeCount;
    private int _numHeart, _numCoin;
    private int _point = 0;
    private int _timeCounter = 0;
    private bool canRevive = true;
    private float timeHandZ = 0;
    private List<MemoryCard> _cards = new List<MemoryCard>();

    public MemoryCard _firstCard;
    public MemoryCard _secondCard;

    private int _numCard = 0;
    private void Start()
    {
        _numHeart =0;
        _numCoin = 0;
        _totalRandomCoinHeart = Random.Range(0, 3);
        _increaseTime.enabled = false;
        //doan tren thong add
        Initialize();
        _timeCounter = _timeLimit;
        timeText.text = _timeCounter.ToString();
        canRevive = true;
        OnFlipCard += OnPlayerFlipCard;
        RevivePopup.OnPlayerRevive += OnRevive;
        RevivePopup.OnPlayerExit += OnGameOver;
    }
    public void Initialize()
    {
        
        int[] idsave = new int[_totalRandomCoinHeart];
        for (int i = 0; i < 2; i++)
        {
            foreach (var data in _listData)
            {
                MemoryCard card = Instantiate(_cardPrefab, _spawnPoint);
                card.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                card.Initialize(this, data);
                _cards.Add(card);

                //check id de sinh coin or heart
                if (!_cardused.Contains(data.id))
                {
                    _cardused.Add(data.id);
                    if(Mathf.Abs(data.id - Random.Range(data.id, data.id + 4)) <= 1 && _totalRandomCoinHeart>0)
                    {
                        Debug.LogError(data.id);
                        idsave[idsave[0] == 0 ? 0 : 1] = data.id;
                        Transform g = card._corner.transform;
                        GameObject tmp = (Random.Range(0, 2) == 0) ? _heartPrefab : _coinPrefab;
                        GameObject t = Instantiate(tmp,g);
                        t.transform.GetComponent<RectTransform>().anchoredPosition=g.GetComponent<RectTransform>().anchoredPosition;
                        t.transform.localScale = Vector3.one * 0.8f;
                        t.transform.GetComponent<Image>().enabled = false;
                        _totalRandomCoinHeart -= 1;
                    }
                }
                // thong add doan tren
            }
        }
        _cardused.Clear();
        foreach (var t in idsave)
        {
            _cardused.Add(t);
        }
        _point = 0;
        //slider.value = 0;
        timeHandZ = 0;
        timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
        
        isTapable = true;
        ShuffleCards();
    }
    public  void OnDestroy()
    {
        OnFlipCard -= OnPlayerFlipCard;
        RevivePopup.OnPlayerRevive -= OnRevive;
        RevivePopup.OnPlayerExit -= OnGameOver;
    }
    public void ShuffleCards()
    {
        ShuffleDeck();
        StartCoroutine(DealCards());
    }
    private void ShuffleDeck()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            
            var temp = _cards[i];
            int randomIndex = Random.Range(i, _cards.Count);
            _cards[i] = _cards[randomIndex];
            _cards[randomIndex] = temp;
            _cards[i].gameObject.SetActive(true);
            
        }
        
    }
    IEnumerator DealCards()
    {
        int totalCards = _cards.Count;
        int cardsPerRow = totalCards / numRows;
        int remainingCards = totalCards % numRows;

        int cardCount = 0;

        float startXPos = -((cardsPerRow - 1) * columnSpacing) / 2f;
        float startYPos = -((numRows - 1) * rowSpacing) / 2f;

        for (int row = 0; row < numRows; row++)
        {
            int numCards = cardsPerRow;

            if (remainingCards > 0)
            {
                numCards++;
                remainingCards--;
            }

            for (int column = 0; column < numCards; column++)
            {
                if (cardCount >= _cards.Count)
                    break;

                var card = _cards[cardCount];
                float xPos = startXPos + (column * columnSpacing);
                float yPos = startYPos + (row * rowSpacing);

                card.GetComponent<RectTransform>().DOAnchorPos(new Vector3(xPos, yPos, 0), _duration);

                //card.transform.DORotate(new Vector3(0, 0, Random.Range(350, 370)), 0.25f, RotateMode.FastBeyond360);
                card.transform.DORotate(new Vector3(0, 0, 360), 0.25f, RotateMode.FastBeyond360);
                cardCount++;
                yield return new WaitForSeconds(1.5f / _cards.Count);
            }
        }

        _timeCount = StartCoroutine(TimeCounting());

    }
    
 
    public void OnPlayerFlipCard(MemoryCard card)
    {
        _numCard++;
        if (_numCard == 1)
        {
            _firstCard = card;
            
        }
        else
        {
            _secondCard = card;
            _numCard = 0;
            isTapable = false;
            if (_firstCard.data.id == _secondCard.data.id)
            {
                RenderIncreaseTime();
                TeraJet.GameUtils.ExcuteFunction(() =>
                {
                     // new
                    _firstCard.OnCollect(Vector2.right * _distanceFromCenter, _collectPoint.position);
                    _secondCard.OnCollect(-Vector2.right * _distanceFromCenter, _collectPoint.position);
                    //point++
                    _point++;
                   
                    MemoryPet.OnGetPoint?.Invoke();
                    
                    SoundFXManager.Instance?.PlayCardCorrect();
                    
                    //slider.DOValue(_point * 1.0f / _listData.Length, _duration);

                    isTapable = true;
                    _cards.Remove(_firstCard);
                    _cards.Remove(_secondCard);
                    _numCard = 0;
                    if (_cards.Count == 0)
                    {
                        if (_timeCount != null)
                        {
                            
                            StopCoroutine(_timeCount);
                        }
                        isTapable = false;
                        TeraJet.GameUtils.ExcuteFunction(() => 
                        {
                            _totalRandomCoinHeart = Random.Range(0, 3);
                            
                            Initialize();
                            //OnCompleted();
                        }, 1);
                        
                    }
                }, 1);
                
            }
            else
            {
                TeraJet.GameUtils.ExcuteFunction(() =>
                {
                    _firstCard?.FlipDown(_waitTime);
                   
                    _secondCard?.FlipDown(_waitTime);
                    
                    TeraJet.GameUtils.ExcuteFunction(() =>
                    {
                        isTapable = true;
                    }, _waitTime);

                    _firstCard = null;
                    _secondCard = null;
                }, 1);                        
            }
        }
    }
    private void OnRevive()
    {
        
        _timeCounter += 30;
        _timeLimit = 30;
        timeHandZ = 0;
        timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
        StopAllCoroutines();
        StartCoroutine(TimeCounting());
    }
    IEnumerator TimeCounting()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (_timeCounter > 0)
        {
            _timeCounter--;
            timeText.text = _timeCounter.ToString();
            timeText.transform.localScale = Vector3.one * 1.2f;
            timeText.transform.DOScale(1, _duration).SetEase(Ease.InBack);
            timeHandZ -= 360f / _timeLimit * 1.0f;
            timeHand.localRotation = Quaternion.Euler(new Vector3(0, 0, timeHandZ));
            yield return wait;
        }
        // Out of time;
        OnGameOver();
    }
    private void OnGameOver()
    {
        ResultContainer.Instance.SetResult( _point , _point * 2 * 100 + _timeCounter, _numHeart, _numCoin);
        if (canRevive)
        {
            outOfTimePopup.OpenPopup();
            canRevive = false;
        }
        else
        {
            losePopup.OpenPopup();
        }
    }
    private void OnCompleted()
    {
        StopAllCoroutines();
        ResultContainer.Instance.SetResult(3, _point * 2 * 100 + _timeCounter, _point * 2, _point * 2);
        winPopup.OpenPopup();
        
    }
    // thong add
    void RenderIncreaseTime()
    {
        _timeCounter += 5;
        timeText.text = _timeCounter.ToString();
        _increaseTime.enabled = true;
        RectTransform a = _increaseTime.transform.GetComponent<RectTransform>();
        float Y = a.anchoredPosition.y;
        a.DOAnchorPosY(a.anchoredPosition.y + 120, 1.2f).OnComplete(() =>
        {
            _increaseTime.enabled = false;
            a.anchoredPosition = new Vector3(a.anchoredPosition.x, Y);
        });
    }
    //thong add
    public void EnaOrDisableImageItem(MemoryCard card, bool isUpFlip)
    {
        Transform g = card._corner.transform;
        if (g.childCount == 0)
            return;
        foreach (var t in _cardused)
        {
            if (card.data.id == t)
            {
                g.GetChild(0).GetComponent<Image>().enabled = (isUpFlip)?true:false;  
            }
        }
    }
    //thong add
    public void CheckItem( MemoryCard card)
    {
        Transform tmp=null;
        if (card._corner.transform.childCount == 0)
            return; 
        tmp = card._corner.transform.GetChild(0);
        if (tmp.tag == "coinRender")
        {
            //tmp.SetParent(_coinRender.transform);
            tmp.DOMove(_textCoin.transform.position, 0.5f).OnComplete(() =>
            {
                
                _numCoin += 1;
                _textCoin.text= _numCoin.ToString();
                Destroy(tmp.gameObject);
            });

        }
        else if (tmp.tag == "heartRender")
        {
            //tmp.SetParent(_heartRender.transform);
            tmp.DOMove(_textHeart.transform.position, 0.5f).OnComplete(() =>
            {
                
                _numHeart += 1;
                _textHeart.text = _numHeart.ToString();
                Destroy(tmp.gameObject);
            });
        }
    }
}

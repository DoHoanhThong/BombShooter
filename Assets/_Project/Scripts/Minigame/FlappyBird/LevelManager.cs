using Ricimi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    Coroutine a;
    public bool isStart;
    public bool isEnd;
    [SerializeField] BirdController _birdController;
    [SerializeField] Text _coinText, _heartText;
    [SerializeField] GameObject _listPipeSpawn;
    [SerializeField] AudioClip _getPointSound;
    [SerializeField] int _score, _2score;
    [SerializeField] int _numHeart, _numCoin;
    [SerializeField] Text _textScore;
    [SerializeField] PopupOpener _losePopUp, outOfTimePopup;
    [SerializeField] GameObject _tutorial;
    public bool canRevive;
    public List<GameObject> listPipe;
    public float _speedofPipe;
    float _checktoIncreaseVl;
    // Start is called before the first frame update
    private static LevelManager _instance;
    public static LevelManager instance => _instance;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        canRevive = true;
        if (_instance == null)
        {
            //Debug.LogError("null");
            _instance = this;
            //DontDestroyOnLoad(this);
            return;
        }
        if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _coinText.text = "0";
        _heartText.text = "0";
        isEnd = false;
        _checktoIncreaseVl = 0;
        _speedofPipe = 3f;
        isStart = false;
        _score = 0;
        _textScore.text= _score.ToString();
        RevivePopup.OnPlayerRevive += OnRevive;
        RevivePopup.OnPlayerExit += GameOver;
    }
    private void OnDestroy()
    {
        RevivePopup.OnPlayerExit -= GameOver;
        RevivePopup.OnPlayerRevive -= OnRevive;
    }
    public void UpdateScore(int score)
    {
        SoundFXManager.Instance.PlaySound(_getPointSound);
        _score += score;
        _checktoIncreaseVl += score;
        _textScore.text= _score.ToString();
        //if(_checktoIncreaseVl >= 10)
        //{
        //    _checktoIncreaseVl = 0;
        //    _speedofPipe += 0.5f;
        //}
        if(_score > _2score)
        {
            _2score = score;
            PlayerPrefs.SetInt("2Sflappybird",_2score);
        }
    }
    public void UpdateCoin()
    {
        _numCoin += 1;
        _coinText.text = _numCoin.ToString();
    }
    public void UpdateHeart()
    {
        _numHeart += 1;
        _heartText.text = _numHeart.ToString();
    }
    public void GameOver()
    {
        ResultContainer.Instance.SetResult(_score, PlayerPrefs.GetInt("2Sflappybird"), _numHeart, _numCoin);
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(1f);
        if (canRevive)
        {
            outOfTimePopup.OpenPopup();
            canRevive = false;
        }
        else
        {
            _losePopUp.OpenPopup();
        }

        
    }
    void OnRevive()
    {
        Physics2D.IgnoreLayerCollision(0, 10, false);
        if (_tutorial != null)
        {
            _tutorial.SetActive(true);
        }
        isStart = false;
        isEnd=false;
        foreach(var t in listPipe)
        {
            if(t!=null && t.activeSelf)
            {
                t.SetActive(false);
            }
        }
        _birdController.Revive();
    }
}

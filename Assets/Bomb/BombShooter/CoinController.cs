using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    Rigidbody2D _rigi;
    Coroutine a;
    [SerializeField] Text _text;
    public int _coinValue;
    public bool jumpLeft;
    [SerializeField] float _jumpForce;
    bool _callInStart;
    Image _im;
    [SerializeField] AudioClip _collectSound;
    [SerializeField] AudioClip _sound;
    void Awake()
    {
        _callInStart = false;
        _rigi = this.GetComponent<Rigidbody2D>();
        _im = this.GetComponent<Image>();
        int a = Random.Range(0, 100);
        if (a <= 33)
        {
            _coinValue = 10;
        }
        else if (a <= 66)
        {
            _coinValue = 15;
        }
        else
        {
            _coinValue = 20;
        }
        _text.text = "+" + _coinValue;
        Jump();
    }
    private void Start()
    {
        Jump();
        _callInStart = true;
        Debug.LogError("enableInStart!");
    }
    private void OnEnable()
    {
        if (!_callInStart)
        {
            Debug.LogError("cant Jump!");
            return;
        }
        Debug.LogError("jUMP in OnEnable!");
        Jump();
    }
    private void OnDestroy()
    {
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
    }
    void Jump()
    {
        Debug.LogError("CoinJump!");
        Vector2 jump = new Vector2(((jumpLeft) ? -1 : 1) * _jumpForce / 2, _jumpForce);
        Debug.LogError("forceJump:" + jump);
        _rigi.AddForce(jump);
    }
    private void OnDisable()
    {
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
        _rigi.gravityScale = 0.8f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioController.Instant.PlaySound(_collectSound);
            _rigi.gravityScale = 0;
            _rigi.velocity = Vector2.zero;
            BombShooterController.Instance.AddCoinOfPlayer(_coinValue);
            if (a != null)
            {
                StopCoroutine(a);
            }
            a = StartCoroutine(DeactiveAftefTime());
        }
    }
    IEnumerator DeactiveAftefTime()
    {
        _im.enabled = false;
        _text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _text.enabled = false;
        _im.enabled = true;
        this.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            AudioController.Instant.PlaySound(_sound);
        }
    }
}

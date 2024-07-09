using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdController : MonoBehaviour
{
    Coroutine s;
    Tween a;
    Rigidbody2D _rigi;
    Animator _anim;
    [SerializeField] CanvasGroup _canGr;
    [SerializeField] GameObject _tuto;
    [SerializeField] GameObject _coinRender, _heartRender;
    [SerializeField] float _jumpForce;
    [SerializeField] AudioClip _tapSound, _hitSound;
    float _rotateZ;
    [SerializeField] float _speedRotate;
    float maxRotation = 0.523598f * Mathf.Rad2Deg;
    private void Awake()
    {
        
        _anim = this.GetComponentInChildren<Animator>();
        _rotateZ = 0;
        _rigi = this.GetComponent<Rigidbody2D>();
        _rigi.gravityScale = 0;
        RevivePopup.OnPlayerRevive += Revive;
    }
    private void OnDestroy()
    {
        RevivePopup.OnPlayerRevive -= Revive;
    }
    private void Update()
    {

        if (LevelManager.instance.isEnd)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _rigi.velocity = new Vector2(_rigi.velocity.x, 0);
            SoundFXManager.Instance.PlaySound(_tapSound);
            if (!LevelManager.instance.isStart)
            {
                _tuto.SetActive(false);
                LevelManager.instance.isStart = true;

                _rigi.gravityScale = 2f;
            }
            Jump();
        }

    }

    private void RotateBird()
    {
        if (!LevelManager.instance.isStart)
            return;

        if (_rigi.velocity.y <= 0)
        {

            _rotateZ -= Time.deltaTime * (_speedRotate);
            if (_rotateZ < -5)
            {
                _rotateZ = -5;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, _rotateZ);
    }

    void Jump()
    {
        _rigi.velocity = Vector2.up * _jumpForce;
        _rotateZ = 25;
        this.transform.rotation = Quaternion.Euler(0, 0, _rotateZ);
        if (a != null)
        {
            a.Kill();
        }
        a = this.transform.DORotate(new Vector3(0, 0, -5 * 0.01745329f * Mathf.Rad2Deg), 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.instance.isEnd)
            return;
        if (collision.gameObject.tag == "score")
        {
            LevelManager.instance.UpdateScore(1);
            //Destroy(collision.transform.parent.gameObject, 4);
            //LevelManager.instance.listPipe.Remove(collision.transform.parent.gameObject);
        }
        if (collision.gameObject.tag == "heartItem")
        {
            collision.gameObject.transform.DOMove(_heartRender.transform.position, 1).OnComplete(() =>
            {
                Destroy(collision.gameObject);
            });
            LevelManager.instance.UpdateHeart();
        }
        if (collision.gameObject.tag == "coinItem")
        {
            collision.gameObject.transform.DOMove(_coinRender.transform.position, 1).OnComplete(() =>
            {
                Destroy(collision.gameObject);
            });
            LevelManager.instance.UpdateCoin();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LevelManager.instance.isEnd)
            return;

        if (collision.gameObject.tag == "pipe" || collision.gameObject.tag == "ground")
        {
            if (a != null)
            {
                a.Kill();
            }
            SoundFXManager.Instance.PlaySound(_hitSound);

            _anim.enabled = false;
            _rigi.gravityScale = 0;
            _rigi.velocity = Vector2.zero;
            LevelManager.instance.isEnd = true;
            if (s != null)
            {
                StopCoroutine(s);
                s = null;
            }
            Stunned();
        }

    }
    void Stunned()
    {

        _canGr.DOFade(0.65f, 0.1f).OnComplete(() =>
        {
            _canGr.DOFade(0, 0.1f);
        });
        LevelManager.instance.GameOver();
    }
    public void Revive()
    {
        _anim.enabled = true;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = new Vector2(-0.38f, 0);
        _rigi.gravityScale = 0;
        _rigi.velocity = Vector2.zero;
    }
}

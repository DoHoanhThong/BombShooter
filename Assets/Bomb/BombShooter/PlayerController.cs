using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] RotateWheel _rtw;
    public static System.Action StopTween;
    Coroutine b;
    Tween a;
    Tween other1;
    [SerializeField] float _timeTele;
    Rigidbody2D _rigi;
    Vector2 _screen;
    [SerializeField] SpriteRenderer _thisSprite;
    float _thisWidth, _thisHeight;
    [SerializeField] float _distancce;
    void Start()
    {
        _screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _thisWidth = _thisSprite.bounds.size.x / 2;
        _rigi = this.GetComponent<Rigidbody2D>();
        b = StartCoroutine(MoveLoop());
        StopTween += StopCRT;
    }
    private void OnDestroy()
    {
        SpawnBullet.ClickButton -= ClickScreen;

    }
    private void Update()
    {
        if (BombShooterController.Instance.isEnd)
            return;
        if (!BombShooterController.Instance.isStart)
            return;
        if (Input.GetMouseButton(0))
        {
            ClickScreen();
        }
    }
    void ClickScreen()
    {

        if (BombShooterController.Instance.isDrag)
            return;
        float x = 0;
        if (GetMousePos().x > _screen.x - _thisWidth)
        {
            x = _screen.x - _thisWidth;
        }
        else if (GetMousePos().x < -_screen.x + _thisWidth)
        {
            x = -_screen.x + _thisWidth;
        }
        else
        {
            x = GetMousePos().x;
        }
        if (a != null)
        {
            a.Kill();
            a = null;
        }
        a = this.transform.DOMoveX(x, _timeTele);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bom")
        {
            _rigi.velocity = Vector2.zero;
            if (a != null)
            {
                a.Kill();
                a = null;
            }
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    IEnumerator MoveLoop()
    {
        float x = this.transform.position.x;
        while (!BombShooterController.Instance.isStart)
        {
            other1 = this.transform.DOMoveX(x + _distancce, 1.5f).OnComplete(() =>
            {
                other1 = this.transform.DOMoveX(x - _distancce, 1.5f);
            });
            yield return new WaitForSeconds(3f);
        }
    }
    void StopCRT()
    {
        if (b != null)
        {
            StopCoroutine(b);
            b = null;
        }
        if (other1 != null)
        {
            other1.Kill();
            other1 = null;
        }
    }
}

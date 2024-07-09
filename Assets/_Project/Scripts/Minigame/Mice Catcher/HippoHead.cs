using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HippoHead : MonoBehaviour
{
    [SerializeField]
    HappyHippoController _hipController;
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private GameObject _mouth;
    [SerializeField]
    private ParticleSystem _rippoFX;
    [SerializeField]
    private PointText _text;
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private float _attackRange = 1;

    private bool _isAttacking;
    private Vector3 _originPos;

    public int point = 0;
    private void Awake()
    {
        _originPos = transform.position;
        _isAttacking = false;
        point = 0;
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Attack();
    //    }
    //}
    public void Attack()
    {
        if (_isAttacking)
            return;
        _isAttacking = true;
        StartCoroutine(AttackCoro());

        //SoundManager.Instance.PlayHippoOpenMouth();
    }
    IEnumerator AttackCoro()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        _mouth.SetActive(true);
        _collider.isTrigger = true;
        while (transform.position != _originPos + Vector3.up * _attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, _originPos + Vector3.up * _attackRange, _speed * Time.deltaTime);
            yield return wait;
        }
        _rippoFX.Play();
        //attack done
        _mouth.SetActive(false);
        _collider.isTrigger = false;
        while (transform.position != _originPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _originPos, _speed * Time.deltaTime);
            yield return wait;
        }
        _isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HappyHippoBall ball = collision.GetComponent<HappyHippoBall>();
        if (ball != null)
        {
            if (ball.ballType == HappyHippoBall.BallType.RED)
            {
                point += 2;
                // update coin, heart
                CheckItem(ball);
                Instantiate(_text, ball.transform.position, Quaternion.Euler(0, 0, 0), transform.parent).Plus(2, true);
            }
            else
            {
                point = point - 1 > 0 ? point - 1 : 0;
                Instantiate(_text, ball.transform.position, Quaternion.Euler(0, 0, 0), transform.parent).Minus(1);
            }
            HappyHippoController.OnPlayerGetPoint?.Invoke();

            Destroy(ball.gameObject);
        }
    }
    void CheckItem(HappyHippoBall ball)
    {
        if (ball.transform.childCount > 0)
        {
            KnifeCollectItem a = ball.transform.GetChild(0).GetComponent<KnifeCollectItem>();
            if (a.type == KnifeCollectItem.Type.Heart)
            {

                _hipController.coin += 1;
            }
            else
            {

                _hipController.heart += 1;
            }
            a.OnCollect();
        }
        _hipController.AddItem();
    }
}

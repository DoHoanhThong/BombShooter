using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCTL : MonoBehaviour
{
    [SerializeField] bool _L, _R;
    Coroutine a;
    Transform _leftPos;
    public TypeBom _type;
    [SerializeField] float _jumpForce;
    Vector3 lastVelocity;
    Rigidbody2D _rigi;
    Vector2 _screen;
    [SerializeField] bool _left;
    public bool x2;
    Vector2 collisionPoint;
    public bool jumpLeft;
    [SerializeField] AudioClip _explosion;
    public void SetJumpLeft(bool a)
    {
        jumpLeft = a;
    }
    private void Awake()
    {
        _screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _rigi = this.GetComponent<Rigidbody2D>();
        _rigi.gravityScale = 0;
        _leftPos = BombShooterController.Instance.listTransform[0];
    }
    void Start()
    {
        if (this.transform.position.x < 0)
        {
            _left = true;
        }
        else
        {
            _left = false;
        }
        if (!x2)
        {
            BeforeFall();
        }
        else
        {
            X2();
        }
    }
    public enum TypeBom
    {
        circle,
        grenade,
        bigbomb,
        rocket,
        biggrenade
    }
    private void OnDestroy()
    {
        if (a != null)
        {
            StopCoroutine(a);
        }
        if (_type == TypeBom.rocket)
            return;
        BombShooterController.Instance.bom_exist = false;

        if (_type != TypeBom.circle && _type != TypeBom.grenade)
            return;
        int random = Random.Range(0, 200);
        if (random <= 60)
        {
            SpawnCoin.Instance.spawnCoin(this.transform.position, 1);
        }
        else if (random <= 100)
        {
            SpawnCoin.Instance.spawnCoin(this.transform.position, 2);
        }
    }
    void FixedUpdate()
    {
        lastVelocity = _rigi.velocity;
    }
    void X2()
    {
        int a = Random.Range(0, 100);
        Vector2 jump = (jumpLeft) ? new Vector2(-_jumpForce * 0.5f, _jumpForce * 1.1f) : new Vector2(_jumpForce * 0.5f, _jumpForce * 1.1f);
        _rigi.AddForce(jump);
        _rigi.gravityScale = 0.5f;
    }
    void BeforeFall()
    {
        if (BombShooterController.Instance.isEnd)
            return;
        int a = Random.Range(0, 100);
        Vector2 pos = new Vector2((_left) ? -_screen.x + 1.05f : _screen.x - 1.05f, _leftPos.position.y);
        Vector2 jump = (a < 50) ? new Vector2(_jumpForce * 0.5f, _jumpForce) : new Vector2(-_jumpForce * 0.5f, _jumpForce);
        this.transform.DOMoveX(pos.x, 2f).OnComplete(() =>
        {
            _rigi.AddForce(jump);
            _rigi.gravityScale = 0.5f;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BombShooterController.Instance.isEnd)
        {
            _rigi.gravityScale = 0;
            _rigi.velocity = Vector2.zero;
            return;
        }
        if (collision.gameObject.tag == "Player")
        {

            collisionPoint = collision.ClosestPoint(transform.position);
            AudioController.Instant.PlaySound(_explosion);
            SpawnSmokeVFX.Instance.Spawn(collisionPoint);
            _rigi.gravityScale = 0;
            _rigi.velocity = Vector3.zero;
            CameraShake.ShakeScreen.Invoke();
            BombShooterController.Instance.isEnd = true;
            AudioController.Instant.DisabledBgMusic();
            a = StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {

        yield return new WaitForSeconds(1);
        BombShooterController.End.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (BombShooterController.Instance.isEnd)
            return;
        if (collision.gameObject.tag == "blockLeft")
        {
            _L = true;
        }
        if (collision.gameObject.tag == "blockRight")
        {
            _R = true;
        }
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        if (Mathf.Approximately(Vector3.Dot(direction, Vector3.up), 1f))
        {
            Vector2 bonus = Vector2.zero;
            if (_L)
            {
                bonus = new Vector2(1, 1);
            }
            else if(_R)
            {
                bonus = new Vector2(-1, 1);
            }
            direction = (Vector3)bonus.normalized;
        }
        _rigi.velocity = direction * Mathf.Max(speed, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    [SerializeField]
    private bool _isRedKnife = true;
    [SerializeField]
    private Vector2 _throwForce;
    [SerializeField]
    private float _gravityScale = 1;
    [SerializeField]
    private float _bound = 15f;
    [SerializeField]
    private GameObject _hitCircleFX;
    [SerializeField]
    private GameObject _hitKnifeFX;
    
    [SerializeField]
    private bool _isActive = true;
    [SerializeField]
    private Rigidbody2D _knifeRb;
    [SerializeField]
    private Collider2D _knifeCollider;
    [SerializeField]
    private TrailRenderer _trail;
    private void Awake()
    {
        _knifeRb.bodyType = RigidbodyType2D.Kinematic;
        _knifeCollider.isTrigger = _isActive;
        //_trail = GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.y) > _bound)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Throw()
    {
        _knifeRb.bodyType = RigidbodyType2D.Dynamic;
        _knifeCollider.isTrigger = false; ;
        _knifeRb.AddForce(_throwForce, ForceMode2D.Impulse);
        _knifeRb.gravityScale = _gravityScale;
        if (_trail != null) _trail.gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive)
            return;
        
        _isActive = false;
        var board = collision.gameObject.GetComponent<KnifeBoard>();
        if (board != null)
        {
            _knifeRb.velocity = Vector2.zero;
            _knifeRb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.transform);
            SoundFXManager.Instance?.PlayKnifeHit();
            Destroy( Instantiate(_hitCircleFX, collision.contacts[0].point, Quaternion.identity).gameObject, 2);
            //_hitCircleFX.SetActive(true);
            board.Shake();
            ThrowKnifeController.OnKnifeHitSuccess?.Invoke();
        }
        else
        {
            if (collision.gameObject.GetComponent<KnifeScript>())
            {
                SoundFXManager.Instance?.PlayKnifeHitIron();
                _knifeRb.velocity = new Vector2(_knifeRb.velocity.x, -2);

                _knifeCollider.enabled = false;
                _knifeRb.gravityScale = _gravityScale * 10;
                var hitFX = Instantiate(_hitKnifeFX, collision.contacts[0].point, Quaternion.identity);
                //hitFX.OnSpawn(collision.contacts[0].point);

                if (_trail != null)
                    _trail.gameObject.SetActive(false);
                ThrowKnifeController.OnKnifeHitFail?.Invoke();
                Destroy(hitFX.gameObject, 2);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive)
            return;
        var item = collision.gameObject.GetComponent<KnifeCollectItem>();
        if (item != null)
        {
            item.OnCollect();
            if (item.type == KnifeCollectItem.Type.Heart)
            {
                ThrowKnifeController.OnHitHeart?.Invoke();
            }
            else
            {
                ThrowKnifeController.OnHitCoin?.Invoke();
            }
        }
    }

}

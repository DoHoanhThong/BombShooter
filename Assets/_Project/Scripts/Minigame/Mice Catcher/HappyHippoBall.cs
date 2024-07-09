using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyHippoBall : MonoBehaviour
{
    public BallType ballType;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public enum BallType
    {
        RED,
        BLUE,
    }
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _lastVelocity;
    [SerializeField]
    private float _speed = 2;
    private void Update()
    {
        _lastVelocity = _rb.velocity;
        if (_rb.velocity == Vector2.zero)
        {
            _rb.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * _speed;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg);
            HandFlip();
        }
    }
    
    private void OnDestroy()
    {
        HippoBallSpawner.OnDestroyHippoBall?.Invoke(ballType == BallType.RED);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = _lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
        _rb.velocity = direction.normalized  * _speed;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        HandFlip();
    }
    private void HandFlip()
    {
        spriteRenderer.flipY = transform.rotation.z < -90 || transform.rotation.z > 90;

    }
}

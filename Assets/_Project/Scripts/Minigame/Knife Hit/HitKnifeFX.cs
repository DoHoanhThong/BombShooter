using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HitKnifeFX : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _pieces;
    [SerializeField]
    private bool _isReverse = false;
    [SerializeField]
    private float _radius = 0.3f;
    [SerializeField]
    private float _fadeDuration = 0.5f;
    public void OnSpawn(Vector2 collisionPos)
    {
        float _angle;
        foreach (var piece in _pieces)
        {
            if (collisionPos.y * (_isReverse? -1 : 1) >= 0)
            {
                _angle = Random.Range(-90, 90);
                piece.transform.position = new Vector2(Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius + collisionPos.x, Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius + collisionPos.y);
            }
            else
            {
                _angle = Random.Range(-90, -270);
                piece.transform.position = new Vector2(Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius + collisionPos.x, Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius + collisionPos.y);
            }


            Vector2 direction = collisionPos - (Vector2)piece.transform.position;
            piece.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg);

            piece.DOFade(0, _fadeDuration).OnComplete(()=>Destroy(piece.gameObject));
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.Rendering;
using DG.Tweening;

public class CatMovement : MonoBehaviour
{
    public float _moveSpeed = 5f;
    public float _scaleSpeed = 200;
    public bool canMove = false;

    [SerializeField]
    private SkeletonAnimation catSpine;
    [SerializeField]
    private SortingGroup sortingGroup;
    [SerializeField]
    private GameObject shadow;
    [SerializeField]
    private Vector2 _center = Vector2.zero;
    [SerializeField]
    private float _maxWidth = 30;
    [SerializeField]
    private float _maxHeight = 2;
    [SerializeField]
    private float _maxX = 5;
    [SerializeField]
    private Vector2[] boundPoints;
    [SerializeField]
    private Bound bound;
    
    private Vector2 _targetPosition;
    private float _xTarget;
    private float _yTarget;

    private void Start()
    {
        canMove = true;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveToTargetPosition();
            HandleScaleAndLayer();
        }
        
    }

    private void MoveToTargetPosition()
    {
        _targetPosition = new Vector2(_xTarget, _yTarget);
        if ((Vector2) transform.position != _targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            canMove = true;
        }
        LookTarget();
    }
    public void RandomX()
    {
        float minX = transform.position.x - _maxX > (-_maxWidth / 2.0f + _center.x) ? transform.position.x - _maxX : -_maxWidth / 2.0f + _center.x;
        float maxX = transform.position.x + _maxX < _maxWidth / 2.0f + _center.x ? transform.position.x + _maxX : _maxWidth / 2.0f + _center.x;
        _xTarget = Random.Range(minX, maxX);              
    }
    public void RandomY()
    {
        //_yTarget = Random.Range(-_maxHeight / 2.0f + _center.y, _maxHeight / 2.0f + _center.y);
        _yTarget = Random.Range(bound.bottomBound, bound.topBound);
        //Debug.Log("RandomY");
    }
    public void LookTarget()
    {
        if (_xTarget < transform.position.x)
        {
            catSpine.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            catSpine.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public bool IsMoveDone()
    {
        return transform.position.x == _xTarget;
    }
    public bool TowardToPosition(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.fixedDeltaTime);
        return (Vector2) transform.position == target;
    }

    public void HandleScaleAndLayer()
    {
        // TO DO hardcode here
        var value = (transform.position.y - bound.bottomBound) * 1.0f / (3.26f - bound.bottomBound);
        sortingGroup.sortingOrder = (int)((1f - value) * 10);
        transform.localScale = Vector3.MoveTowards(transform.localScale, (1f - value * 0.5f) * Vector3.one, Time.deltaTime * _scaleSpeed);
    }
    public void OnDrag()
    {
        canMove = false;
        sortingGroup.sortingOrder = 99;
        //transform.localScale = Vector3.one;
        transform.DOScale(Vector3.one, 0.25f);
        shadow.SetActive(false);
    }
    public void OnEndDrag()
    {
        canMove = true;
        //sortingGroup.sortingOrder = 99;
        //transform.localScale = Vector3.one;
        shadow.SetActive(true);
    }


    private Vector2 GetRandomPointInPolygon(Vector2[] points)
    {
        List<Triangle> triangles = TriangulatePolygon(points);
        Triangle randomTriangle = SelectRandomTriangle(triangles);
        return GetRandomPointInTriangle(randomTriangle);
    }

    private List<Triangle> TriangulatePolygon(Vector2[] polygon)
    {
        List<Triangle> triangles = new List<Triangle>();

        for (int i = 1; i < polygon.Length - 1; i++)
        {
            triangles.Add(new Triangle(polygon[0], polygon[i], polygon[i + 1]));
        }

        return triangles;
    }

    private Triangle SelectRandomTriangle(List<Triangle> triangles)
    {
        float totalArea = 0;
        foreach (var triangle in triangles)
        {
            totalArea += triangle.Area;
        }

        float randomValue = Random.Range(0, totalArea);
        float accumulatedArea = 0;

        foreach (var triangle in triangles)
        {
            accumulatedArea += triangle.Area;
            if (randomValue <= accumulatedArea)
            {
                return triangle;
            }
        }

        return triangles[triangles.Count - 1];
    }

    private Vector2 GetRandomPointInTriangle(Triangle triangle)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        float sqrtR1 = Mathf.Sqrt(r1);
        float x = (1 - sqrtR1) * triangle.A.x + (sqrtR1 * (1 - r2)) * triangle.B.x + (sqrtR1 * r2) * triangle.C.x;
        float y = (1 - sqrtR1) * triangle.A.y + (sqrtR1 * (1 - r2)) * triangle.B.y + (sqrtR1 * r2) * triangle.C.y;

        return new Vector2(x, y);
    }

    float GetRandomYInPentagonWithKnownX(Vector2[] vertices, float x)
    {
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 a = vertices[i];
            Vector2 b = vertices[(i + 1) % vertices.Length];

            if ((a.x <= x && x <= b.x) || (b.x <= x && x <= a.x))
            {
                float y = Mathf.Lerp(a.y, b.y, (x - a.x) / (b.x - a.x));
                minY = Mathf.Min(minY, y);
                maxY = Mathf.Max(maxY, y);
            }
        }

        return Random.Range(minY, maxY);
    }
}

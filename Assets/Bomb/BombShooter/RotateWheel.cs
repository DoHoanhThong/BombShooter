using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _player;
    Quaternion q;
    float _current;
    float _next;
    [SerializeField] bool _isMoveLeft;
    Coroutine a;
    private float previousX;
    float currentX;
    private void Start()
    {
        previousX = _player.position.x;
        q = this.transform.rotation;
    }
    private void OnDestroy()
    {
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
    }
    private void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        float deltaAngle = _speed * Time.deltaTime;
        currentX = _player.position.x;
        q = this.transform.rotation;
        if (currentX - previousX > 0.05f)
        {
            //Debug.LogError(currentX - previousX);
            q *= Quaternion.Euler(0, 0, -deltaAngle);
            //Debug.LogError(">");
        }
        else if (currentX - previousX <  -0.05f)
        {
            //Debug.LogError(currentX - previousX);
            q *= Quaternion.Euler(0, 0, deltaAngle);
            //Debug.LogError("<");
        }
        else return;
        this.transform.rotation = q;
        previousX = currentX;
    }

    //IEnumerator rotate()
    //{
    //    q = this.transform.rotation;
    //    while (true)
    //    {
    //        _next = _player.position.x;
    //        if (_current > _next)
    //        {

    //            q.z = q.z + _speed * Time.deltaTime;
    //            _isMoveLeft = true;
    //            Debug.LogError("isMoveLeft!");
    //        }
    //        else if(_current < _next)
    //        {
    //            _isMoveLeft = false;
    //            q.z = q.z - _speed * Time.deltaTime;
    //            Debug.LogError("isMoveRight!");
    //        }
    //        this.transform.rotation = q;
    //        _current = _next;
    //        yield return new WaitForSeconds(1/Application.targetFrameRate);
    //    }
    //}

}

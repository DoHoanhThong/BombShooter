using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTargetLv2 : CircleTarget
{
    [SerializeField]
    private float _changeDirTime = 5f;
    [SerializeField]
    private float _slowdownSpeed = 3;

    private float _timeCounter = 0;

    private float _originSpeed = 0;
    public override void Start()
    {
        base.Start();
        _originSpeed = speed;
    }
    public override void Update()
    {
        
        if (_timeCounter > _changeDirTime)
        {

            speed -= _slowdownSpeed * Time.deltaTime;
            if (_originSpeed > 0 && speed < -_originSpeed)
            {
                _timeCounter = 0;
                _originSpeed *= -1;
                _slowdownSpeed *= -1;
            }
            if (_originSpeed < 0 && speed > -_originSpeed)
            {
                _timeCounter = 0;
                _originSpeed *= -1;
                _slowdownSpeed *= -1;
            }
        }
        else
        {
            _timeCounter += Time.deltaTime;
        }
        base.Update();
    }
}

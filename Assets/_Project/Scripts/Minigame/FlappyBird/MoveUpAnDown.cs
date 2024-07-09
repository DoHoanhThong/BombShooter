using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAnDown : MonoBehaviour
{
    [SerializeField] float _topPos, _bottomPos;
    [SerializeField] float _speed;
    bool _isMoveUp;
    // Start is called before the first frame update
    void Start()
    {
        _isMoveUp = true;
       
    }
 
    private void Update()
    {
        Vector2 pos = this.transform.position;
        if (!_isMoveUp)
        {
            if (this.transform.position.y > _bottomPos)
            {
                pos = new Vector2(pos.x, pos.y - _speed * Time.deltaTime);
                this.transform.position = pos;
            }
            else
                _isMoveUp = true;
        }
        else
        {
            if (this.transform.position.y < _topPos)
            {
                pos = new Vector2(pos.x, pos.y + _speed * Time.deltaTime);
                this.transform.position = pos;
            }
            else
                _isMoveUp = false;
        }
    }
    
}

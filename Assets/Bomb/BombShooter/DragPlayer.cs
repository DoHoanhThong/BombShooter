using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragPlayer : MonoBehaviour
{
    Vector3 _dragOffset;
    Vector2 _screen;
    [SerializeField] float _speed;
    [SerializeField] SpriteRenderer _sprite;
    float _thisWidth;
    private void Start()
    {
        _screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _thisWidth = _sprite.bounds.size.x / 2;
    }
    private void OnMouseDown()
    {
        BombShooterController.Instance.isDrag = true;
        _dragOffset = this.transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        if (BombShooterController.Instance.isEnd)
            return;
        if (!BombShooterController.Instance.isStart)
            return;
        if (BombShooterController.Instance.isTele)
            return;
        Vector3 pos = this.transform.position;
        pos.x = GetMousePos().x + _dragOffset.x;
        if (pos.x > _screen.x - _thisWidth)
        {
            pos.x = _screen.x - _thisWidth;
        }
        else if (pos.x < -_screen.x + _thisWidth)
        {
            pos.x = -_screen.x + _thisWidth;
        }
        this.transform.position = Vector3.Lerp(this.transform.position, pos, _speed);
        //this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() + _dragOffset, _speed*Time.deltaTime);
    }
    private void OnMouseUp()
    {
        BombShooterController.Instance.isDrag = false;
    }
    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

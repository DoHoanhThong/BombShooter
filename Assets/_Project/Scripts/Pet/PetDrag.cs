using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDrag : DragObject
{
    public bool isPlaying = false;
    [SerializeField]
    private float forceMultiplier = 10f;
    [SerializeField]
    private float circleRadius = 2;

    private Rigidbody2D _rb;
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private LayerMask toyLayer;
    private CatMovement movement;

    private Vector2 startPos;
    private Vector2 endPos;

    public PlayToy currentToy;

    public override void Start()
    {
        base.Start();
        movement = GetComponent<CatMovement>();
        _rb = GetComponent<Rigidbody2D>();
        //_collider = GetComponent<Collider2D>();
    }

    public override void OnStartDrag()
    {
        if (isPlaying)
            return;
        base.OnStartDrag();
        movement.OnDrag();

        //_collider.enabled = false;
    }
    public override void OnDrag()
    {
        if (!isPlaying)
        {
            startPos = transform.position;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, followSpeed * Time.deltaTime);
            endPos = newPosition;

            CheckPlayObj();
        }
    }

    public override void OnEndDrag()
    {
        if (isPlaying)
            return;
        base.OnEndDrag();
        //_collider.enabled = true;
        if (CheckPlayObjAround())
            return;
        if (transform.position.y < -1)
        {
            movement.canMove = true;
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _collider.isTrigger = false;
            Vector2 direction = endPos - startPos;
            _rb.AddForce(direction.normalized * forceMultiplier);
        }
    }
    private bool CheckPlayObjAround()
    {

        if (currentToy != null)
        {
            //currentToy.OnPlay(GetComponent<PetController>());
            //currentToy = null;
            return true;
        }
        return false;
    }
    private void CheckPlayObj()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, toyLayer);
        if (hit)
        {
            PlayToy toy = hit.collider.GetComponent<PlayToy>();
            if (toy != null)
            {
                if (currentToy != toy)
                {
                    if (currentToy != null)
                        currentToy.OnPointerExit();
                    currentToy = toy;
                    currentToy.OnPointerEnter();
                }
            }

        }
        else
        {
            if (currentToy != null)
            {
                currentToy.OnPointerExit();
                currentToy = null;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //movement.canMove = true;
            movement.OnEndDrag();
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = Vector2.zero;
            _collider.isTrigger = true;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isDragging)
    //    {
    //        var toy = collision.gameObject.GetComponent<PlayToy>();
    //        if (toy != null && currentToy != toy)
    //        {
    //            currentToy = toy;
    //            currentToy.OnPointerEnter();
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (isDragging)
    //    {
    //        var toy = collision.gameObject.GetComponent<PlayToy>();
    //        if (toy != null && currentToy == toy)
    //        {
    //            currentToy.OnPointerExit();
    //            currentToy = null;
    //        }
    //    }
    //}
}

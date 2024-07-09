using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HouseTrash : DragObject
{
    public bool isAble = true;
    //[SerializeField]
    //private Rigidbody2D _rb;
    //[SerializeField]
    //private Collider2D _col;
    [SerializeField]
    private Image clonePrefab;    
    [SerializeField]
    private ParticleSystem collectFX;
    [SerializeField]
    private float duration = 0.5f;

    private Image clone;
    private float originScale;
    private Vector2 originPos;

    public override void Start()
    {
        base.Start();
        originPos = transform.position;
        originScale = transform.localScale.x;
        isAble = true;
    }
    public void OnSpawn(Transform cloneHolder)
    {
        clone = Instantiate(clonePrefab, cloneHolder);
        //clone.transform.SetParent(cloneHolder);
        clone.gameObject.SetActive(false);
    }
    public override void OnStartDrag()
    {       
        if (RecycleBin.Instance.currentTrash == null)
        {
            RecycleBin.Instance.OnStartHoldTrash(this);
            base.OnStartDrag();
            //_rb.bodyType = RigidbodyType2D.Kinematic;
            clone.transform.position = Input.mousePosition;
            clone.gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            GameController.Instance.isSwipeable = false;
        }
    }
    public override void OnDrag()
    {
        base.OnDrag();
        
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        //Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        clone.transform.position = mousePosition;
    }
    public override void OnEndDrag()
    {
        if (RecycleBin.Instance.currentTrash == this)
        {
            base.OnEndDrag();
            //_rb.bodyType = RigidbodyType2D.Dynamic;
            clone.gameObject.SetActive(false);
            //_col.isTrigger = false;
            transform.localScale = Vector3.one * originScale;
            transform.DOMove(originPos, duration);
            
            GameController.Instance.isSwipeable = true;
            RecycleBin.Instance.OnStopHoldTrash();
        }
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (isAble)
    //    {
    //        _rb.bodyType = RigidbodyType2D.Kinematic;
    //        _col.isTrigger = true;
    //        _rb.velocity = Vector2.zero;
    //    }
    //}
    public void OnCollect()
    {
        TrashSpawner.OnTrashClean?.Invoke();
        CoinAnim.Instance.AddCoins(Camera.main.WorldToScreenPoint(transform.position), 1, 1);
        Destroy(Instantiate(collectFX, transform.position, collectFX.transform.rotation).gameObject, 2);
        Destroy(clone.gameObject);
        Destroy(gameObject);
    }

}

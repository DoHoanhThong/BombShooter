using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool isDragging = false;
    public bool isTapping = false;
    
    [SerializeField]
    protected float followSpeed = 15;
    [SerializeField]
    protected float yOffset = 1f;

    protected Vector3 offset;


    [SerializeField]
    private float tapThreshold = 0.1f;
    private float tapCounter = 0;

    public virtual void Start()
    {

    }
    public virtual void OnPlayerTouch()
    {
        isTapping = true;
        tapCounter = 0;
        isDragging = false;
    }
    public virtual void OnPlayerUnTouch()
    {
        if (isTapping)
        {
            OnTap();
        }
        else
        {
            OnEndDrag();
        }
    }
    public virtual void OnStartDrag()
    {       
        if (!isDragging)
        {
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPosition.z);
            offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
            offset.y += yOffset;
            isDragging = true;
        }       
    }
    public virtual void Update()
    {
        if (isTapping && !isDragging)
        {
            tapCounter += Time.deltaTime;
            if (tapCounter > tapThreshold)
            {
                isTapping = false;
                OnStartDrag();
            }
        }
        if (isDragging)
        {
            OnDrag();
        }
    }
    public virtual void OnDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
    public virtual void OnEndDrag()
    {
        isDragging = false;     
    }
    public virtual void OnTap()
    {
        //Debug.Log("Tap!!");
        isTapping = false;
    }
}

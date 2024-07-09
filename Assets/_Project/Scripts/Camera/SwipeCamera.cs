using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCamera : MonoBehaviour
{
    [Header("Setting")]
    public float panSpeed = 20f;
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    public float edgeThreshold = 50f;

    public LayerMask backgroundLayer;
    public LayerMask draggableLayer;
    public LayerMask collectObjLayer;

    private Vector3 touchStart;
    private Camera cam;
    private bool isSwipingBackground;
    private bool isDraggingObject;
    private DragObject currentDragObj;

    void Start()
    {
        cam = Camera.main;
        isSwipingBackground = false;
        isDraggingObject = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hitCollect = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, collectObjLayer);
            if (hitCollect.collider != null)
            {
                hitCollect.collider.GetComponent<CollectObject>()?.OnCollect();
                return;
            }

            RaycastHit2D hitDraggable = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, draggableLayer);

            if (hitDraggable.collider != null)
            {
                isDraggingObject = true;
                if (currentDragObj == null)
                {
                    currentDragObj = hitDraggable.collider.GetComponent<DragObject>();
                    currentDragObj.OnPlayerTouch();
                }

            }
            else
            {
                RaycastHit2D hitBackground = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, backgroundLayer);
                if (hitBackground.collider != null)
                {
                    isSwipingBackground = true;
                    touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
                    touchStart.z = 0;
                }
                
            }
        }

        if (Input.GetMouseButton(0) && GameController.Instance.isSwipeable)
        {
            if (isSwipingBackground)
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                direction.z = 0;
                transform.position += new Vector3(direction.x, 0, 0) * panSpeed * Time.deltaTime;
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), transform.position.y, transform.position.z);
            }
            else if (isDraggingObject)
            {
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x <= cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x + edgeThreshold * cam.aspect)
                {
                    transform.position += new Vector3(-panSpeed * Time.deltaTime, 0, 0);
                }
                else if (mousePos.x >= cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane)).x - edgeThreshold * cam.aspect)
                {
                    transform.position += new Vector3(panSpeed * Time.deltaTime, 0, 0);
                }
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), transform.position.y, transform.position.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwipingBackground = false;
            isDraggingObject = false;
            if (currentDragObj != null)
            {
                currentDragObj.OnPlayerUnTouch();
                currentDragObj = null;
            }
        }
    }
}

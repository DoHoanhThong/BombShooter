using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler, IEndDragHandler
{
    #region Private Members

    /// <summary>
    /// The ScrollContent component that belongs to the scroll content GameObject.
    /// </summary>
    [SerializeField]
    private ScrollContent scrollContent;

    /// <summary>
    /// How far the items will travel outside of the scroll view before being repositioned.
    /// </summary>
    [SerializeField]
    private float outOfBoundsThreshold;

    /// <summary>
    /// The ScrollRect component for this GameObject.
    /// </summary>
    private ScrollRect scrollRect;

    /// <summary>
    /// The last position where the user has dragged.
    /// </summary>
    private Vector2 lastDragPosition;

    /// <summary>
    /// Is the user dragging in the positive axis or the negative axis?
    /// </summary>
    private bool positiveDrag;

    #endregion

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.vertical = scrollContent.Vertical;
        scrollRect.horizontal = scrollContent.Horizontal;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        //Huy
        //middleItem = FindClosestToPoint(scrollContent.transform, scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center));
        //Huy
    }

    /// <summary>
    /// Called when the user starts to drag the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Called while the user is dragging the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>

    
    public void OnDrag(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.position.y > lastDragPosition.y;
        }
        else if (scrollContent.Horizontal)
        {
            positiveDrag = eventData.position.x > lastDragPosition.x;
        }
        isDragging = true;
        lastDragPosition = eventData.position;
    }
    

    /// <summary>
    /// Called when the user starts to scroll with their mouse wheel in the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the scroll event.</param>
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            // Scrolling up on the mouse wheel is considered a negative scroll, but I defined
            // scrolling downwards (scrolls right in a horizontal view) as the positive direciton,
            // so I check if the if scrollDelta.y is less than zero to check for a positive drag.
            positiveDrag = eventData.scrollDelta.y < 0;
        }
    }

    /// <summary>
    /// Called when the user is dragging/scrolling the scroll view.
    /// </summary>
    public void OnViewScroll()
    {
        if (scrollContent.Vertical)
        {
            HandleVerticalScroll();
        }
        else
        {
            HandleHorizontalScroll();
        }
    }

    /// <summary>
    /// Called if the scroll view is oriented vertically.
    /// </summary>
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight - scrollContent.ItemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight + scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }
   
    /// <summary>
    /// Called if the scroll view is oriented horizontally.
    /// </summary>
    private void HandleHorizontalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);
        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.x = endItem.position.x - scrollContent.ChildWidth - scrollContent.ItemSpacing;
        }
        else
        {
            newPos.x = endItem.position.x + scrollContent.ChildWidth  + scrollContent.ItemSpacing;            
        }
        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    /// <summary>
    /// Checks if an item has the reached the out of bounds threshold for the scroll view.
    /// </summary>
    /// <param name="item">The item to be checked.</param>
    /// <returns>True if the item has reached the threshold for either ends of the scroll view, false otherwise.</returns>
    private bool ReachedThreshold(Transform item)
    {
        if (scrollContent.Vertical)
        {
            float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        }
        else
        {
            float posXThreshold = transform.position.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
                item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
        }

        //if (scrollContent.Vertical)
        //{
        //    float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
        //    float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
        //    return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
        //        item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        //}
        //else
        //{
        //    float posXThreshold = scrollRect.transform.position.x + 3 * (scrollContent.ChildWidth + scrollContent.ItemSpacing);
        //    float negXThreshold = scrollRect.transform.position.x - 3 * (scrollContent.ChildWidth + scrollContent.ItemSpacing);
        //    return positiveDrag ? item.position.x  > posXThreshold : item.position.x < negXThreshold;
        //}
    }

    #region DevCustom
    [Space]
    [Header("Dev Custom")]
    public float scaleFactor = 0.75f;
    public float scaleSpeed = 50;
    public float lerpSpeed = 150;
    public float stopVelocity = 10;

    private bool isDragging = false;
    private Transform middleItem;
    private void Update()
    {
        if (scrollContent.Vertical)
        {
            HandleVerticalScale();
        }
        else
        {
            //HandleHorizontalScale();
        }
        
        if (isDragging == false && middleItem != null)
        {
            TowardsContentToCenter();
        }
    }
    private void HandleHorizontalScale()
    {
        //for (int i = 0; i < scrollRect.content.childCount; i++)
        //{
        //    RectTransform item = scrollRect.content.GetChild(i) as RectTransform;
        //    float distanceToCenter = Mathf.Abs(item.anchoredPosition.x + scrollRect.content.anchoredPosition.x);
        //    float normalizedDistance = Mathf.Clamp01(1 - distanceToCenter / (scrollRect.viewport.rect.width / 2));
        //    float targetScale = 1 + scaleFactor * normalizedDistance;
        //    item.localScale = Vector3.Lerp(item.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
        //}
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform item = scrollRect.content.GetChild(i) as RectTransform;
            float viewportCenterX = scrollRect.viewport.rect.width / 2;
            float distanceToCenter = Mathf.Abs(item.anchoredPosition.x + scrollRect.content.anchoredPosition.x - viewportCenterX);
            float normalizedDistance = Mathf.Clamp01(1 - distanceToCenter / viewportCenterX);
            float targetScale = 1 + scaleFactor * normalizedDistance;
            item.localScale = Vector3.Lerp(item.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
        }
    }
    private void HandleVerticalScale()
    {
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform item = scrollRect.content.GetChild(i) as RectTransform;
            float distanceToCenter = Mathf.Abs(item.anchoredPosition.y + scrollRect.content.anchoredPosition.y);
            float normalizedDistance = Mathf.Clamp01(1 - distanceToCenter / (scrollRect.viewport.rect.width / 2));
            float targetScale = 1 + scaleFactor * normalizedDistance;
            item.localScale = Vector3.Lerp(item.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
        }
    }
    private void TowardsContentToCenter()
    {
        Vector3 viewportCenter = scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center);
        float distanceToCenter = scrollContent.Vertical ? (viewportCenter.y - middleItem.position.y)
            : (viewportCenter.x - middleItem.position.x);

        if (Mathf.Abs(distanceToCenter) > 0.01f)
        {
            Vector2 targetPos = scrollContent.Vertical ? new Vector2(scrollContent.transform.position.x, scrollContent.transform.position.y + distanceToCenter)
                : new Vector2(scrollContent.transform.position.x + distanceToCenter, scrollContent.transform.position.y);
            scrollContent.transform.position = Vector2.MoveTowards(scrollContent.transform.position, targetPos, Time.deltaTime * lerpSpeed);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(WaitToStop());
        
    }
    IEnumerator WaitToStop()
    {
        yield return new WaitUntil(() => scrollRect.velocity.x < stopVelocity);
        if (isDragging)
        {
            isDragging = false;
            middleItem = FindClosestToPoint(scrollContent.transform, scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center));
        }
        
    }

    private Transform FindClosestToPoint(Transform parent, Vector3 point)
    {
        Transform closestTransform = null;
        float minDistance = float.MaxValue;

        foreach (Transform t in parent)
        {
            float distance = Vector3.Distance(t.position, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTransform = t;
            }
        }
        return closestTransform;
    }     
    private Transform GetNextSiblingTransform(Transform currentTransform)
    {
        int currentIndex = -1;

        for (int i = 0; i < currentTransform.parent.childCount; i++)
        {
            if (currentTransform.parent.GetChild(i) == currentTransform)
            {
                currentIndex = i;
                break;
            }
        }

        if (currentIndex != -1 && currentIndex + 1 < currentTransform.parent.childCount)
        {
            return currentTransform.parent.GetChild(currentIndex + 1);
        }
        else
        {
            return null;
        }
    }
    private Transform GetPreviousSiblingTransform(Transform currentTransform)
    {
        int currentIndex = -1;

        for (int i = 0; i < currentTransform.parent.childCount; i++)
        {
            if (currentTransform.parent.GetChild(i) == currentTransform)
            {
                currentIndex = i;
                break;
            }
        }

        if (currentIndex > 0)
        {
            return currentTransform.parent.GetChild(currentIndex - 1);
        }
        else
        {
            return null;
        }
    }

    public void NextItem()
    {
        if (middleItem == null)
        {
            middleItem = FindClosestToPoint(scrollContent.transform, scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center));
        }
        positiveDrag = false;
        middleItem = GetNextSiblingTransform(middleItem);
    }
    public void PreItem()
    {
        if (middleItem == null)
        {
            middleItem = FindClosestToPoint(scrollContent.transform, scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center));
        }
        positiveDrag = true;
        middleItem = GetPreviousSiblingTransform(middleItem);
    }
    #endregion
}

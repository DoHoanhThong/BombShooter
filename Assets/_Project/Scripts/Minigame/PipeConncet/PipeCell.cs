using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class PipeCell : MonoBehaviour, IPointerClickHandler
{
    public bool isCollect = false;

    private PipeData pipeData;

    private GameObject item;

    private bool canRotate = true;

    private float duration = 0.25f;

    private float correctRotation;

    private float zRotate = 0;
    public void Initialize(PipeData pipeData, float correctRotation)
    {
        if (pipeData != null)
        {
            this.pipeData = pipeData;
            item = Instantiate(pipeData.pipePrefab, transform);
            RandomRotation();
            this.correctRotation = correctRotation;
        }
        
    }
    public void RandomRotation()
    {
        var random = (int) Random.Range(1, 4);
        zRotate = correctRotation + random * 90;
        item.transform.localRotation = Quaternion.Euler(0, 0, zRotate);
        canRotate = true;

        if (CheckCorrect())
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
    public void NextState()
    {
        if (!canRotate || pipeData == null)
            return;
        canRotate = false;
        zRotate += 90;
        item.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, zRotate), duration).OnComplete(() =>
        {
            canRotate = true;
            if (CheckCorrect())
            {
                GetComponent<Image>()?.DOColor(Color.green, duration);
            }
            else
            {
                GetComponent<Image>()?.DOColor(Color.white, duration);
            }
            PipeBoard.OnPipeClicked?.Invoke();
        });
    }
    public bool CheckCorrect()
    {
        if (pipeData == null)
            return true;
        isCollect = pipeData.hasTwoDirection ? zRotate % 180 == correctRotation % 180 : correctRotation % 360 == zRotate % 360;
        return isCollect;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PipeConnectController.Instance.isPaused)
            NextState();
    }
    public void DestroyObj()
    {
        transform.DOScale(0, duration).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }
}

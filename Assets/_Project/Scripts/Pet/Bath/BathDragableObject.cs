using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathDragableObject : UIDragableObject
{
    public string triggerName;
    public void OnTaskDone()
    {
        image.DOFade(0, returnDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

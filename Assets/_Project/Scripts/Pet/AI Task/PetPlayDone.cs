using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
public class PetPlayDone : Action
{
    public float jumpHeight = 5f;
    public float duration = 1f;

    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 midPoint;

    private PetDrag dragObject;
    private CatAnim anim;

    public override void OnAwake()
    {
        dragObject = GetComponent<PetDrag>();
        anim = GetComponent<CatAnim>();
    }
    private bool isJumping = false;
    public override void OnStart()
    {
        startPos = transform.position;
        endPos = dragObject.currentToy.jumpPos.position;
        midPoint = (startPos + endPos) / 2;
        midPoint.y += jumpHeight;
        isJumping = true;

        transform.DOPath(new[] { startPos, midPoint, endPos }, duration, PathType.CatmullRom)
           .SetEase(Ease.Linear)
           .OnWaypointChange(waypointIndex =>
           {
               if (waypointIndex == 0)
               {
                   anim.PlayJumpUp();
               }
               else if (waypointIndex == 1)
               {
                   // Action at mid point
                   anim.PlayJumpDown();
               }
           })
           .OnComplete(() => { isJumping = false; });
    }

    public override TaskStatus OnUpdate()
    {
        if (isJumping)
        {
            return TaskStatus.Running;
        }
        transform.position = endPos;
        ItemSpawner.Instance?.SpawnHeart(transform.position);
        GetComponent<CatMovement>().OnEndDrag();
        dragObject.currentToy = null;
        return TaskStatus.Success;
    }
}

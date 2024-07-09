using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
public class PetPlay : Action
{
    PetDrag dragObject;
    public override void OnAwake()
    {
        dragObject = GetComponent<PetDrag>();
    }
    public override void OnStart()
    {
        dragObject.currentToy.OnPlay(dragObject);
        dragObject.isPlaying = true;
    }

    public override TaskStatus OnUpdate()
    {
        if (dragObject.isPlaying)
        {
            return TaskStatus.Running;
        }
        dragObject.isPlaying = false;    
        return TaskStatus.Success;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IsPetPlaying : Conditional
{
    private PetDrag dragObject;
    public override void OnAwake()
    {
        dragObject = GetComponent<PetDrag>();
    }
    public override TaskStatus OnUpdate()
    {
        if (dragObject.currentToy != null && dragObject.isDragging == false)
        {
            dragObject.currentToy.OnPointerExit();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

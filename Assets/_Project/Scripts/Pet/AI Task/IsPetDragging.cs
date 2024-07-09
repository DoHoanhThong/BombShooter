using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class IsPetDragging : Conditional
{
    private DragObject dragObject;
    public override void OnAwake()
    {
        dragObject = GetComponent<DragObject>();
    }
    public override TaskStatus OnUpdate()
    {
        if (dragObject.isDragging)
        {          
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

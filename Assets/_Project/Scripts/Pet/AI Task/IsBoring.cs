using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class IsBoring : Conditional
{
    private PetController controller;
    public override void OnAwake()
    {
        controller = GetComponent<PetController>();
    }
    public override TaskStatus OnUpdate()
    {
        if (controller.runtimeData.currentState == CharacterState.Boring)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
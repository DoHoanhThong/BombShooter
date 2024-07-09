using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class PlayPickup : Action
{
    private CatAnim anim;
    public override void OnAwake()
    {
        anim = GetComponent<CatAnim>();
    }
    public override TaskStatus OnUpdate()
    {
        anim.PlayPickup();
        return TaskStatus.Success;
    }
}

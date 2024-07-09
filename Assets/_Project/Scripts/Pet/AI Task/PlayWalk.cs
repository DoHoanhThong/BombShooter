using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class PlayWalk: Action
{
    private CatAnim anim;
    public override void OnAwake()
    {
        anim = GetComponent<CatAnim>();
    }
    public override TaskStatus OnUpdate()
    {
        anim.PlayWalk();
        return TaskStatus.Success;
    }
}
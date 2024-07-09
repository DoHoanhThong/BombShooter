using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IsPetMove : Conditional
{
    [SerializeField]
    private float minMoveTime = 4;
    [SerializeField]
    private float maxMoveTime = 6;

    private CatMovement movement;
    private float moveTime;
    private float counter = 0;
    public override void OnAwake()
    {
        movement = GetComponent<CatMovement>();
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        counter = 0;
    }
    public override TaskStatus OnUpdate()
    {
        if (movement.canMove)
        {
            counter += Time.deltaTime;
        }
        
        if (counter < moveTime && movement.IsMoveDone())
        {
            movement.RandomX();
            return TaskStatus.Success;
        }
        if (counter > moveTime)
        {
            counter = 0;
            moveTime = Random.Range(minMoveTime, maxMoveTime);
            movement.canMove = false;
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}

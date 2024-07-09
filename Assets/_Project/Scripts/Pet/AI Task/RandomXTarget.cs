using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class RandomXTarget : Action
{
    //public float minCooldownTime = 4f;
    //public float maxCooldownTime = 5f;

    //private float cooldownTime;
    //private float counter;

    private CatMovement movement;

    public override void OnAwake()
    {
        //cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        //counter = 0;
        movement = GetComponent<CatMovement>();
    }

    public override TaskStatus OnUpdate()
    {
        if (!movement.IsMoveDone())
        {
            return TaskStatus.Success;
        }
        //movement.RandomX();
        return TaskStatus.Failure;
        //counter += Time.deltaTime;

        //if (counter >= cooldownTime)
        //{
            
        //    counter = 0;
        //    cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        //}

        //return TaskStatus.Success;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class RandomYTarget : Action
{
    public float minCooldownTime = 1f;
    public float maxCooldownTime = 2f;

    private float cooldownTime;
    private float counter;

    private CatMovement movement;

    public override void OnAwake()
    {
        cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        counter = 0;
        movement = GetComponent<CatMovement>();
    }

    public override TaskStatus OnUpdate()
    {
        counter += Time.deltaTime;

        if (counter >= cooldownTime)
        {
            movement.RandomY();
            counter = 0;
            cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        }

        return TaskStatus.Success;
    }
}

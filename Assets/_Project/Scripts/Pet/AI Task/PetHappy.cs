using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class PetHappy : Action
{
    [SerializeField]
    private float happyTime = 2;
    private CatMovement movement;
    private CatAnim anim;

    private float counter = 0;

    private int randomIndex = 0;
    public override void OnAwake()
    {
        movement = GetComponent<CatMovement>();
        anim = GetComponent<CatAnim>();
        randomIndex = Random.Range(0, 4);
    }
    public override void OnStart()
    {
        base.OnStart();
        SoundFXManager.Instance?.PlayCatHappy();

    }
    public override TaskStatus OnUpdate()
    {
        counter += Time.deltaTime;
        if (counter > happyTime)
        {
            movement.RandomX();
            movement.canMove = true;
            counter = 0;
            randomIndex = Random.Range(0, 4);
            anim.PlayWalk();
            return TaskStatus.Success;
        }
        RandomAnim();
        return TaskStatus.Running;
    }
    private void RandomAnim()
    {
        switch (randomIndex)
        {
            case 0:
                anim.PlayIdle();
                break;
            case 1:
                anim.PlayIdle_1();
                break;
            case 2:
                anim.PlayHappy();
                break;
            case 3:
                anim.PlayHappy_1();
                break;
        }
    }
}

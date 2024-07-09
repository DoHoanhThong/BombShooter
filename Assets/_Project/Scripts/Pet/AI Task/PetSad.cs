using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class PetSad : Action
{
    [SerializeField]
    private float sadTime = 4;
    private CatMovement movement;
    private CatAnim anim;

    private float counter = 0;
    //private int randomIndex = 0;
    public override void OnAwake()
    {
        movement = GetComponent<CatMovement>();
        anim = GetComponent<CatAnim>();
        //randomIndex = Random.Range(0, 4);
    }
    public override void OnStart()
    {
        base.OnStart();
        SoundFXManager.Instance?.PlayCatSad();
    }
    public override TaskStatus OnUpdate()
    {
        counter += Time.deltaTime;
        if (counter > sadTime)
        {
            movement.RandomX();
            movement.canMove = true;
            counter = 0;
            //randomIndex = Random.Range(0, 4);
            anim.PlayWalk();
            return TaskStatus.Success;
        }
        anim.PlaySad();
        return TaskStatus.Running;
    }
   
}

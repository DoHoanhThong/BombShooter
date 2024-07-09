using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PetController : MonoBehaviour
{
    public RuntimeData runtimeData;
    [SerializeField]
    private CatAnim anim;

    [SerializeField]
    private RequestBubble requestBubble;
    [SerializeField]
    private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;

    private CatMovement movement;

    public bool isRequesting = false;

    private void Awake()
    {
        isRequesting = false;
        movement = GetComponent<CatMovement>();

    }
    public void Initialize(RuntimeData runtimeData)
    {
        anim.SetSkin(runtimeData.petData.skinID);
        this.runtimeData = runtimeData;
        switch (runtimeData.currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Requesting:
                ShowRequest(runtimeData.lastRequest);
                break;
            case CharacterState.Boring:
                
                break;
        }
    }


    public void SetPlayState(string animName, float duration)
    {
        anim.PlayAnimation(animName, true, 1);        
        movement.canMove = false;
    }
    public void ShowRandomRequest()
    {
        ShowRequest(runtimeData.GetRandomRequest());
    }
    public void ShowRequest(RequestType request)
    {
        switch (request)
        {
            case RequestType.Feed:
                RequestFeed();
                break;
            case RequestType.Bath:
                RequestBath();
                break;
        }
        runtimeData.currentState = CharacterState.Requesting;
        isRequesting = true;
    }
    public void SetCurrentRequestPet()
    {
        DataContainer.Instance.currentCatData = runtimeData;
    }
    public void RequestFeed()
    {
        //RequestAction(Action.FEED, data.id);
        requestBubble.RequestFeed();
    }
    public void RequestBath()
    {
        requestBubble.RequestBath();
    }
    public void OnUnlocked()
    {
        StartCoroutine(DelayBehavior());
    }
    IEnumerator DelayBehavior()
    {
        behaviorTree.enabled = false;
        anim.PlayIdle();
        yield return new WaitForSeconds(1.5f);
        behaviorTree.enabled = true;
    }
}

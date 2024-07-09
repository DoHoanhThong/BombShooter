using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayToy : MonoBehaviour
{
    public PetDrag currentPet;
    public bool isPlaying;
    public Transform animHolder;

    public Transform playPos;
    public Transform jumpPos;
    [SerializeField]
    private GameObject outline;
    [SerializeField]
    protected string animName = "idle";
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    private float duration = 3;
    [SerializeField]
    private float scale = 1.2f;
    [SerializeField]
    private float scaleDuration = .25f;
    private float originScale;
    
    public virtual void Start()
    {
        originScale = transform.localScale.x;
    }
    public virtual void OnPlay(PetDrag dragObject)
    {
        if (currentPet == null && !isPlaying)
        {
            currentPet = dragObject;
            dragObject.transform.position = playPos.position;
            dragObject.transform.SetParent(animHolder);
            animator.SetTrigger("play");
            isPlaying = true;
        }
            
    }
    
    public virtual void OnPlayDone()
    {
        currentPet.isPlaying = false;
        currentPet.transform.SetParent(transform.root);
        currentPet = null;
        isPlaying = false;
    }
    public virtual void OnPointerEnter()
    {
        if (currentPet == null)
        {
            outline.SetActive(true);
            transform.DOScale(originScale * scale, scaleDuration);
        }
        
    }
    public virtual void OnPointerExit()
    {
        if (currentPet == null)
        {
            outline.SetActive(false);
            transform.DOScale(originScale, scaleDuration);
        }
        
    }
   
}

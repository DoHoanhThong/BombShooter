using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounterHeart : MonoBehaviour
{
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite deactiveSprite;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController idleNormal;
    [SerializeField]
    private RuntimeAnimatorController idlePluse;


    public void SetCurrent()
    {
        animator.runtimeAnimatorController = idlePluse;       
    }
    public void SetActive()
    {
        animator.runtimeAnimatorController = idleNormal;
        lifeImage.sprite = activeSprite;
    }
    public void SetDeactive()
    {
        animator.runtimeAnimatorController = idleNormal;
        lifeImage.sprite = deactiveSprite;
    }
}

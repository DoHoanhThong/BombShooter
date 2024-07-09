using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Sirenix.OdinInspector;

public class AnimationControllerBase : MonoBehaviour
{
    public enum Type
    {
        Animation = 0,
        Graphic = 1,
    }
    [SerializeField]
    private Type _type;
    [SerializeField, ShowIf("_type", Type.Animation)] protected SkeletonAnimation anim;
    [SerializeField, ShowIf("_type", Type.Graphic)] protected SkeletonGraphic animUI;

    //[SerializeField] protected SkeletonAnimation anim;

    protected string currentAnim = "";

    public void PlayAnimation(string animName, bool isLoop = true, float timeScale = 1)
    {
        if (currentAnim != animName)
        {
            currentAnim = animName;    
            switch (_type)
            {
                case Type.Animation:
                    anim.AnimationState.SetAnimation(0, animName, isLoop).TimeScale = timeScale;                   
                    break;
                case Type.Graphic:
                    animUI.AnimationState.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
                    break;
            }
                     
        }
    }
    public void PlayHasReset(string animName, bool isLoop = true, float timeScale = 1)
    {
        switch (_type)
        {
            case Type.Animation:
                anim.AnimationState.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
                break;
            case Type.Graphic:
                animUI.AnimationState.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
                break;
        }
    }
    public void Reset()
    {
        currentAnim = null;
    }
    public void SetTimeScale(float timeScale)
    {
        anim.AnimationState.TimeScale = timeScale;
    }
    protected void PlayTwoAnim(string animName_1, string animName_2, float delay, bool isLoop_1 = true, bool isLoop_2 = true, float timeScale_1 = 1, float timeScale_2 = 1)
    {
        //if (anim.gameObject.activeInHierarchy)
            //StartCoroutine(PlayAnimUI_1To_2(animName_1, animName_2, isLoop_1, isLoop_2));
            StartCoroutine(PlayTwoAnimCoro(animName_1, animName_2, delay));
    }
    public IEnumerator PlayAnimUI_1To_2(string animName_1, string animName_2, bool isLoop_1 = true,  bool isLoop_2 = true, float timeScale_1 = 1,float timeScale_2 = 1)
    {        
        var track = animUI.AnimationState.SetAnimation(0, animName_1, isLoop_1);
        SetTimeScale(timeScale_1);       
        if (currentAnim != animName_1)
        {
            currentAnim = animName_1;
        }
        
        WaitForSpineAnimationComplete wait = new WaitForSpineAnimationComplete(track);
        yield return wait;

        animUI.AnimationState.SetAnimation(0, animName_2, isLoop_2);
        SetTimeScale(timeScale_2);
        if (currentAnim != animName_2)
        {
            currentAnim = animName_2;
        }
    }
    public IEnumerator PlayStillEnd(string animName, bool isLoop = true)
    {
        var track = anim.state.SetAnimation(0, animName, isLoop);
        WaitForSpineAnimationComplete wait = new WaitForSpineAnimationComplete(track);
        yield return wait;
        gameObject.SetActive(false);
    }
    public void FlipX(bool isEnable)
    {
        //Away set this before set anim
        if (animUI != null)
        {
            animUI.initialFlipX = isEnable;
        }        
    }
    IEnumerator PlayTwoAnimCoro(string animName_1, string animName_2, float delay)
    {
        PlayAnimation(animName_1);
        yield return new WaitForSeconds(delay);
        PlayAnimation(animName_2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class FishAnim : AnimationControllerBase
{
    [SerializeField, SpineAnimation] protected string strIdle;
    [SerializeField, SpineAnimation] protected string strAttack;

    private float currentIdleTimeScale;
    private Coroutine attackCoro;
    private float takeDameDuration = 0.15f;

    public void SetSkin(string skinID)
    {
        anim.Skeleton.SetSkin(skinID);
    }
    public void PlayIdle()
    {        
        PlayAnimation(strIdle,true,1);       
        currentIdleTimeScale = 1;
    }
    //public void PlayAttack()
    //{
    //    //PlayAnimation(strAttack, false);             
    //    PlayTwoAnim(strAttack, strIdle, false, true, 1, currentIdleTimeScale);
    //}
    public void PlayLoopAttack()
    {
        PlayAnimation(strAttack, true, 1);
        currentIdleTimeScale = 1;
    }
    public void PlayBoost()
    {
        //PlayAnimation(strIdle, true, 4);
        SetTimeScale(4f);
        currentIdleTimeScale = 4;
    }
    public void PlayStopBoost()
    {
        SetTimeScale(1f);
        currentIdleTimeScale = 1;
    }
    public void PlayTakeDame()
    {
        StartCoroutine(ChangeColor());
    }
    public void ResetColor()
    {
        anim.Skeleton.SetColor(Color.white);
    }
    IEnumerator ChangeColor()
    {
        anim.Skeleton.SetColor(Color.red);
        yield return new WaitForSeconds(takeDameDuration);
        anim.Skeleton.SetColor(Color.white);
    }
    public bool IsInCameraPos()
    {
        return GetComponent<Renderer>().isVisible;
    }
    
}

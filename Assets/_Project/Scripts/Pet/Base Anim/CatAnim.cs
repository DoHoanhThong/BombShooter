using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CatAnim : AnimationControllerBase
{
    [SerializeField, SpineAnimation] protected string strBath;
    [SerializeField, SpineAnimation] protected string strIdle;
    [SerializeField, SpineAnimation] protected string strIdle_1;
    [SerializeField, SpineAnimation] protected string strIdle_2;
    [SerializeField, SpineAnimation] protected string strIdle_3;
    [SerializeField, SpineAnimation] protected string strWalk;
    [SerializeField, SpineAnimation] protected string strHappy;
    [SerializeField, SpineAnimation] protected string strHappy_1;
    [SerializeField, SpineAnimation] protected string strHappy_2;
    [SerializeField, SpineAnimation] protected string strEat_1;
    [SerializeField, SpineAnimation] protected string strEat_2;
    [SerializeField, SpineAnimation] protected string strEat_3;
    [SerializeField, SpineAnimation] protected string strPickup;
    [SerializeField, SpineAnimation] protected string strTrampoline;
    [SerializeField, SpineAnimation] protected string strTest;
    [SerializeField, SpineAnimation] protected string strJumpUp;
    [SerializeField, SpineAnimation] protected string strJumpDown;
    [SerializeField, SpineAnimation] protected string strSad;

    private float currentIdleTimeScale;
    private Coroutine attackCoro;
    private float takeDameDuration = 0.15f;

    public void SetSkin(string skinID)
    {
        if (anim != null)
        {
            anim.Skeleton.SetSkin(skinID);
        }
        else
        {
            animUI.Skeleton.SetSkin(skinID);
        }
        
    }
    public void PlayBath()
    {
        PlayAnimation(strBath, true, 1);
    }
    public void PlayIdle()
    {
        PlayAnimation(strIdle, true, 1);
    }
    public void PlayIdle_1()
    {
        PlayAnimation(strIdle_1, true, 1);
    }
    public void PlayIdle_2()
    {
        PlayAnimation(strIdle_2, true, 1);
    }
    public void PlayIdle_3()
    {
        PlayAnimation(strIdle_3, true, 1);
    }
    public void PlayWalk()
    {
        PlayAnimation(strWalk, true, 1);
    }
    public void PlayHappy()
    {
        PlayAnimation(strHappy, true, 1);
    }
    public void PlayHappy_1()
    {
        PlayAnimation(strHappy_1, true, 1);
    }
    public void PlayHappy_2()
    {
        PlayAnimation(strHappy_2, true, 1);
    }
    public void PlayEat_1()
    {
        PlayAnimation(strEat_1, true, 1);
    }
    public void PlayEat_2()
    {
        PlayAnimation(strEat_2, true, 1);
    }
    public void PlayEat_3()
    {
        //PlayAnimation(strEat_3, false, 1);
        //PlayTwoAnim
        PlayTwoAnim(strEat_3, strEat_1, 2);
    }
    public void PlayJumpUp()
    {
        PlayAnimation(strJumpUp, false, 2);
    }
    public void PlayJumpDown()
    {
        PlayAnimation(strJumpDown, false, 2);
    }
    public void PlayPickup()
    {
        PlayAnimation(strPickup, true, 1);
    }
    public void PlaySad()
    {
        PlayAnimation(strSad, true, 1);
    }
    public void PlayTrampoline()
    {
        PlayAnimation(strTrampoline, true, 1);
    }
    //public void OnGetPoint()
    //{
    //    PlayTwoAnim(strHappy, strIdle);
    //}
    public void PlayTakeDame()
    {
        StartCoroutine(ChangeColor());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TrampolineAnim : AnimationControllerBase
{
    [SerializeField, SpineAnimation] protected string strBounce;
    [SerializeField, SpineAnimation] protected string strIdle;

    public void PlayBounce()
    {
        PlayHasReset(strBounce, false, 2);
    }
    public void PlayIdle()
    {
        PlayAnimation(strIdle, true, 1);
    }
}

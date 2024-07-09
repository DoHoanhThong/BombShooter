using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : PlayToy
{
    [SerializeField]
    private TrampolineAnim toyAnim;
    public void OnPetBounce()
    {
        var anim = currentPet?.GetComponent<CatAnim>();
        anim.PlayAnimation(animName, false, 1);
        anim.Reset();
        toyAnim.PlayBounce();
    }
}

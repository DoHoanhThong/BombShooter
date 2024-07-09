using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumblerDoll : PlayToy
{
    public override void OnPlay(PetDrag dragObject)
    {
        base.OnPlay(dragObject);
        if (currentPet != null)
        {
            var anim = currentPet?.GetComponent<CatAnim>();
            anim.PlayAnimation(animName, true, 1);
        }
    }
}

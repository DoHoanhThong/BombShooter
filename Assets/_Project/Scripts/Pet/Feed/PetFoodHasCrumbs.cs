using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFoodHasCrumbs : PetFood
{
    [SerializeField]
    private FoodCrumbs crumbsPrefab;

    public override void OnUsed(RectTransform mouthPos)
    {
        base.OnUsed(mouthPos);
        var crumbs = Instantiate(crumbsPrefab, mouthPos.transform);
        crumbs.OnSpawn();
        crumbs.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
       
    }
}

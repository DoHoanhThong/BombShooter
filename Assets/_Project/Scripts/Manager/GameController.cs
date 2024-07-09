using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
public class GameController : Singleton<GameController>
{
    public bool isSwipeable = true;
    [SerializeField]
    private PetManager petManager;
    [SerializeField]
    private PropManager propManager;
    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        isSwipeable = true;
        Screen.orientation = ScreenOrientation.LandscapeRight;
        HouseItemHolder.Instance?.ActiveItems();
    }
    private void OnDestroy()
    {
        HouseItemHolder.Instance?.DeactiveItem();
    }

    public void BuyNewCat(string petID)
    {
        petManager.UnlockNewPet(petID);        
    }
    public void BuyNewProp(string propID)
    {
        propManager.UnlockNewProp(propID);
    }
}

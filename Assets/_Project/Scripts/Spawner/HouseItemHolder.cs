using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
public class HouseItemHolder : Singleton<HouseItemHolder>
{
    public Transform holder;
    public Transform canvasHolder;
    public void ActiveItems()
    {
        holder.gameObject.SetActive(true);
    }
    public void DeactiveItem()
    {
        holder.gameObject.SetActive(false);
    }
}

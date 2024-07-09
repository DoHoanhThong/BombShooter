using System.Collections.Generic;
using UnityEngine;

public class KnifePooler : MonoBehaviour
{
    public KnifeScript knifePrefab;
    public int poolSize = 10;
    private List<KnifeScript> knifePool;
    public void Init()
    {
        knifePool = new List<KnifeScript>();
        for (int i = 0; i < poolSize; i++)
        {
            KnifeScript knife = Instantiate(knifePrefab);
            knife.gameObject.SetActive(false);
            knifePool.Add(knife);
            knife.transform.SetParent(transform);
        }
    }

    public KnifeScript GetKnife()
    {
        foreach (KnifeScript knife in knifePool)
        {
            if (!knife.gameObject.activeInHierarchy)
            {
                knife.transform.SetParent(transform);
                knife.gameObject.SetActive(true);
                return knife;
            }
        }

        KnifeScript newKnife = Instantiate(knifePrefab);
        newKnife.transform.SetParent(transform);
        knifePool.Add(newKnife);
        return newKnife;
    }
}

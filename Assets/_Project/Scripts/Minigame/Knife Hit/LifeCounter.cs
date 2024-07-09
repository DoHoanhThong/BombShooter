using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeCounter : MonoBehaviour
{
    private int lifeCount = 3;
    [SerializeField]
    private LifeCounterHeart heartPrefab;
    [SerializeField]
    private RectTransform content;
    
    private List<LifeCounterHeart> lifeImages = new List<LifeCounterHeart>();

    private int counter;
    public void Init(int lifeCount)
    {
        this.lifeCount = lifeCount;
        for (int i = 0; i < lifeCount; i++)
        {
            var lifeImage = Instantiate(heartPrefab, content);
            lifeImages.Add(lifeImage);
            lifeImage.SetActive();
            lifeImage.gameObject.SetActive(true);
        }
        counter = lifeCount;
        lifeImages[counter - 1].SetCurrent();
    }
    public void LoseLife()
    {
        if (counter > 0)
            lifeImages[counter - 1].SetDeactive();
        counter--;
        if (counter > 0)
            lifeImages[counter - 1].SetCurrent();
    }
    public void Reset()
    {
        foreach (var lifeImage in lifeImages)
        {
            lifeImage.SetActive();
        }
        counter = lifeCount;
        lifeImages[counter - 1].SetCurrent();
    }
}

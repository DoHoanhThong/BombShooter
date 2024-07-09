using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class KnifeCount : MonoBehaviour
{
    [SerializeField]
    private Transform _countHolder;
    [SerializeField]
    private Image _lightPrefab;
    [SerializeField]
    private Sprite _startSprite;
    [SerializeField]
    private Material grayScale;

    [SerializeField]
    private float duration = 0.25f;

    private int _lightCount = 7;

    private List<Image> listCount = new List<Image>();
    public void Initalize(int count)
    {       
        foreach (var item in listCount)
        {
            Destroy(item.gameObject);
        }
        listCount.Clear();

        _lightCount = count;
        for (int i = 0; i < _lightCount; i++)
        {
            var item = Instantiate(_lightPrefab, _countHolder);
            item.sprite = _startSprite;
            listCount.Add(item);
            item.color = Color.white;
            item.material = null;
            item.transform.localScale = Vector3.zero;
            item.gameObject.SetActive(true);
            item.transform.DOScale(1, duration).SetDelay(i * (duration / 2f)).SetEase(Ease.OutBack);
        }
        gameObject.SetActive(true);
    }
    public void OnThrowKnife(int index, bool isHit = true)
    {        
        if (isHit)
        {
            //listCount[index].color = Color.white;
            listCount[listCount.Count - 1 - index].material = grayScale;
        }
        else
        {
            listCount[listCount.Count - 1 - index].color = new Color (1, 1, 1, 100f / 255f);           
        }
    }
    public void Reset()
    {
        //foreach (Image item in listCount)
        //{
        //    item.sprite = _startSprite;
        //    item.color = Color.white;
        //    item.material = null;
        //}
        for (int i = listCount.Count - 1; i >= 0; i--)
        {
            listCount[i].transform.DOScale(0, duration).SetDelay(i * (duration / 2f)).SetEase(Ease.InBack);
        }
    }
    //public void OnChangeCountAmount(int value)
    //{
    //    _lightCount = value;
    //    Initalize();
    //}
}

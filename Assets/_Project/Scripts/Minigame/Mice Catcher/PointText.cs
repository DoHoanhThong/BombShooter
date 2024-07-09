using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PointText : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;
    [SerializeField]
    private Color _minusColor;
    [SerializeField]
    private Color _redColor;
    [SerializeField]
    private Color _blueColor;
    [SerializeField]
    private float _duration = 0.25f;
    private void Awake()
    {
        _text.rectTransform.localScale = Vector3.zero;
    }
    public void Plus(int point, bool isRed)
    {
        _text.text = "+" + point.ToString();
        _text.color = isRed ? _redColor : _blueColor;
        TextAnim();
    }
    public void Minus(int point)
    {
        _text.text = "-" + point.ToString();
        _text.color = _minusColor;
        TextAnim();
    }
    private void TextAnim()
    {
        _text.rectTransform.localScale = Vector3.zero;
        //_text.rectTransform.pivot = new Vector2(0.5f, 0);
        _text.rectTransform.DOScale(1, _duration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            //_text.rectTransform.pivot = new Vector2(0.5f, 1f);
            _text.rectTransform.DOScale(0, _duration).SetDelay(_duration).OnComplete(() => Destroy(gameObject));
        });
    }
}

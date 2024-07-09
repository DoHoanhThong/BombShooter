using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MemoryCard : MonoBehaviour, IPointerClickHandler
{
    public MemoryCardData data;

    private bool isFaceDown;
    private MemoryController _controller;
    public GameObject _corner;
    [SerializeField]
    private Image _card;
    //[SerializeField]
    //private Sprite _faceupSprite;
    //[SerializeField]
    //private Sprite _facedownSprite;

    [SerializeField]
    private float _rotateSpeed = 2;   
    

    [SerializeField]
    private float _collectScale = 0.5f;

    [SerializeField]
    private float _flipDuration = 0.75f;
    [SerializeField]
    private float _scaleMultiplier = 0.5f;
    [SerializeField]
    private float _startScale = 0.23f;

    [Header("Shadow")]
    [SerializeField]
    private RectTransform _shadow;

    [SerializeField]
    private float _shadowMaxScale = 0.75f;
    [SerializeField]
    private float _shadowStartScale = 0.28f;

    private bool _isFliping = false;

    private float _duration = 0.5f;

    private Vector2 _shadowOffset;

    public void Initialize(MemoryController controller, MemoryCardData data)
    {
        this.data = data;

        _controller = controller;

        _card.sprite = data.backSprite;

        isFaceDown = true;

        _isFliping = false;

        _shadowOffset = _card.rectTransform.anchoredPosition - _shadow.anchoredPosition;
        //_shadowOffset = new Vector2(transform.position.x - _shadow.transform.position.x, transform.position.y - _shadow.transform.position.y);

        //_shadow.transform.SetParent(transform.parent);
        _shadow.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }
    float ratio = 1;
    private void Update()
    {
        //ratio = 1 + (transform.localScale.x - _startScale) / (_scaleMultiplier - _startScale);
        _shadow.transform.rotation = _card.transform.rotation;
        _shadow.anchoredPosition = new Vector2(_card.rectTransform.anchoredPosition.x - _shadowOffset.x * ratio, _card.rectTransform.anchoredPosition.y - _shadowOffset.y * ratio);
        _shadow.transform.localScale = Vector3.one * (ratio * _shadowStartScale);
    }


    public void FlipDown(float delay)
    {
        if (isFaceDown)
            return;
        isFaceDown = true;   
        StartCoroutine(FlipCard(data.frontSprite, data.backSprite, false, delay));        
    }
    public void FlipUp()
    {
        if (_isFliping)
            return;
        MemoryController.OnFlipCard?.Invoke(this);
        isFaceDown = false;
        _isFliping = true;
        StartCoroutine(FlipCard(data.backSprite, data.frontSprite, true));
    }
     
    private void OnDestroy()
    {
        Destroy(_shadow.gameObject);
    }


    private IEnumerator FlipCard(Sprite firstSprite, Sprite secondSprite, bool isUpFlip = true, float delay = 0f)
    {       
        yield return new WaitForSeconds(delay);
        _card.sprite = firstSprite;
        
        SoundFXManager.Instance?.PlayCardFlip();
        float zRotate = transform.rotation.eulerAngles.z;
        for (float t = 0f; t <= _flipDuration / 2f; t += Time.deltaTime)
        {
            
            float normalizedTime = Mathf.Clamp01(t / (_flipDuration / 2f));
            float rotationAngle = Mathf.Lerp(0f, 90f, normalizedTime);
            float scale = Mathf.Lerp(_startScale, _scaleMultiplier, normalizedTime);

            _card.transform.rotation = Quaternion.Euler(0f, rotationAngle, zRotate);
            _card.transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }
        
        _card.sprite = secondSprite;
        if (!isUpFlip)
        {

            _controller.EnaOrDisableImageItem(_card.transform.parent.GetComponent<MemoryCard>(), isUpFlip);
        }
        else
        {
            _controller.EnaOrDisableImageItem(_card.transform.parent.GetComponent<MemoryCard>(), isUpFlip);
        }
        
        for (float t = 0f; t <= _flipDuration / 2f; t += Time.deltaTime)
        {
            
            float normalizedTime = Mathf.Clamp01(t / (_flipDuration / 2f));
            float rotationAngle = Mathf.Lerp(90f, 0, normalizedTime);
            float scale = Mathf.Lerp(_scaleMultiplier, _startScale, normalizedTime);

            _card.transform.rotation = Quaternion.Euler(0f, rotationAngle, zRotate);
            _card.transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }
        _card.transform.localScale = Vector3.one * _startScale;
    
        if (!isUpFlip)
        {
            _isFliping = false;
        }
    }
    public void OnCollect(Vector3 centerPoint, Vector3 collectPoint)
    {
        Debug.LogError("2hheh");
        _shadow.transform.SetAsFirstSibling();
        transform.SetAsLastSibling();

        transform.DOMove(centerPoint, _duration).OnComplete(() =>
        {
            _controller.CheckItem(_card.transform.parent.GetComponent<MemoryCard>());
        });
        transform.DORotate(Vector3.zero, _duration, RotateMode.FastBeyond360);
        transform.DOScale(_collectScale, _duration );
        transform.DOMove(collectPoint, _duration).SetDelay(_duration * 2).OnComplete(() => 
        {
            transform.DOScale(0, _duration / 2f).OnComplete(() => Destroy(gameObject));
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_controller.isTapable)
        {
            return;
        }

        FlipUp();
    }
    public void ActiveShadow()
    {

    }
}

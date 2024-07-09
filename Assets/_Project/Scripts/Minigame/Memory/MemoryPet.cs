using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MemoryPet : MonoBehaviour
{
    public static System.Action OnGetPoint;
    [SerializeField]
    private CatAnim catAnim;
    [SerializeField]
    private float walkDuration = 0.5f;
    [SerializeField]
    private float distance = 600;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(startPos.x - distance, startPos.y);
        WalkToCenter();

        OnGetPoint += PlayHappy;
    }
    private void OnDestroy()
    {
        OnGetPoint -= PlayHappy;
    }
    private void WalkToCenter()
    {
        catAnim.PlayWalk();
        rectTransform.DOAnchorPosX(startPos.x, walkDuration).SetEase(Ease.Linear).OnComplete(
            ()=> {
                catAnim.PlayIdle_3();
            });
    }
    public void PlayHappy()
    {
        StartCoroutine(DelayPetHappy());
    }
    IEnumerator DelayPetHappy()
    {
        catAnim.PlayHappy_1();
        yield return new WaitForSeconds(2);
        catAnim.PlayIdle_3();
    }
}

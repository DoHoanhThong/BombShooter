using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Ricimi;

public class RequestBubble : MonoBehaviour
{
    [SerializeField]
    private string feedScene = "FeedScene";
    [SerializeField]
    private string bathScene = "BathScene";
    [SerializeField]
    private SceneTransition transition;
    [SerializeField]
    private Animator bubbleAnim;
    [SerializeField]
    private GameObject chatBubble;
    [SerializeField]
    private GameObject alertIcon;
    [SerializeField]
    private Image bubble;
    [SerializeField]
    private Sprite feedSprite;
    [SerializeField]
    private Sprite bathSprite;
    [SerializeField]
    private ParticleSystem clickFX;
    [SerializeField]
    private float duration = .25f;

    //private PetController.Action currentRequest;
    private PetController currentPet;

    private float originScale;
    private void Awake()
    {
        originScale = transform.localScale.x;
        chatBubble.SetActive(false);
        alertIcon.SetActive(false);
    }
    public void OnRequestDone()
    {
        chatBubble.SetActive(false);
        bubbleAnim.SetBool("isAlerting", false);
        alertIcon.SetActive(false);
    }
    public void OnTrasitionScene()
    {
        SoundFXManager.Instance?.PlayCatYes();
        Destroy(Instantiate(clickFX, transform.position, Quaternion.identity), 2);
        gameObject.SetActive(false);
        Invoke("SceneTransition", 0.25f);
    }
    public void SceneTransition()
    {
        transition.PerformTransition();
    }
    public void RequestFeed()
    {
        chatBubble.SetActive(true);
        transition.SetTransitionScene(feedScene);        
        bubble.sprite = feedSprite;
        StartCoroutine(ShowAlert());
    }
    public void RequestBath()
    {
        chatBubble.SetActive(true);
        transition.SetTransitionScene(bathScene);
        bubble.sprite = bathSprite;
        StartCoroutine(ShowAlert());
    }
    IEnumerator ShowAlert()
    {
        yield return new WaitForSeconds(5);
        alertIcon.SetActive(true);
        bubbleAnim.SetBool("isAlerting", true);
    }
}

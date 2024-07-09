using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CircleTarget : MonoBehaviour
{
	public Vector3 rotate = Vector3.forward;
	public bool canRotate = true;
	public float speed = 30;


	[Header("Shake")]
	[SerializeField]
	private float _shakeDuration = 0.1f;
	[SerializeField]
	private float _shakeStrength = 0.1f;
	[SerializeField]
	private int _shakeVibrato = 1;
	[SerializeField]
	private float _shakeRandomness = 90;

	public virtual void Update()
	{
		if (canRotate)
			transform.Rotate(speed * rotate * Time.deltaTime);
	}
    public virtual void Start()
    {
		ThrowKnifeController.OnKnifeHitSuccess += Shake;
    }
    public virtual void OnDestroy()
    {
		ThrowKnifeController.OnKnifeHitSuccess -= Shake;
    }
	public virtual void Shake()
    {
		//transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
		transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
	}
    public void Reset()
    {
		foreach (var knife in transform.GetComponentsInChildren<KnifeScript>())
        {
			Destroy(knife.gameObject);
        }
    }
}

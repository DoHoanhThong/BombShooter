using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static System.Action ShakeScreen;
    Coroutine a;
    public AnimationCurve curve;
    public float shakeTime = 1;
    private void Awake()
    {
        //Screen.orientation = ScreenOrientation.Portrait;
        ShakeScreen += StartShake;
    }
    private void OnDestroy()
    {
        ShakeScreen -= StartShake;
    }
    public void StartShake()
    {
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
        a= StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        Vector3 startPos = this.transform.position;
        float timeUsed = 0;
        Handheld.Vibrate();
        while (timeUsed < shakeTime)
        {
            timeUsed += Time.deltaTime;
            float strength = curve.Evaluate(timeUsed);
            this.transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;  
        }
        this.transform.position = startPos;
    }
}

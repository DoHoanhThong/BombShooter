using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsrender : MonoBehaviour
{
    public float updateInterval = 0.5f;
    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;
    GUIStyle textStyle = new GUIStyle();
    private void Start()
    {
        timeleft = updateInterval;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        if (timeleft <= 0.0)
        {
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(5,5,100,25),fps.ToString("F2")+"FPS", textStyle);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceenLandscapeRotate : MonoBehaviour
{
    private bool isLandscapeLeft;
    private void Awake()
    {
        isLandscapeLeft = (Input.deviceOrientation == DeviceOrientation.LandscapeLeft) ;
    }
    void Update()
    {
        if (!isLandscapeLeft && Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            isLandscapeLeft = true;
        }
        else if (isLandscapeLeft && Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            isLandscapeLeft = false;
        }
    }
}

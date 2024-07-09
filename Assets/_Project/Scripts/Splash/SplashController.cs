using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    //float adsWaitCounter = 0;
    void Start()
    {
        StartCoroutine(InitializeScene());
    }
    IEnumerator InitializeScene()
    {
        StartCoroutine(CanvasSplashController.Instance.StartLoading());

        yield return new WaitForSeconds(0.5f);
      
        //FirebaseManager.Instance.Initialize();

        //load player data
        //GameManager.Instance.userData = GameTool.LoadUserData();    

        CanvasSplashController.Instance.SetMaxProgress(0.3f);


        AsyncOperation operation = new AsyncOperation();


        operation = SceneManager.LoadSceneAsync("Home");
       

        //GameManager.Instance.userData.currentLevel = 9;
        //operation = SceneManager.LoadSceneAsync("Level_" + GameManager.Instance.userData.currentLevel);


        operation.allowSceneActivation = false;

        //load firebase
        //FirebaseManager.Instance.Initialize();

        CanvasSplashController.Instance.SetMaxProgress(0.6f);

        //while (AdsController.Instance.IsInitialized == false && adsWaitCounter < 4)
        //while (adsWaitCounter < 4)
        //{
        //    adsWaitCounter += Time.deltaTime;
        //    yield return new WaitForEndOfFrame();
        //}
        yield return new WaitForSeconds(0.5f);
        CanvasSplashController.Instance.SetMaxProgress(0.8f);

        WaitForSeconds wait = new WaitForSeconds(0.5f);
        float waiter = 0;
        //while (waiter <= 3f && !GameManager.Instance.IsOAAReady())
        while (waiter <= 3f)
        {
            waiter += 0.5f;
            yield return wait;
        }
        
        //if (PlayerPrefs.GetInt("FirstOpenApp", 1) == 1)
        //{
        //    PlayerPrefs.SetInt("FirstOpenApp", 0);
        //}
        //else
        //{
            
        //}
        //GameManager.Instance.ShowOAA(true);
        yield return wait;
        //GameManager.Instance.ShowBanner();
        while (!operation.isDone)
        {


            if (operation.progress >= 0.9f)
            {
                CanvasSplashController.Instance.SetMaxProgress(1f);
                yield return new WaitUntil(() => CanvasSplashController.Instance.isCanvasLoadDone == true);
                operation.allowSceneActivation = true;

            }
            yield return null;
        }
        
        //GameManager.Instance.OnLevelLoaded?.Invoke();
    }
}

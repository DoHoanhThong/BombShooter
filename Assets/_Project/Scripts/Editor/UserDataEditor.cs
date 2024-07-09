using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UserDataEditor
{
    [MenuItem("UserData/Reset UserData")]
    private static void ClearUserData()
    {
        //ES3.Clear("UserData", new UserData());
       // ES3.DeleteKey("UserData");
        Debug.Log("UserData Reset!");
    }
    [MenuItem("UserData/Save UserData")]
    private static void SaveUserData()
    {
        //ES3.Clear("UserData", new UserData());
        if (GameManager.Instance != null)
        {
            GameTool.SaveUserData(GameManager.Instance.userData);
            Debug.Log("UserData Saved!");
        }
        else
        {
            Debug.Log("Save in Runtime Only");
        }
        
    }
}


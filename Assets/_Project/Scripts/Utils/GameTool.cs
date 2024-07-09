using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEditor;
using TeraJet;

public static class GameTool
{

    #region EXCUTE_FUNCTION
    public static Vector2 ZRotationToVector2(float z)
    {
        float fRotation = z * Mathf.Deg2Rad;
        float fX = Mathf.Sin(fRotation);
        float fY = Mathf.Cos(fRotation);

        return new Vector2(fY, fX); ;
    }
    public static float CalculateVelocity(float distance, float time)
    {
        return distance / time;
    }
    public static float CalculateAcceleration(float deltaVelocity, float deltaTime)
    {
        return deltaVelocity / deltaTime;
    }
    public static Vector2 GetRandomDirection()
    {
        float x = UnityEngine.Random.Range(-1.0f, 1.0f);
        float y;

        if (UnityEngine.Random.value < 0.5f)
        {
            y = 1.0f - Mathf.Abs(x);
        }
        else
        {
            y = -1.0f + Mathf.Abs(x);
        }

        return new Vector2(x, y).normalized;
    }
    public static void ResetOfflineTime(DateTime date)
    {
        PlayerPrefs.SetString("LastOffline", date.ToString());
    }
    public static T[] RemoveFirstArrayElement<T>(this T[] original)
    {
        T[] finalArray = new T[original.Length - 1];
        for (int i = 0; i < original.Length - 1; i++)
        {
            original[i] = original[i + 1];
            finalArray[i] = original[i];
        }
        return finalArray;
    }
    public static T[] RemoveArrayElement<T>(this T[] original, int index)
    {
        T[] finalArray = new T[original.Length - 1];
        int finalIndex = -1;
        for (int i = 0; i < original.Length; i++)
        {
            if (i != index)
            {
                finalIndex++;
                finalArray[finalIndex] = original[i];

            }
        }
        return finalArray;
    }
    public static bool UnityInternetAvailable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region CONVERT
    public static string ConvertToTens(this int origin)
    {
        if (origin < 10)
        {
            return "0" + origin.ToString();
        }
        else
        {
            return origin.ToString();
        }
    }
    public static string MinuteToHour(this int minutes)
    {
        string time = "";
        if ((int)(minutes / 60.0f) >= 10)
        {
            time += ((int)(minutes / 60.0f)).ToString();
        }
        else
        {
            time += "0" + ((int)(minutes / 60.0f)).ToString();
        }
        if ((int)(minutes % 60) >= 10)
        {
            time += "h" + ((int)(minutes % 60)).ToString() + "m";
        }
        else
        {
            time += "h0" + ((int)(minutes % 60)).ToString() + "m";
        }
        return time;
    }
    public static string SecondsToMinute(this int seconds)
    {
        if (seconds >= 60)
        {
            return ((int)(seconds / 60.0f)).ToString() + "m" + ((int)(seconds % 60)).ToString() + "s";
        }
        return seconds.ToString() + "s";
    }
    public static string SecondsToHour(this int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    static string[] prefixes = { "", "K", "M", "B", "T", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT",
                                    "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT",
                                    "BU", "BV", "BW", "BX", "BY", "BZ"};
    public static string ToMoney(this double _money)
    {

        int index = 0;
        double newValue = _money;
        if (newValue < 1000)
        {
            return Math.Round(_money).ToString();
        }

        while (newValue >= 1000)
        {
            newValue /= 1000;
            index++;
        }
        return $"{newValue:0.#} {prefixes[index]}";
    }
    public static string RankIntToText(this int index)
    {
        switch (index + 1)
        {
            case 1:
                return "1st";
            case 2:

                return "2nd";
            case 3:

                return "3rd";
            default:
                return index.ToString() + "th";
        }
    }
    public static float ConvertPixelsToDp(float px)
    {
        float density = Screen.dpi / 160f;
        return px / density;
    }
    #endregion

    #region PLAYER_DATA

    //public static UserData LoadUserData()
    //{
        //string path = Application.persistentDataPath + PlayerPrefsConfig.PLAYER_DATA;
        //if (File.Exists(path))
        //{
        //    Debug.Log("Saved file found in " + path);

        //    BinaryFormatter formatter = new BinaryFormatter();

        //    FileStream stream = new FileStream(path, FileMode.Open);

        //    UserData data = formatter.Deserialize(stream) as UserData;

        //    data.CheckNullableObject();

        //    stream.Close();

        //    return data;
        //}
        //else
        //{
        //    Debug.LogWarning("Saved file not found in " + path + " Generate Zero Data");
        //    UserData data = new UserData();
        //    return data;
        //}
        //UserData userData = ES3.Load("UserData", new UserData());
        //return userData;
    //}
    public static void SaveUserData(UserData data)
    {
        //BinaryFormatter formatter = new BinaryFormatter();

        //string path = Application.persistentDataPath + PlayerPrefsConfig.PLAYER_DATA;

        //FileStream stream = new FileStream(path, FileMode.Create);

        //formatter.Serialize(stream, data);
        //stream.Close();

        //Debug.Log(data.currentCoin);

        //ES3.Save("UserData", data);
    }

    #endregion
}
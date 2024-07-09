using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEditor;
using TeraJet;
using BlockPuzzle;
namespace HwiTera
{
    public static class CustomLib
    {

        #region EXCUTE_FUNCTION
        public static Vector2 ZRotationToVector2(float z)
        {
            float fRotation = z * Mathf.Deg2Rad;
            float fX = Mathf.Sin(fRotation);
            float fY = Mathf.Cos(fRotation);

            return new Vector2(fY, fX); ;
        }

        public static float CameraHeight()
        {
            return Camera.main.orthographicSize * 2;
        }
        public static float CameraWidth()
        {
            return CameraHeight() * Camera.main.aspect;
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
        public static bool IsOutCameraRange(Vector2 pos)
        {
            return Mathf.Abs(pos.x) > Camera.main.transform.position.x + CameraWidth() || Mathf.Abs(pos.y) > Camera.main.transform.position.y + CameraHeight();
        }
        //public static string GoldToString(this int origin)
        //{
        //    string S = origin.ToString();
        //    int startIndex = S.Length % 3;
        //    for (int i = startIndex; i < S.Length; i += 3)
        //    {
        //        S.Insert(i, ",");
        //    }
        //    return S;
        //}
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
        //public static int CompoundInterest(int origin, int month, float rate)
        //{
        //    //Debug.Log(origin * Mathf.Pow((1 + rate / 100f), month));
        //    return (int) (origin * Mathf.Pow((1f + rate * 1.0f / 100f), month));
        //}
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
            //return (((int)(minutes / 60.0f)) >= 10 ? ((int)(minutes / 60.0f)).ToString() : "0" + ((int)(minutes / 60.0f)).ToString()) + "h" + ((int)(minutes % 60)) >= 10 ? ((int)(minutes % 60)).ToString() : ("0" + ((int)(minutes % 60)).ToString()) + "m";
        }
        public static string SecondsToMinute(this int seconds)
        {
            if (seconds >= 60)
            {
                return ((int)(seconds / 60.0f)).ToString() + "m" + ((int)(seconds % 60)).ToString() + "s";
            }
            return seconds.ToString() + "s";
        }
        public static string ToMoney(this int _money)
        {
            float quintillion = (float)_money / 1000000000000000000;
            if (quintillion >= 1)

                return Math.Round(quintillion, 1).ToString() + "P";

            float quadrillion = (float)_money / 1000000000000000;
            if (quadrillion >= 1)

                return Math.Round(quadrillion, 1).ToString() + "Q";

            float trillion = (float)_money / 1000000000000;
            if (trillion >= 1)

                return Math.Round(trillion, 1).ToString() + "T";

            float billion = (float)_money / 1000000000;
            if (billion >= 1)

                return Math.Round(billion, 1).ToString() + "B";

            float million = (float)_money / 1000000;
            if (million >= 1)

                return Math.Round(million, 1).ToString() + "M";
            float thousand = (float)_money / 1000;
            if (thousand >= 1)
            {

                return Math.Round(thousand, 1).ToString() + "K";
            }

            return Math.Round((float)_money).ToString();
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
        #endregion

        #region PLAYER_DATA

        public static UserData LoadPlayerData()
        {
            string path = Application.persistentDataPath + PlayerPrefsConfig.PLAYER_DATA;
            if (File.Exists(path))
            {
                Debug.Log("Saved file found in " + path);

                BinaryFormatter formatter = new BinaryFormatter();

                FileStream stream = new FileStream(path, FileMode.Open);

                //PlayerData data = formatter.Deserialize(stream) as PlayerData;

                UserData data = formatter.Deserialize(stream) as UserData;

                data.CheckNullableObject();

                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Saved file not found in " + path + " Generate Zero Data");
                //PlayerData data = new PlayerData();
                UserData data = new UserData();
                return data;
            }
        }
        public static void SavePlayerData(UserData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + PlayerPrefsConfig.PLAYER_DATA;

            FileStream stream = new FileStream(path, FileMode.Create);

            // PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        #endregion       
    }
}
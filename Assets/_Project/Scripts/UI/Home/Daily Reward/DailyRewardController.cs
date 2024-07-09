using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardController
{
    private const string c_firstDayDate = "FirstDayDate";
    private const string c_lastClaimDay = "LastClaimDay";
    private const string c_nextClaimDay = "NextClaimDay";
    private const string c_dayClaim = "DayClaim";

    private const string c_lastReward = "LastReward";
    public DateTime FirstDayDate
    {
        get => GetDate(c_firstDayDate);
        set => SetDate(c_firstDayDate, value);
    }

    public DateTime LastClaimDay
    {
        get => GetDate(c_lastClaimDay);
        set => SetDate(c_lastClaimDay, value);  
    }
    public DateTime NextClaimDay
    {
        get => GetDate(c_nextClaimDay);
        set => SetDate(c_nextClaimDay, value);
    }
    public DailyRewardController()
    {

    }  
    

    public void SetDate(string key,DateTime date)
    {
        PlayerPrefs.SetString(key, date.ToString());
    }

    public DateTime GetDate(string key)
    {
        DateTime date = DateTime.Now;

        DateTime.TryParse(GetDateString(key), out date);

        return date;
    }
    string GetDateString(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            SetDate(key, DateTime.Now);
        }
        return PlayerPrefs.GetString(key);
    }
    string GetDateString(string key, DateTime defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            SetDate(key, defaultValue);
        }
        return PlayerPrefs.GetString(key);
    }
    public int GetDayClaimStatus(int day)
    {
        return PlayerPrefs.GetInt(c_dayClaim + day, 0);
    }
    public void SetDayClaimed(int day, int value)
    {
        PlayerPrefs.SetInt(c_dayClaim + day, value);
    }
}

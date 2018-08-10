using Newtonsoft.Json;
using System;
using UnityEngine;

public class User
{
    private bool isNoAds;

    private int coin;

    private struct VideoCoinData
    {
        public string date;
        public int chanceToday;
    }

    private VideoCoinData coinVideoData;

    private int adFreeNum = 1;
    private int playCount;

    public bool IsVideoCoinAvailable { get { return coinVideoData.chanceToday > 0; } }

    public int Coins
    {
        get { return coin; }
        set
        {
            coin = value;
            PlayerPrefs.SetInt(PrefsName.Coins, coin);
            Game.EventManager.Send(EventName.CoinChanged);
        }
    }

    public void HasWatchVideoOfCoin()
    {
        coinVideoData.chanceToday--;
        DateTime dt = DateTime.Now;
        coinVideoData.date = dt.ToShortDateString();
        string textJson = JsonConvert.SerializeObject(coinVideoData);
        PlayerPrefs.SetString(PrefsName.VideoCoinChance, textJson);
    }

    public bool IsNoAds
    {
        get { return isNoAds; }
        set
        {
            isNoAds = value;
            PlayerPrefs.SetInt(PrefsName.NoAds, isNoAds ? 1 : 0);
            Game.EventManager.Send(EventName.RemoveAds);
        }
    }

    public bool IsNoInterstitial
    {
        get { return isNoAds || playCount < adFreeNum; }
    }

    private User()
    {
    }

    public void Init()
    {
        Coins = PlayerPrefs.GetInt(PrefsName.Coins, 0);

        IsNoAds = PlayerPrefs.GetInt(PrefsName.NoAds, 0) == 1;

        DateTime dt = DateTime.Now;
        string date = dt.ToShortDateString();
        string s = PlayerPrefs.GetString(PrefsName.VideoCoinChance, string.Empty);
        try
        {
            coinVideoData = JsonConvert.DeserializeObject<VideoCoinData>(s);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
        if (coinVideoData.date != date)
        {
            coinVideoData.chanceToday = Game.Config.VideoCoinChancePerDay;
        }

        // 存档相关
        //PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
        //PlayGamesPlatform.InitializeInstance(configuration);
        //PlayGamesPlatform.DebugLogEnabled = true;
        //PlayGamesPlatform.Activate();
    }

    public void PlayCountIncrease()
    {
        playCount++;
    }
}
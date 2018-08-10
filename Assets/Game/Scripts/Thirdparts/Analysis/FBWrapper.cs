using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class FBWrapper : IAnalysis
{
    public void Init()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(FB_InitCallback, FB_OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void FB_InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            Debug.Log("You may see results showing up at https://www.facebook.com/analytics/" + FB.AppId);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void FB_OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void OnStartGame()
    {
        FB.LogAppEvent(AnalysisEventName.JY_START_GAME_001, null, null);
    }

    public void OnClickMall()
    {
        FB.LogAppEvent(AnalysisEventName.JY_MALL_002, null, null);
    }

    public void OnClickNoAds()
    {
        FB.LogAppEvent(AnalysisEventName.JY_NO_AD_003, null, null);
    }

    public void OnClickRank()
    {
        FB.LogAppEvent(AnalysisEventName.JY_RANK_004, null, null);
    }

    public void OnBuySkin(int skinId)
    {
        FB.LogAppEvent(AnalysisEventName.JY_LOVE_SKIN_005, null, new Dictionary<string, object>() { { "skinId", skinId } });
    }

    public void OnClickPurchaseItem(string productID)
    {
        FB.LogAppEvent(AnalysisEventName.JY_PAY_006, null, new Dictionary<string, object>() { { "productID", productID } });
    }

    public void OnClickWatchVideoCoin()
    {
        FB.LogAppEvent(AnalysisEventName.JY_TV_COIN_007, null, null);
    }

    public void OnWatchVideoCoinSucc()
    {
        FB.LogAppEvent(AnalysisEventName.JY_TV_COIN_GET_008, null, null);
    }

    public void OnChangeSkin(int skinId)
    {
        FB.LogAppEvent(AnalysisEventName.JY_POPULAR_SKIN_009, null, new Dictionary<string, object>() { { "skinId", skinId } });
    }

    public void OnContiuneGame()
    {
        FB.LogAppEvent(AnalysisEventName.JY_CONTIUNE_GAME_010, null, null);
    }

    public void OnSoloMode()
    {
        FB.LogAppEvent(AnalysisEventName.JY_SOL_OMODE_011, null, null);
    }

    public void OnVersusMode()
    {
        FB.LogAppEvent(AnalysisEventName.JY_V_SMODE_012, null, null);
    }
}

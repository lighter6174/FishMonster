public class FirebaseWrapper : IAnalysis
{
    public void Init()
    {
          // Firebase 在第一次调用LogEvent的时候进行初始化，因此需要在载入游戏的时候调用此函数进行初始化
        Firebase.Analytics.FirebaseAnalytics.LogEvent("[jy]init");
    }

    public void OnStartGame()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_START_GAME_001);
    }

    public void OnClickMall()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_MALL_002);
    }

    public void OnClickNoAds()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_NO_AD_003);
    }

    public void OnClickRank()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_RANK_004);
    }

    public void OnBuySkin(int skinId)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_LOVE_SKIN_005, "skinId", skinId);
    }

    public void OnClickPurchaseItem(string productID)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_PAY_006, "productID", productID);
    }

    public void OnClickWatchVideoCoin()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_TV_COIN_007);
    }

    public void OnWatchVideoCoinSucc()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_TV_COIN_GET_008);
    }

    public void OnChangeSkin(int skinId)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_POPULAR_SKIN_009, "skinId", skinId);
    }

    public void OnContiuneGame()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_CONTIUNE_GAME_010);
    }

    public void OnSoloMode()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_SOL_OMODE_011);
    }

    public void OnVersusMode()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalysisEventName.JY_V_SMODE_012);
    }
}

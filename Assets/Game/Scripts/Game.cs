using MoleMole;
using System;

public class Game
{
    public static bool IsChallengeVersion;

    public static bool IsAllModuleInited { get { return IsAnalysisInited && IsAdsInited; } }

    public static bool IsAnalysisInited { get; set; }

    public static bool IsAdsInited { get; set; }

    //private static UIManager uiManager;

    //public static UIManager UIManager
    //{
    //    get
    //    {
    //        if (uiManager != null)
    //            return uiManager;
    //        Singleton<UIManager>.Create();
    //        uiManager = Singleton<UIManager>.Instance;
    //        return uiManager;
    //    }
    //}

    private static Shop shop;

    public static Shop Shop
    {
        get
        {
            if (shop != null)
                return shop;
            Singleton<Shop>.Create();
            shop = Singleton<Shop>.Instance;
            return shop;
        }
    }

    private static User user;

    public static User User
    {
        get
        {
            if (user == null)
            {
                Singleton<User>.Create();
                user = Singleton<User>.Instance;
            }
            return user;
        }
    }

    private static TipboxManager tipboxManager;

    public static TipboxManager TipboxManager
    {
        get
        {
            if (tipboxManager == null)
            {
                Singleton<TipboxManager>.Create();
                tipboxManager = Singleton<TipboxManager>.Instance;
            }
            return tipboxManager;
        }
    }

    private static GameConfig gameConfig;

    public static GameConfig Config
    {
        get
        {
            if (gameConfig == null)
            {
                gameConfig = GameConfig.Load();
#if UNITY_EDITOR
                if (gameConfig == null)
                {
                    throw new Exception("GameConfig载入失败");
                }
#endif
            }
            return gameConfig;
        }
    }

    private static EventManager eventManager;

    public static EventManager EventManager
    {
        get
        {
            if (eventManager == null)
            {
                Singleton<EventManager>.Create();
                eventManager = Singleton<EventManager>.Instance;
            }
            return eventManager;
        }
    }
}
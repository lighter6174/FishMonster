// 事件用字符串来标识，枚举值不方便序列化
public static class EventName
{
    public const string None = "None";
    public const string Test = "Test";
    public const string CurrentScoreChanged = "CurrentScoreChanged";
    public const string HighScoreChanged = "HighScoreChanged";
    public const string CoinChanged = "CoinChanged";
    public const string ItemSelectChanged = "ItemSelectChanged";
    public const string PurchaseOk = "PurchaseOk";
    public const string RemoveAds = "RemoveAds";
    public const string Relive = "Relive";
    public const string PauseGame = "PauseGame";
    public const string StaminaChanged = "StaminaChanged";
    public const string KeyChanged = "KeyChanged";
    public const string LevelItemClicked = "LevelItemClicked";
    public const string ChallengeBuyBall = "ChallengeBuyBall";
    public const string GotoMainMenuScreen = "GotoMainMenuScreen";
    public const string GotoShop = "GotoShop";
    public const string Rate = "Rate";
    public const string ShowRank = "ShowRank";
    public const string StartGameSolo = "StartGameSolo";
    public const string StartGameVersus = "StartGameVersus";
    public const string GameStarted = "GameStarted";
    public const string UpdateBallPos = "UpdateBallPos";
    public const string NoAds = "NoAds";
    public const string GameModeChanged = "GameModeChanged";
}

public class EventData
{
}

public class RechargeEventData : EventData
{
    public string identifier;
}
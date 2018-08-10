public interface IAnalysis
{
    void Init();

    /// <summary>
    /// 每天游戏开局最多次数/人数
    /// </summary>
    void OnStartGame();

    /// <summary>
    /// 点击【商城】次数/人数
    /// </summary>
    void OnClickMall();

    /// <summary>
    /// 点击【去除广告】次数/人数
    /// </summary>
    void OnClickNoAds();

    /// <summary>
    /// 点击【排行榜】次数/人数
    /// </summary>
    void OnClickRank();

    /// <summary>
    /// 解锁蛇皮肤最多次数/人数
    /// </summary>
    void OnBuySkin(int skinId);

    /// <summary>
    /// 支付金额总数最多/人数
    /// </summary>
    void OnClickPurchaseItem(string productID);

    /// <summary>
    /// 点击【免费获得货币】次数/人数
    /// </summary>
    void OnClickWatchVideoCoin();

    /// <summary>
    /// 真实【免费获得货币】次数/人数
    /// </summary>
    void OnWatchVideoCoinSucc();

    /// <summary>
    /// 选择最多的皮肤ID/人数
    /// </summary>
    void OnChangeSkin(int skinId);

    /// <summary>
    /// 点击【广告】继续挑战次数/人数
    /// </summary>
    void OnContiuneGame();

    /// <summary>
    /// 每天游玩SOLO模式次数/人数
    /// </summary>
    void OnSoloMode();

    /// <summary>
    /// 每天游玩VS模式次数/人数
    /// </summary>
    void OnVersusMode();
}

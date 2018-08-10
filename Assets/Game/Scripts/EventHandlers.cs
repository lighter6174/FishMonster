// 事件处理机制之一，将事件的处理逻辑放到各个事件处理类里

using UnityEngine;
using UnityEngine.Purchasing;

[GEvent(EventName.StartGameSolo)]
public class StartGameSoloEvent : AEvent
{
    public override void Run()
    {
        //GamePlay.Instance.StartGameSolo();
    }
}

[GEvent(EventName.StartGameVersus)]
public class StartGameVersusEvent : AEvent
{
    public override void Run()
    {
        //GamePlay.Instance.StartGameVersus();
    }
}

[GEvent(EventName.GotoShop)]
public class GotoShopEvent : AEvent
{
    public override void Run()
    {
    }
}

[GEvent(EventName.Rate)]
public class RateEvent : AEvent
{
    public override void Run()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}

[GEvent(EventName.ShowRank)]
public class ShowRankEvent : AEvent
{
    public override void Run()
    {
    }
}

[GEvent(EventName.PurchaseOk)]
public class PurchaseOkEvent : AEvent<RechargeEventData>
{
    public override void Run(RechargeEventData eventData)
    {
        string identifier = eventData.identifier;
        var item = Game.Shop.GetRechargePoint(identifier);
        if (item.Type == (int)ProductType.Consumable)
        {
            Game.User.Coins += item.Coin;
        }
        else if (item.Type == (int)ProductType.NonConsumable && string.Equals(identifier, "remove_ads"))
        {
            Game.User.IsNoAds = true;
            Ads.Instance.HideBanner();
        }
        else
        {
            Debug.LogError("recharge point error, product identifier = " + identifier + ",Type = " + item.Type);
        }
    }
}
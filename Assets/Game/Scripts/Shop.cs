using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public struct RechargeData
{
    #region Json Table Fields

    public int Id;
    public int Coin;
    public int Type;
    public float Price;
    public string Identifier;

    #endregion Json Table Fields
}

public enum SkinType
{
    Sprite = 1,
}

// 皮肤的变种如果实在太多，就使用组件式设计
public class ShopItemData
{
    #region Json Table Fields

    public int Id;
    public int Type;
    public int Grade;
    public int Coin;
    public string HeadPic;
    public string BodyPic1;
    public string BodyPic2;
    public string TailPic;
    public string ShowPic;

    public float HeadSpacing;
    public float BodySpacing1;
    public float BodySpacing2;
    public float TailSpacing;

    [JsonConverter(typeof(TupleColorConvert))]
    public Color DominantColor;

    #endregion Json Table Fields

    public bool IsUnlock;

    public void LoadUserData()
    {
    }

    public void Unlock()
    {
        IsUnlock = true;
        Game.Shop.SaveItemUnlockData();
    }

    // 皮肤暂时没那么复杂，没必要过度设计
    public Color GetBallItemColor()
    {
        return Color.white;
    }

    public Sprite ShowSprite
    {
        get
        {
            // 不做缓存，皮肤数量众多，这个简单的做缓存，内存消耗较大。有时间倒可以做个缓存池
            string path = ShowPic.Replace(".png", "");
            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }

    public Sprite HeadPicSprite
    {
        get
        {
            string path = HeadPic.Replace(".png", "");
            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }

    public Sprite BodyPicSprite1
    {
        get
        {
            if (BodyPic1 == "")
            {
                return null;
            }
            string path = BodyPic1.Replace(".png", "");
            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }

    public Sprite BodyPicSprite2
    {
        get
        {
            if (BodyPic2 == "")
            {
                return null;
            }
            string path = BodyPic2.Replace(".png", "");
            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }

    public Sprite TailPicSprite
    {
        get
        {
            if (TailPic == "")
            {
                return null;
            }
            string path = TailPic.Replace(".png", "");
            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }
}

public class Shop
{
    public List<RechargeData> RechargeTable;

    public List<ShopItemData> SnakeSkinData = new List<ShopItemData>();

    public ShopItemData CurrentSnakeSkin;

    private Shop()
    {
        LoadRechargeTable();
        LoadSnakeSkinTable();
    }

    private void LoadRechargeTable()
    {
        var ct = Resources.Load<TextAsset>("Table/shop.recharge.json").text;
        var arrdata1 = Newtonsoft.Json.Linq.JArray.Parse(ct);
        RechargeTable = arrdata1.ToObject<List<RechargeData>>();
    }

    private void LoadSnakeSkinTable()
    {
        var si = Resources.Load<TextAsset>("Table/shop.item.json").text;
        var arrdata2 = Newtonsoft.Json.Linq.JArray.Parse(si);
        SnakeSkinData = arrdata2.ToObject<List<ShopItemData>>();

        Dictionary<int, bool> itemUnlockData = LoadItemUnlockData();

        foreach (ShopItemData item in SnakeSkinData)
        {
            item.LoadUserData();
            bool isUnlock = false;
            itemUnlockData.TryGetValue(item.Id, out isUnlock);
            if (item.Coin == 0)
            {
                isUnlock = true;
            }
            item.IsUnlock = isUnlock;
        }

        RefreshCurrentUsedItem(out CurrentSnakeSkin, PrefsName.CurrentSnakeSkinId, SnakeSkinData);
    }

    private void RefreshCurrentUsedItem(out ShopItemData currentItem, string itemType, List<ShopItemData> itemList)
    {
        if (itemList == null || itemList.Count <= 0)
        {
            currentItem = null;
            return;
        }
        int currId = PlayerPrefs.GetInt(itemType, 0);
        currentItem = itemList.Find(t => currId == t.Id);
        currentItem = currentItem ?? itemList[0];
        currentItem = currentItem.IsUnlock ? currentItem : itemList[0];
    }

    public Dictionary<int, bool> LoadItemUnlockData()
    {
        var text = PlayerPrefs.GetString(PrefsName.UnlockSnakeSkin, "");
        Dictionary<int, bool> itemUnlockData = null;
        try
        {
            itemUnlockData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, bool>>(text);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
        itemUnlockData = itemUnlockData ?? new Dictionary<int, bool>();
        return itemUnlockData;
    }

    public void SaveItemUnlockData()
    {
        Dictionary<int, int> itemUnlockData = new Dictionary<int, int>();
        foreach (var item in SnakeSkinData)
        {
            itemUnlockData.Add(item.Id, item.IsUnlock ? 1 : 0);
        }
        string text = Newtonsoft.Json.JsonConvert.SerializeObject(itemUnlockData);
        PlayerPrefs.SetString(PrefsName.UnlockSnakeSkin, text);
    }

    public void BuyItem(ShopItemData item)
    {
        if (item.Coin <= Game.User.Coins)
        {
            Game.User.Coins -= item.Coin;
            item.Unlock();
            SelectItem(item);

            if (item.Type == 1)
            {
                Analysis.Instance.OnBuySkin(item.Id);
            }
        }
    }

    public void SelectItem(ShopItemData item)
    {
        if (item.Type == 1)
        {
            CurrentSnakeSkin = item;
            PlayerPrefs.SetInt(PrefsName.CurrentSnakeSkinId, CurrentSnakeSkin.Id);
            Analysis.Instance.OnChangeSkin(item.Id);
        }

        Game.EventManager.Send(EventName.ItemSelectChanged);
    }

    public void AddProduct(ConfigurationBuilder build)
    {
        foreach (var item in RechargeTable)
        {
            build.AddProduct(item.Identifier, (ProductType)item.Type);
        }
    }

    public RechargeData GetRechargePoint(string id)
    {
        foreach (var item in RechargeTable)
        {
            if (item.Identifier == id)
            {
                return item;
            }
        }
        return new RechargeData();
    }

    public void Recharge(int idx)
    {
        foreach (var item in RechargeTable)
        {
            if (item.Id == idx)
            {
                IAP.Instance.Purchase(item.Identifier);
                Analysis.Instance.OnClickPurchaseItem(item.Identifier);
                break;
            }
        }
    }

    public void NoAds()
    {
        Recharge(4);
        Analysis.Instance.OnClickNoAds();
    }
}
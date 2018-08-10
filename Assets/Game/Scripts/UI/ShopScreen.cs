using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopScreen : SimpleMenu<ShopScreen>
{
    public CoinItem[] CoinItems;

    public Button videoBtn;
    public GameObject mask;

    public RectTransform scrollContent;

    public GameObject[] posList;

    public float ShopItemW = 200f;
    public float ShopItemH = 170f;
    public float ShopItemMargin = 10f;

    public GameObject shopItemPrefab;

    private readonly List<GameObject> itemList = new List<GameObject>();
    private float[] scrollItemXList;

    private void Start()
    {
       // Game.EventManager.Register(this);
       // ResetRechargeItem();
      //  ResetShopItem();
      //  RefreshVideoButton();
    }

    private void OnEnable()
    {
        RefreshVideoButton();
    }

    private void RefreshVideoButton()
    {
        //videoBtn.interactable = Game.User.IsVideoCoinAvailable;
        //mask.SetActive(!Game.User.IsVideoCoinAvailable);
    }

    private void ResetRechargeItem()
    {
        int i = 0;
        foreach (var item in Game.Shop.RechargeTable)
        {
            if (item.Type == (int)ProductType.Consumable)
            {
                CoinItem oneItem = CoinItems[i++];
                oneItem.CoinText.text = string.Empty + "+" + item.Coin;
                oneItem.PriceText.text = "FOR $" + item.Price;
                if (i == 3)
                {
                    break;
                }
            }
        }
    }

    public void ResetShopItem()
    {
        if (scrollItemXList == null)
        {
            scrollItemXList = new float[posList.Length];
            for (int j = 0; j < posList.Length; j++)
            {
                scrollItemXList[j] = posList[j].GetComponent<RectTransform>().anchoredPosition.x;
            }
        }

        itemList.Clear();
        foreach (Transform item in scrollContent.transform)
        {
            if (item.name.Contains("ShopItem_"))
            {
                Destroy(item.gameObject);
            }
        }

        int skinNum = Game.Shop.SnakeSkinData.Count;
        float ballskinH = (skinNum / 3 + 1) * (ShopItemH + ShopItemMargin) + 10;
        var size1 = scrollContent.sizeDelta;
        scrollContent.sizeDelta = new Vector2(size1.x, ballskinH);

        for (int i = 0; i < skinNum; i++)
        {
            var data = Game.Shop.SnakeSkinData[i];
            GameObject item = Instantiate(shopItemPrefab);
            item.transform.SetParent(scrollContent.transform, false);
            item.GetComponent<ShopItem>().SetUp(data);
            item.GetComponent<ShopItem>().SetSelected(Game.Shop.CurrentSnakeSkin.Id);
            int col = i % 3;
            int row = i / 3;
            float x = scrollItemXList[col];
            float y = ShopItemH / 2f + row * (ShopItemH + ShopItemMargin) + ShopItemMargin - ballskinH / 2f;
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -y);
            item.name = "ShopItem_SnakeSkin_" + i;
            itemList.Add(item);
        }
    }

    private void RefreshShopItems()
    {
        foreach (var item in itemList)
        {
            item.GetComponent<ShopItem>().Refresh();
            item.GetComponent<ShopItem>().SetSelected(Game.Shop.CurrentSnakeSkin.Id);
        }
    }

    [GEventMethod(EventName.ItemSelectChanged)]
    private void OnItemSelectChanged()
    {
        RefreshShopItems();
    }

    public void OnRechargeClicked(int idx)
    {
        Game.Shop.Recharge(idx);
    }

    public void OnWatchVideoClicked()
    {
        //if (!Game.User.IsVideoCoinAvailable)
       //     return;

        Ads.Instance.ShowRewardedVideo((bool isReqSucc, bool isRewarded) =>
        {
            if (isReqSucc && isRewarded)
            {
                Game.User.Coins += 20;
                Game.User.HasWatchVideoOfCoin();
                RefreshVideoButton();
                Analysis.Instance.OnWatchVideoCoinSucc();
            }
        });
       // Analysis.Instance.OnClickWatchVideoCoin();
    }

    private void OnDestroy()
    {
        Game.EventManager.Unregister(this);
    }

    public void Back()
    {
        Hide();
    }

    public override void OnBackPressed()
    {
        Back();
    }
}
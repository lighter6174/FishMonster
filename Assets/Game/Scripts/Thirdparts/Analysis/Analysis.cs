using System.Collections.Generic;
using UnityEngine;

public class Analysis : MonoBehaviour
{
    public static Analysis Instance;

    private readonly List<IAnalysis> list = new List<IAnalysis>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    public void Init()
    {
        IAnalysis fb = new FBWrapper();
        IAnalysis firebase = new FirebaseWrapper();
        list.Add(fb);
        list.Add(firebase);

        foreach (var item in list)
        {
            item.Init();
        }

        Game.IsAnalysisInited = true;
    }

    public void OnStartGame()
    {
        foreach (var item in list)
        {
            item.OnStartGame();
        }
    }

    public void OnClickMall()
    {
        foreach (var item in list)
        {
            item.OnClickMall();
        }
    }

    public void OnClickNoAds()
    {
        foreach (var item in list)
        {
            item.OnClickNoAds();
        }
    }

    public void OnClickRank()
    {
        foreach (var item in list)
        {
            item.OnClickRank();
        }
    }

    public void OnBuySkin(int skinId)
    {
        foreach (var item in list)
        {
            item.OnBuySkin(skinId);
        }
    }

    public void OnClickPurchaseItem(string productID)
    {
        foreach (var item in list)
        {
            item.OnClickPurchaseItem(productID);
        }
    }

    public void OnClickWatchVideoCoin()
    {
        foreach (var item in list)
        {
            item.OnClickWatchVideoCoin();
        }
    }

    public void OnWatchVideoCoinSucc()
    {
        foreach (var item in list)
        {
            item.OnWatchVideoCoinSucc();
        }
    }

    public void OnChangeSkin(int skinId)
    {
        foreach (var item in list)
        {
            item.OnChangeSkin(skinId);
        }
    }

    public void OnContiuneGame()
    {
        foreach (var item in list)
        {
            item.OnContiuneGame();
        }
    }

    public void OnSoloMode()
    {
        foreach (var item in list)
        {
            item.OnSoloMode();
        }
    }

    public void OnVersusMode()
    {
        foreach (var item in list)
        {
            item.OnVersusMode();
        }
    }
}

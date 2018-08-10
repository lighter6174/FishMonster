using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdType
{
    Banner,
    Interstitial,
    RewardedVideo,
}

public enum AdError
{
}

public delegate void AdCallback(bool isReqSucc, bool isRewarded);

public class AdTask
{
    public int id;
    public AdType type;
    public int currAdIdx;
    public bool pending;
    public bool succ;
    public bool rewarded;
    public bool finished;
    public AdError error;
    public AdCallback callback;
}

public class Ads : MonoBehaviour
{
    public static Ads Instance;
    private IAd admob;
    private IAd unityAd;
    private IAd fbAd;
    private readonly List<IAd> ads = new List<IAd>();

    private int taskCount;

    public AdTask CurrTask { get; private set; }

    private void Awake()
    {
        Instance = this;
        taskCount = 1;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }

    public void Init()
    {
        ads.Clear();

#if !UNITY_EDITOR_WIN
        fbAd = new FBAd();
        fbAd.Init(gameObject);
        ads.Add(fbAd);
#endif
        Debug.Log("Start");
        admob = new AdMob();
        admob.Init(gameObject);
        ads.Add(admob);

        unityAd = new UnityAds();  // 放最后
        unityAd.Init(gameObject);
        ads.Add(unityAd);

        Game.IsAdsInited = true;
    }

    private AdTask CreateAdTask(AdCallback callback, AdType adType)
    {
        var task = new AdTask
        {
            id = taskCount++,
            type = adType,
            callback = callback
        };
        return task;
    }

    private void DoShowAd()
    {
        // Debug.Log("Play Ads");
        if (CurrTask == null)
        if (CurrTask.finished) return;
        if (!CurrTask.pending)
        {
            if (CurrTask.succ)
            {
                Debug.Log("[DoShowAd]Show Ad Succ, ad type:" + CurrTask.type);
                OnShowAdFinished();
                return;
            }

            if (CurrTask.currAdIdx < ads.Count)
            {
                var ad = ads[CurrTask.currAdIdx];
                switch (CurrTask.type)
                {
                    case AdType.Banner:
                        CurrTask.pending = true;
                        ad.ShowBanner();
                        break;

                    case AdType.Interstitial:
                        CurrTask.pending = true;
                        ad.ShowInterstitial();
                        break;

                    case AdType.RewardedVideo:
                        CurrTask.pending = true;
                        ad.ShowRewardedVideo();
                        break;

                    default:
                        break;
                }
                CurrTask.currAdIdx++;
            }
            else
            {
                Debug.Log("[DoShowAd]Show Ad Failed");
                OnShowAdFinished();
                return;
            }
        }
    }

    private void OnShowAdFinished()
    {
        Debug.Log("ad task finished: " + CurrTask);
        CurrTask.finished = true;
        StartCoroutine(OnAdCallback(CurrTask));
        CurrTask = null;
    }

    private IEnumerator OnAdCallback(AdTask task)
    {
        yield return null;
        task.callback(task.succ, task.rewarded);
        yield return null;
    }

    public void OnAdStateChanged(bool succ, bool pending, bool isRewarded = false)
    {
        if (CurrTask != null)
        {
            CurrTask.succ = succ;
            CurrTask.pending = pending;
            CurrTask.rewarded = CurrTask.rewarded || isRewarded;
            if (!CurrTask.pending)
            {
                StartCoroutine(OnNextAd());
            }
        }
    }

    private IEnumerator OnNextAd()
    {
        yield return null;
        DoShowAd();
        yield return null;
    }

    public void ShowBanner(AdCallback callback)
    {
        CurrTask = CreateAdTask(callback, AdType.Banner);
        DoShowAd();
    }

    public void HideBanner()
    {
        foreach (var ad in ads)
        {
            ad.HideBanner();
        }
    }

    public void ShowInterstitial(AdCallback callback)
    {
        CurrTask = CreateAdTask(callback, AdType.Interstitial);
        DoShowAd();
    }

    public void ShowRewardedVideo(AdCallback callback)
    {
        CurrTask = CreateAdTask(callback, AdType.RewardedVideo);
        DoShowAd();
    }

    private void OnDestroy()
    {
        foreach (var ad in ads)
        {
            ad.OnDestroy();
        }
    }
}
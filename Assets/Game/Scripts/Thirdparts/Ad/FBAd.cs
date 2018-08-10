using AudienceNetwork;
using UnityEngine;

public class FBAd : IAd
{
    private AdView bannerAdView;

    private InterstitialAd interstitialAd;
    private bool isInterstitialLoaded;

    private RewardedVideoAd rewardedVideoAd;
    private bool isRewardedVideoLoaded;

    private GameObject gameObject;

    private const string banner_placement_id = "711585492510540_711588232510266";
    private const string interstitial_placement_id = "1616009088434890_1673110276058104";
    private const string rewardedvideo_placement_id = "1616009088434890_1673109719391493";

    public void Init(GameObject go)
    {
        gameObject = go;
        InitInterstitial();
        LoadInterstitial();
        InitRewardedVideo();
        LoadRewardedVideo();
    }

    #region Banner

    public bool ShowBanner()
    {
        // Create a banner's ad view with a unique placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        bannerAdView = new AdView(banner_placement_id, AdSize.BANNER_HEIGHT_50);
        bannerAdView.Register(gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        bannerAdView.AdViewDidLoad = delegate ()
        {
            Debug.Log("Ad view loaded.");
            bool succ = bannerAdView.Show(AdPosition.BOTTOM);
            Ads.Instance.OnAdStateChanged(true, false);
        };
        bannerAdView.AdViewDidFailWithError = delegate (string error)
        {
            Debug.Log("Ad view failed to load with error: " + error);
            Ads.Instance.OnAdStateChanged(false, false);
        };
        bannerAdView.AdViewWillLogImpression = delegate ()
        {
            Debug.Log("Ad view logged impression.");
        };
        bannerAdView.AdViewDidClick = delegate ()
        {
            Debug.Log("Ad view clicked.");
        };

        // Initiate a request to load an ad.
        bannerAdView.LoadAd();

        return true;
    }

    public void HideBanner()
    {
        if (bannerAdView)
        {
            bannerAdView.Dispose();
        }
    }

    #endregion Banner

    #region Interstitial

    public bool ShowInterstitial()
    {
        if (isInterstitialLoaded)
        {
            interstitialAd.Show();
            isInterstitialLoaded = false;
            return true;
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    private void InitInterstitial()
    {
        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        interstitialAd = new InterstitialAd(interstitial_placement_id);
        interstitialAd.Register(gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        interstitialAd.InterstitialAdDidLoad = delegate ()
        {
            Debug.Log("Interstitial ad loaded.");
            isInterstitialLoaded = true;
        };
        interstitialAd.InterstitialAdDidFailWithError = delegate (string error)
        {
            Debug.Log("Interstitial ad failed to load with error: " + error);
        };
        interstitialAd.InterstitialAdWillLogImpression = delegate ()
        {
            Debug.Log("Interstitial ad logged impression.");
        };
        interstitialAd.InterstitialAdDidClick = delegate ()
        {
            Debug.Log("Interstitial ad clicked.");
        };
        interstitialAd.InterstitialAdDidClose = delegate ()
        {
            Debug.Log("Interstitial ad closed.");
            Ads.Instance.OnAdStateChanged(true, false);
            LoadInterstitial();
        };
    }

    private void LoadInterstitial()
    {
        interstitialAd.LoadAd();
    }

    #endregion Interstitial

    #region RewardedVideo

    public bool ShowRewardedVideo()
    {
        if (isRewardedVideoLoaded)
        {
            rewardedVideoAd.Show();
            isRewardedVideoLoaded = false;
            return true;
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    private void InitRewardedVideo()
    {
        // Create the rewarded video unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        rewardedVideoAd = new RewardedVideoAd(rewardedvideo_placement_id);
        rewardedVideoAd.Register(gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        rewardedVideoAd.RewardedVideoAdDidLoad = delegate ()
        {
            Debug.Log("RewardedVideo ad loaded.");
            isRewardedVideoLoaded = true;
        };
        rewardedVideoAd.RewardedVideoAdDidFailWithError = delegate (string error)
        {
            Debug.Log("RewardedVideo ad failed to load with error: " + error);
        };
        rewardedVideoAd.RewardedVideoAdWillLogImpression = delegate ()
        {
            Debug.Log("RewardedVideo ad logged impression.");
        };
        rewardedVideoAd.RewardedVideoAdDidClick = delegate ()
        {
            Debug.Log("RewardedVideo ad clicked.");
        };
        rewardedVideoAd.rewardedVideoAdComplete = delegate ()
        {
            Ads.Instance.OnAdStateChanged(true, true, true);
            Debug.Log("RewardedVideo ad complete.");
        };
        rewardedVideoAd.RewardedVideoAdDidSucceed = delegate ()
        {
            Ads.Instance.OnAdStateChanged(true, true, true);
            Debug.Log("RewardedVideo ad Succeed.");
        };
        rewardedVideoAd.rewardedVideoAdDidClose = delegate ()
        {
            Ads.Instance.OnAdStateChanged(true, false);
            Debug.Log("RewardedVideo ad closed.");
            LoadRewardedVideo();
        };
    }

    private void LoadRewardedVideo()
    {
        rewardedVideoAd.LoadAd();
    }

    #endregion RewardedVideo

    public void OnDestroy()
    {
        // Dispose of banner ad when the scene is destroyed
        if (bannerAdView)
        {
            bannerAdView.Dispose();
        }

        // Dispose of interstitial ad when the scene is destroyed
        if (interstitialAd != null)
        {
            interstitialAd.Dispose();
        }

        // Dispose of rewardedVideo ad when the scene is destroyed
        if (rewardedVideoAd != null)
        {
            rewardedVideoAd.Dispose();
        }
        Debug.Log("FBAd was destroyed!");
    }
}
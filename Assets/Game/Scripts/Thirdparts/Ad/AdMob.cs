using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdMob : IAd
{
    private string appid = ThirdpartParams.Instance.Get("admob_appid");
    private string unitid_banner = ThirdpartParams.Instance.Get("admob_unitid_banner");
    private string unitid_interstitial = ThirdpartParams.Instance.Get("admob_unitid_interstitial");
    private string unitid_rewarded_video = ThirdpartParams.Instance.Get("admob_unitid_rewarded_video");

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    public void Init(GameObject go)
    {
        MobileAds.Initialize(appid);
        InitRewardedVideo();
        RequestInterstitial();
        RequestRewardedVideo();
    }

    public void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
    }

    public bool ShowBanner()
    {
        if (bannerView == null)
        {
            bannerView = new BannerView(unitid_banner, AdSize.SmartBanner, AdPosition.Bottom);
            bannerView.OnAdClosed += (object sender, EventArgs args) => { };
            bannerView.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) =>
            {
                Ads.Instance.OnAdStateChanged(false, false);
            };
            bannerView.OnAdLoaded += (object sender, EventArgs args) =>
            {
                Ads.Instance.OnAdStateChanged(true, false);
            };
            bannerView.OnAdOpening += (object sender, EventArgs args) => { };
        }
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
        return true;
    }

    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }

    private void RequestInterstitial()
    {
        if (interstitial == null)
        {
            interstitial = new InterstitialAd(unitid_interstitial);
            interstitial.OnAdClosed += (object sender, EventArgs args) =>
            {
                Ads.Instance.OnAdStateChanged(true, false);
                Debug.Log("HandleAdClosed event received. sender: " + sender + ", args: " + args);
                RequestInterstitial();
            };
        }

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public bool ShowInterstitial()
    {
        if (interstitial != null)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
#if UNITY_EDITOR
                Ads.Instance.OnAdStateChanged(false, false);
#endif
                return true;
            }
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    public void RequestRewardedVideo()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        Debug.Log("admob RequestRewardedVideo: " + unitid_rewarded_video);
        rewardBasedVideo.LoadAd(request, unitid_rewarded_video);
    }

    public bool ShowRewardedVideo()
    {
        Debug.Log("ShowRewardedVideo, isLoad: " + rewardBasedVideo.IsLoaded());
        if (rewardBasedVideo.IsLoaded())
        {
            Debug.Log("rewardBasedVideo.IsLoaded: true");
            rewardBasedVideo.Show();
            return true;
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    private void InitRewardedVideo()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) =>
        {
            Debug.Log("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
        };
        rewardBasedVideo.OnAdRewarded += (object sender, Reward args) =>
        {
            string type = args.Type;
            double amount = args.Amount;
            Ads.Instance.OnAdStateChanged(true, true, amount > 0);
            Debug.Log("User rewarded with: " + amount + " " + type);
        };
        rewardBasedVideo.OnAdClosed += (object sender, EventArgs args) =>
        {
            Ads.Instance.OnAdStateChanged(true, false);
            RequestRewardedVideo();
        };
    }
}
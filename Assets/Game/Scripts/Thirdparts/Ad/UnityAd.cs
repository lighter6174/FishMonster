using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : IAd
{
#if UNITY_IOS
private string gameId = "1747291";
#elif UNITY_ANDROID
    private string gameId = "2612612";
#else
    private string gameId = "";
#endif

    public string placementId = "rewardedVideo";

    public void Init(GameObject go)
    {
        Debug.Log("Advertisement.isSupported: " + Advertisement.isSupported);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId);
        }
    }

    public void OnDestroy()
    {
    }

    public bool ShowBanner()
    {
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    public void HideBanner()
    {
    }

    public bool ShowInterstitial()
    {
        Debug.Log("[ShowInterstitial] Advertisement.IsReady: " + Advertisement.IsReady());
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = (ShowResult result) =>
            {
                if (result == ShowResult.Failed)
                {
                    Ads.Instance.OnAdStateChanged(false, false);
                }
                else if (result == ShowResult.Skipped)
                {
                    Ads.Instance.OnAdStateChanged(true, false);
                }
                else if (result == ShowResult.Finished)
                {
                    Ads.Instance.OnAdStateChanged(true, false);
                }
            };
            Advertisement.Show(options);
            return true;
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    public bool ShowRewardedVideo()
    {
        Debug.Log("[ShowRewardedVideo] Advertisement.IsReady: " + Advertisement.IsReady(placementId));
        if (Advertisement.IsReady(placementId))
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(placementId, options);
            return true;
        }
        Ads.Instance.OnAdStateChanged(false, false);
        return false;
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            // Reward your player here.
            Ads.Instance.OnAdStateChanged(true, false, true);
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
            Ads.Instance.OnAdStateChanged(true, false);
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            Ads.Instance.OnAdStateChanged(false, false);
        }
    }
}
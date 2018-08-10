using UnityEngine;

public interface IAd
{
    void Init(GameObject go);

    bool ShowBanner();

    void HideBanner();

    bool ShowInterstitial();

    bool ShowRewardedVideo();

    void OnDestroy();
}
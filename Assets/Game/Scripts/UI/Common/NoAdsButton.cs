using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NoAdsButton : MonoBehaviour
{
    private void Start()
    {
        UpdateNoAdsButton();
        GetComponent<Button>().onClick.AddListener(NoAds);
        Game.EventManager.Register(this);
    }

    private void OnDestroy()
    {
        Game.EventManager.Unregister(this);
    }

    private void OnEnable()
    {
        UpdateNoAdsButton();
    }

    [GEventMethod(EventName.RemoveAds)]
    private void OnRemoveAds()
    {
        PlayerPrefs.SetInt(PrefsName.NoAds, 1);
        UpdateNoAdsButton();
    }

    private void UpdateNoAdsButton()
    {
        if (PlayerPrefs.GetInt(PrefsName.NoAds, 0) == 1) //已去除广告
        {
            GetComponent<Image>().color = Color.gray;
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Button>().enabled = true;
        }
    }

    public void NoAds()
    {
        AudioManager.Instance.ButtonAudioSource.PlayOneShot(AudioManager.Instance.svs_click, 1f);
        Game.Shop.NoAds();
    }
}
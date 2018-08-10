using UnityEngine;
using UnityEngine.UI;

public class ReliveScreen : SimpleMenu<ReliveScreen>
{
    public GameObject CountdownNode;
    public Image CountdownImg;
    public Text CountdownText;
    private int countdown;

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        CountdownNode.SetActive(true);

        countdown = 10 * 60;
        CountdownText.text = "10";
        CountdownImg.fillAmount = 1;

        CancelInvoke("IncreaseTimer");
        IncreaseTimer();
    }

    private void IncreaseTimer()
    {
        --countdown;
        CountdownText.text = string.Empty + (int)(countdown / 60f + 1);
        CountdownImg.fillAmount = countdown / (10f * 60f);
        if (countdown > 0)
        {
            Invoke("IncreaseTimer", 1f / 60f);
        }
        else
        {
            Hide();
            GameOverScreen.Show();
        }
    }

    public void Relive()
    {
        CountdownNode.SetActive(false);
        CancelInvoke("IncreaseTimer");

#if UNITY_EDITOR
        DoRelive();
        return;
#endif

        if (Game.User.IsNoAds)
        {
            Invoke("DoRelive", 0.2f);
        }
        else
        {
            Ads.Instance.ShowRewardedVideo((bool isReqSucc, bool isRewarded) =>
            {
                Debug.Log("[ReliveScreen.Relive] isReqSucc: " + isReqSucc + "  ,  isRewarded: " + isRewarded);
                OnShowRewardedVideo(isReqSucc, isRewarded);
            });
        }
        Analysis.Instance.OnContiuneGame();
    }

    private void OnShowRewardedVideo(bool isReqSucc, bool isRewarded)
    {
        Debug.Log("[ReliveScreen.OnShowRewardedVideo] isReqSucc: " + isReqSucc + "  ,  isRewarded: " + isRewarded);
        if (isReqSucc)
        {
            if (isRewarded)
            {
                Debug.Log("ReliveScreen Relive");
                Invoke("DoRelive", 0.2f);
            }
            else
            {
                Hide();
                GameOverScreen.Show();
            }
        }
        else
        {
            Hide();
            GameOverScreen.Show();
        }
    }

    private void DoRelive()
    {
        Debug.Log("DoRelive Relive");
        Hide();
        //GamePlay.Instance.Relive();
    }

    private void DoRestart()
    {
        Hide();
        //GamePlay.Instance.Restart();
    }

    public void Restart()
    {
        if (Game.User.IsNoAds)
        {
            DoRestart();
        }
        else
        {
            Ads.Instance.ShowInterstitial((bool isReqSucc, bool isRewarded) =>
            {
                DoRestart();
            });
        }
    }

    public void GotoMainMenu()
    {
        Hide();
        MainScreen.Show();
    }

    public override void OnBackPressed()
    {
    }
}
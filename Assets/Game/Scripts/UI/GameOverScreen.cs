using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : SimpleMenu<GameOverScreen>
{
    public GameObject classicNode;
    public Text currentScore;
    public Text highScore;

    public GameObject versusNode;
    public Text currentVersusScore;
    public Text highVersusScore;

    private void Start()
    {
        RefreshScore();
    }

    private void OnEnable()
    {
        RefreshScore();
    }

    private void RefreshScore()
    {
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
        GotoMainMenu();
    }
}
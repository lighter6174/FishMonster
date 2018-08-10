using UnityEngine.UI;

public class GameScreen : SimpleMenu<GameScreen>
{
    public Text ScoreText;

    private void Start()
    {
        Game.EventManager.Register(this);
    }

    private void OnDestroy()
    {
        Game.EventManager.Unregister(this);
    }

    private void OnEnable()
    {
    }

    [GEventMethod(EventName.CurrentScoreChanged)]
    private void OnCurrentScoreChanged()
    {
    }

    public override void OnBackPressed()
    {
    }
}
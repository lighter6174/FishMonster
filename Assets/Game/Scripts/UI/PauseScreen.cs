public class PauseScreen : SimpleMenu<PauseScreen>
{
    private void Start()
    {
    }

    public void ResumeGame()
    {
    }

    public void Restart()
    {
        Hide();
        //GamePlay.Instance.Restart();
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
using UnityEngine;

public class MainScreen : SimpleMenu<MainScreen>
{
    private void Start()
    {
    }

    public void ShowShop()
    {
        ShopScreen.Show();
        Analysis.Instance.OnClickMall();
    }

    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
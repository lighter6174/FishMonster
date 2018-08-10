using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = string.Empty + Game.User.Coins;
        Game.EventManager.Register(this);
    }

    private void OnEnable()
    {
        text.text = string.Empty + Game.User.Coins;
    }

    private void OnDestroy()
    {
        Game.EventManager.Unregister(this);
    }

    [GEventMethod(EventName.CoinChanged)]
    private void OnCoinChanged()
    {
        text.text = string.Empty + Game.User.Coins;
    }
}
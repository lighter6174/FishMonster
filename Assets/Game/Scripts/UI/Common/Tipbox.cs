using UnityEngine;
using UnityEngine.UI;

public class Tipbox : MonoBehaviour
{
    private Text text;

    private void Start()
    {
    }

    public void SetUp(string msg)
    {
        text.text = msg;
        Invoke("StartFade", Game.Config.TipboxShowTime);
    }

    private void StartFade()
    {
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        Game.TipboxManager.RemoveTipbox(this);
    }
}
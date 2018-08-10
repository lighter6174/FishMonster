using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Text CoinText;
    public GameObject LockNode;
    public GameObject UnlockNode;
    public GameObject SelectBg;
    public Image ItemIcon;

    private ShopItemData data;
    private float lastClickTime;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Time.time - lastClickTime > 0.5f)
                {
                    lastClickTime = Time.time;
                    transform.localScale = Vector3.one;
                    transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1f), 0.4f, 1, 0.01f);
                }
            }
        );
    }

    public void SetUp(ShopItemData data)
    {
        this.data = data;
        CoinText.text = string.Empty + data.Coin;
        ItemIcon.overrideSprite = data.ShowSprite;
        Refresh();
    }

    public void Refresh()
    {
        bool isUnlock = data.IsUnlock;
        LockNode.SetActive(!isUnlock);
        UnlockNode.SetActive(isUnlock);
        transform.localScale = Vector3.one;

        if (data.ShowSprite == null)
        {
            ItemIcon.gameObject.SetActive(false);
        }
    }

    public void SetSelected(int currentId)
    {
        SelectBg.SetActive(currentId == data.Id);
    }

    public void OnClicked()
    {
        if (!data.IsUnlock)
        {
            Game.Shop.BuyItem(data);
            AudioManager.Instance.ButtonAudioSource.PlayOneShot(AudioManager.Instance.svs_selectnone, 1f);
        }
        else
        {
            Game.Shop.SelectItem(data);
            AudioManager.Instance.ButtonAudioSource.PlayOneShot(AudioManager.Instance.svs_select, 1f);
        }
    }
}
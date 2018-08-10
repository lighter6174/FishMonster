using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [ETitle("点击音效")]
    public bool isPlayClickSound = true;

    [ShowIf("isPlayClickSound")]
    public bool useGlobalClickSound = true;

    [ShowIf("isPlayClickSound")]
    public AudioClip audioClip;

    [ETitle("消息发送")]
    public string[] eventTypes;

    [ETitle("点击缩放")]
    public bool isScaleOnClick;  // 点击时，按钮是否有缩放的效果

    [ShowIf("isScaleOnClick")]
    public float scale = 1.2f;

    [ETitle("点击回震")]
    public bool isPunchScaleAfterClick;  // 点击完毕后，按钮是否震一下

    [ShowIf("isPunchScaleAfterClick")]
    public Vector3 punch = new Vector3(0.1f, 0.1f, 1f);

    [ShowIf("isPunchScaleAfterClick")]
    public float punchDuration = 0.4f;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate () { OnClick(); });
    }

    public void PlayClickSound()
    {
        if (useGlobalClickSound)
        {
            AudioManager.Instance.ButtonAudioSource.PlayOneShot(AudioManager.Instance.svs_click, 1f);
        }
        else
        {
            if (audioClip != null)
            {
                AudioManager.Instance.ButtonAudioSource.PlayOneShot(audioClip, 1f);
            }
        }
    }

    private void OnClick()
    {
        //Debug.Log("OnClick");
        if (isPlayClickSound)
        {
            PlayClickSound();
        }
        if (eventTypes != null)
        {
            foreach (var eventType in eventTypes)
            {
                Game.EventManager.Send(eventType);
            }
        }

        if (isPunchScaleAfterClick)
        {
            transform.localScale = Vector3.one;
            transform.DOPunchScale(punch, punchDuration, 1, 0.01f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        if (isPunchScaleAfterClick)
        {
            transform.localScale = Vector3.one;
        }
        if (isScaleOnClick && GetComponent<Button>().interactable)
        {
            transform.localScale = Vector3.one * scale;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp");
        if (isScaleOnClick && GetComponent<Button>().interactable)
        {
            transform.localScale = Vector3.one;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    public Image img;
    public Sprite onSprite;
    public Sprite offSprite;

    private void Start()
    {
        UpdateSoundButton();
        GetComponent<Button>().onClick.AddListener(ToggleSound);
    }

    private void OnEnable()
    {
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        if (AudioListener.volume == 1f)
        {
            img.sprite = onSprite;
        }
        else if (AudioListener.volume == 0f)
        {
            img.sprite = offSprite;
        }
    }

    public void ToggleSound()
    {
        if (AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt(PrefsName.SoundOff, 0);
        }
        else if (AudioListener.volume == 1f)
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt(PrefsName.SoundOff, 1);
        }
        UpdateSoundButton();
        AudioManager.Instance.ButtonAudioSource.PlayOneShot(AudioManager.Instance.svs_click, 1f);
    }
}
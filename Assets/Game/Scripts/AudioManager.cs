using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ButtonAudioSource;
    public AudioSource GameAudioSource;

    public AudioClip svs_again;
    public AudioClip svs_break;
    public AudioClip svs_click;
    public AudioClip svs_dead;
    public AudioClip svs_eat;
    public AudioClip svs_select;
    public AudioClip svs_selectnone;
    public AudioClip svs_touch;
    public AudioClip svs_unstoppable;

    private void Awake()
    {
        Debug.Log("[AudioManager] Awake");
        Instance = this;
    }

    private void Start()
    {
        // AudioEffectSource.PlayOneShot(ReliveSound2, 1f);
    }
}
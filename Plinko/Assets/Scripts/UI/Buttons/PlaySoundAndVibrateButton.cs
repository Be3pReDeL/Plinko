using UnityEngine;
using CandyCoded.HapticFeedback;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundAndVibrateButton : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void Vibrate(){
        if(PlayerPrefs.GetInt("Vibrate", 0) == 1)
            HapticFeedback.LightFeedback();
    }
}

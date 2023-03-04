using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSoundTrack;

    [SerializeField] private GameObject _sound;

    public void StateSound()
    {
        _sound.SetActive(!_sound.activeSelf);

        if(_sound.activeSelf)
            _audioSoundTrack.enabled = true;
            
        if(!_sound.activeSelf)
            _audioSoundTrack.enabled = false;

    }
}

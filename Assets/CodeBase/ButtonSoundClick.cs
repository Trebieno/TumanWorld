using UnityEngine;
using Feeling;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class ButtonSoundClick : MonoBehaviour
{
    private AudioSource _hoverFx;
    private AudioSource _clickFx;
    private EventTrigger _eventTrigger;
    

    private void Start()
    {
        _hoverFx = gameObject.AddComponent<AudioSource>();
        _clickFx = gameObject.AddComponent<AudioSource>();

        _hoverFx.playOnAwake = false;
        _clickFx.playOnAwake = false;

        _hoverFx.volume = Buttons.Instance.Volume;
        _clickFx.volume = Buttons.Instance.Volume;

        _hoverFx.pitch = Buttons.Instance.Pitch;
        _clickFx.pitch = Buttons.Instance.Pitch;

        _eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerEnter;
        entry1.callback.AddListener((p) => {HoverSound();});
        _eventTrigger.triggers.Add(entry1);
        
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerClick;
        entry2.callback.AddListener((p) => {ClickSound();});
        _eventTrigger.triggers.Add(entry2);
        

        _hoverFx.clip = AudioEffects.Instance.AudioHoverFx;
        _clickFx.clip = AudioEffects.Instance.AudioClickFx;

        AudioMixer audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Effect");
        _hoverFx.outputAudioMixerGroup = audioMixGroup[0];
        _clickFx.outputAudioMixerGroup = audioMixGroup[0];
    }

    public void HoverSound()
    {
        _hoverFx.Play();
    }

    public void ClickSound()
    {
        _clickFx.Play();
    }
}

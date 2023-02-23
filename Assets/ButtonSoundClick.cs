using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundClick : MonoBehaviour
{

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void Hoversound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}

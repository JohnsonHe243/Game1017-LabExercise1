using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    void Start()
    {  
        SoundManager.Instance.AddSound("Boom", Resources.Load<AudioClip>("boom"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Death", Resources.Load<AudioClip>("death"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Jump", Resources.Load<AudioClip>("jump"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Laser", Resources.Load<AudioClip>("laser"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("MASK", Resources.Load<AudioClip>("MASK"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("TCats", Resources.Load<AudioClip>("Thundercats"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("Turtles", Resources.Load<AudioClip>("Turtles"), SoundManager.SoundType.SOUND_MUSIC);
    }

    public void PlaySFX(string soundKey)
    {
        SoundManager.Instance.PlaySound(soundKey);
    }

    public void PlayMusic(string soundKey)
    {
        SoundManager.Instance.PlayMusic(soundKey);
    }
}

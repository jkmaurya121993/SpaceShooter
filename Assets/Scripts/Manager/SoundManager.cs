using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SERIALIZE FIELDS

    [SerializeField] AudioClip buttonAudio;

    [SerializeField] AudioClip playerShootAudio;

    [SerializeField] AudioClip explosionAudio;

    [SerializeField] AudioSource audio_source;
    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this); 
    }
    public void PlayAudio(AUDIOTYPE type, bool isLoop = false, float volume = 1)
    {
        audio_source.mute = false;
        switch (type)
        {
            case AUDIOTYPE.BUTTON:
                audio_source.clip = buttonAudio;
                break;

            case AUDIOTYPE.PLAYERSHOT:
                audio_source.clip = playerShootAudio;
                break;

            case AUDIOTYPE.EXPLOSION:
                audio_source.clip = explosionAudio;
                break;
        }

        audio_source.volume = volume;
        audio_source.loop = isLoop;
        audio_source.Play();
    }
}

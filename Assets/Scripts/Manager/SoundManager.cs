using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SERIALIZE FIELDS

    [SerializeField] AudioClip buttonAudio;

    [SerializeField] AudioClip playerShootAudio;

    [SerializeField] AudioClip explosionAudio;

    [SerializeField] AudioSource source;
    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this); 
    }
    public void PlayAudio(AUDIOTYPE type, bool isLoop = false, float volume = 1)
    {
        //   source.mute = isAudioOff;
        source.mute = false;
        switch (type)
        {
            case AUDIOTYPE.BUTTON:
                source.clip = buttonAudio;
                break;

            case AUDIOTYPE.PLAYERSHOT:
                source.clip = playerShootAudio;
                break;

            case AUDIOTYPE.EXPLOSION:
                source.clip = explosionAudio;
                break;
        }

        source.volume = volume;
        source.loop = isLoop;
        source.Play();
    }
}

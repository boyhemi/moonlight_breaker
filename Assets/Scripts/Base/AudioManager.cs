using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum bgmClip { PLAYING, GAME_OVER }
    public enum sfxlips { DRAGGED_BLOCK, DESTROYED_BLOCK }

    public AudioSource bgmAudio;
    public AudioSource sfxAudio;

    private AudioClip currentBgm;
    private AudioClip currentSfx;

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    public static AudioManager init;


    void Awake()
    {
        if (!init)
        {
            init = this;
        }
    }

    public void PlayBGMAudio(int clip)
    {
        if (bgmAudio.isPlaying)
        {
            bgmAudio.Stop();
        }

        currentBgm = bgmClips[clip];

        bgmAudio.clip = currentBgm;

        bgmAudio.Play();

    }

    public void PlaySFXAudio(int clip)
    {
        if (sfxAudio.isPlaying)
        {
            sfxAudio.Stop();
        }

        currentSfx = sfxClips[clip];

        sfxAudio.clip = currentSfx;

        sfxAudio.Play();
    }

    public void MuteBGM(bool isMute)
    {
        if (isMute)
            bgmAudio.mute = true;
        else
            bgmAudio.mute = false;
    }

    public void MuteSFX(bool isMute)
    {
        if (isMute)
            sfxAudio.mute = true;
        else
            sfxAudio.mute = false;
    }

}

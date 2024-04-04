using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip hitEffect;
    public AudioClip jumpEffect;
    public AudioClip collectEffect;
    public AudioClip winEffect;

    private Slider volumeSlider;
    private float musicPlaybackTime;

    private bool effectsOff = false;

    private void Start()
    {
        musicSource.clip = background;
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        musicSource.volume = volume;
        if (GameObject.FindGameObjectWithTag("Slider"))
        {
            volumeSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
            volumeSlider.value = volume;
        }
        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            StartNewLevel();
        }
        else if (PlayerPrefs.HasKey("MusicPlaybackTime"))
        {
            musicPlaybackTime = PlayerPrefs.GetFloat("MusicPlaybackTime");
            musicSource.time = musicPlaybackTime;
        }
        
        musicSource.Play();
    }
    private void OnEnable()
    {
        Player.OnPlayerDamaged += PlayHitEffect;
        Player.OnWin += PlayWinEffect;
        PlayerMovementSystem.OnPlayerJump += PlayJumpEffect;
        Nut.OnNutDestroyed += PlayCollectItemEffect;
    }

    private void OnDisable()
    {
        Player.OnPlayerDamaged -= PlayHitEffect;
        Player.OnWin -= PlayWinEffect;
        PlayerMovementSystem.OnPlayerJump -= PlayJumpEffect;
        Nut.OnNutDestroyed -= PlayCollectItemEffect;
    }

    private void Update()
    {
        musicPlaybackTime = musicSource.time;
    }
    private void PlayHitEffect()
    {
        SFXSource.clip = hitEffect;
        if (!effectsOff)
        {
            SFXSource.Play();
        }
    }

    private void PlayWinEffect()
    {
        SFXSource.clip = winEffect;
        if (!effectsOff) 
        {
            SFXSource.Play();
        }
    }

    private void PlayJumpEffect()
    {
        SFXSource.clip = jumpEffect;
        if (!effectsOff)
        {
            SFXSource.Play();
        }
    }

    private void PlayCollectItemEffect()
    {
        SFXSource.clip = collectEffect;
        if (!effectsOff)
        {
            SFXSource.Play();
        }
    }

    public void onMusic()
    {
        if (!musicSource.isPlaying)
        {
            float volume = PlayerPrefs.GetFloat("volume", 0.5f);
            musicSource.volume = volume;
            volumeSlider.value = volume;
            if (effectsOff)
            {
                musicSource.time = musicPlaybackTime;
            }
            musicSource.Play();
            effectsOff = false;
        }
    }
    public void offMusic()
    {
        musicSource.Pause();
        float volumeBefore = PlayerPrefs.GetFloat("volume", 0.5f);
        volumeSlider.value = 0f;
        PlayerPrefs.SetFloat("volume", volumeBefore);
        effectsOff = true;

        musicPlaybackTime = musicSource.time;
        PlayerPrefs.SetFloat("MusicPlaybackTime", musicPlaybackTime);
    }

    public void ChangeVolume()
    {
        
        float volume = volumeSlider.value;
        
        if (volume == 0f)
        {
            offMusic();
        }
        else
        {
            musicSource.volume = volume;
            SFXSource.volume = volume;
            PlayerPrefs.SetFloat("volume", volume);
            onMusic();
        }
    }

    public void StartNewLevel()
    {
        musicPlaybackTime = 0f;
        PlayerPrefs.SetFloat("MusicPlaybackTime", musicPlaybackTime);
    }



}

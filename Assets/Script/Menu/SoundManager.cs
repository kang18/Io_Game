using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound_BGM;
    public AudioSource sound_Button;
    public Slider musicVolumeSlider; // 배경 음악 볼륨을 조절하는 슬라이더
    public Slider effectVolumeSlider; // 버튼 효과음 볼륨을 조절하는 슬라이더
    private float originalBGMVolume; // 원래 배경 음악 볼륨 값을 저장할 변수
    private float originalButtonVolume; // 원래 버튼 효과음 볼륨 값을 저장할 변수

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume") && PlayerPrefs.HasKey("ButtonVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            sound_BGM.volume = bgmVolume;
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume");
            sound_Button.volume = buttonVolume;
        }


        originalBGMVolume = sound_BGM.volume;
        originalButtonVolume = sound_Button.volume;
        musicVolumeSlider.value = originalBGMVolume;
        effectVolumeSlider.value = originalButtonVolume;
    }


    public void SetBgmVolume(float volume)
    {
        sound_BGM.volume = volume;
        musicVolumeSlider.value = volume;
    }

    public void SetEffectVolume(float volume)
    {
        sound_Button.volume = volume;
        effectVolumeSlider.value = volume;
    }


    public void OnSoundButton()
    {
        sound_Button.Play();
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", sound_BGM.volume);
        PlayerPrefs.SetFloat("ButtonVolume", sound_Button.volume);
    }

    public void LoadSoundSettings()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            sound_BGM.volume = bgmVolume;
            musicVolumeSlider.value = bgmVolume;
        }

        if (PlayerPrefs.HasKey("ButtonVolume"))
        {
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume");
            sound_Button.volume = buttonVolume;
            effectVolumeSlider.value = buttonVolume;
        }
    }
}
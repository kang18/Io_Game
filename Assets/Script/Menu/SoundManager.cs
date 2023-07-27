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
    public Toggle muteToggle; // 음소거 버튼
    private float originalBGMVolume; // 원래 배경 음악 볼륨 값을 저장할 변수
    private float originalButtonVolume; // 원래 버튼 효과음 볼륨 값을 저장할 변수
    public bool isMute; // 음소거 상태인지 아닌지

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume") && PlayerPrefs.HasKey("ButtonVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            sound_BGM.volume = bgmVolume;
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume");
            sound_Button.volume = buttonVolume;
        }

        if (PlayerPrefs.HasKey("BGMVolume_original") && PlayerPrefs.HasKey("ButtonVolume_original"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume_original");
            sound_BGM.volume = bgmVolume;
            float buttonVolume = PlayerPrefs.GetFloat("ButtonVolume_original");
            sound_Button.volume = buttonVolume;
        }

        muteToggle.isOn = isMute;
        originalBGMVolume = sound_BGM.volume;
        originalButtonVolume = sound_Button.volume;
        musicVolumeSlider.value = originalBGMVolume;    // 씬 전환 시 슬라이더를 현재 volume 값에 맞춤
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

    public void Mute()
    {
        if(!isMute)
        {
            PlayerPrefs.SetFloat("BGMVolume_original", sound_BGM.volume);
            PlayerPrefs.SetFloat("ButtonVolume_original", sound_Button.volume);

            sound_BGM.volume = 0f;  
            sound_Button.volume = 0f;
            musicVolumeSlider.value = 0f;
            effectVolumeSlider.value = 0f;

            isMute = true;
        }
        else
        {
            sound_BGM.volume = PlayerPrefs.GetFloat("BGMVolume_original");
            sound_Button.volume = PlayerPrefs.GetFloat("ButtonVolume_original");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume_original");
            effectVolumeSlider.value = PlayerPrefs.GetFloat("ButtonVolume_original");

            isMute = false;
        }
        
    }


    public void OnSoundButton() // 버튼 클릭시 사운드 재생
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
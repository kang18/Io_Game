using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Menu_GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject soundPanel;

    public void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(soundPanel.activeSelf)
            {
                soundPanel.SetActive(false);
            }
        }
    }



    public void SceneToPlay() // Main Menu의 게임 시작 버튼과 연결되어 있음
    {
        soundManager.SaveSoundSettings();
        SceneManager.LoadScene("Play");
    }

    public void ToggleUIPanel()
    {
        soundPanel.SetActive(!soundPanel.activeSelf);
    }

    public void QuitGame() // 게임 종료 버튼 클릭(게임 종료)
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Menu_GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject mainPanel;
    public GameObject soundPanel;
    public GameObject stagePanel;


    public void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            if(soundPanel.activeSelf)
            {
                soundPanel.SetActive(false);
            }

            if (stagePanel.activeSelf)
            {
                stagePanel.SetActive(false);
                mainPanel.SetActive(true);
            }
        }
    }




    public void SceneToPlay() // Main Menu의 게임 시작 버튼과 연결되어 있음
    {
        soundManager.SaveSoundSettings();
        SceneManager.LoadScene("Play");
    }

    public void GameStartButton()
    {
        if (!soundPanel.activeSelf)
        {
            mainPanel.SetActive(false);
            stagePanel.SetActive(true);
        }
    }

    public void ToggleUIPanel()
    {
        if(!soundPanel.activeSelf)
        {
            soundPanel.SetActive(!soundPanel.activeSelf);
        }

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


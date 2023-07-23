using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Menu_GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject soundPanel;

    public bool uiActive;

    public void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            
        }


    }

    public void KeyInput()
    {
  
    }







    public void SceneToPlay()
    {
        soundManager.SaveSoundSettings();
        SceneManager.LoadScene("Play");
    }

    public void ToggleUIPanel()
    {
        soundPanel.SetActive(!soundPanel.activeSelf);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}


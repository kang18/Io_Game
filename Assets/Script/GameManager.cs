using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // GameManager의 인스턴스를 저장할 변수
    private bool isPaused = false;
    public bool IsPaused { get => isPaused; } // isPaused를 외부에서 읽기만 가능하도록 getter만 선언


    public PlayerBehavior player;
    public Image hpBar;
    public Image []gemBar;
    public GameObject playOption;
    public GameObject soundPanel;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        // #. 플레이어 상태 정보 업데이트
        hpBar.fillAmount = (float)player.hp * 0.01f;
        







        // gemPoint 만들고 있었음













        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 일시정지
            playOption.SetActive(true);
        }
        else
        {
            if(soundPanel.activeSelf)
            {
                isPaused = true;
                soundPanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f; // 게임 진행
                playOption.SetActive(false);
            }
        }
    }

    public void SceneToPlay() // Option 창에 "다시하기" 버튼이랑 연결되어 있음
    {
        TogglePause();
        SceneManager.LoadScene("Play");
           
    }

    public void SceneToMainTitle() // Option 창에 "메인화면" 버튼이랑 연결되어 있음
    {
        TogglePause();
        SceneManager.LoadScene("Menu");
    }

    public void ToggleUIPanel()
    {
        soundPanel.SetActive(!soundPanel.activeSelf);
    }
}

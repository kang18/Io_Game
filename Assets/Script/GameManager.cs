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
    public GameObject []gemBar;
    public Image[] skillShadow;
    public GameObject playOption;
    public GameObject soundPanel;
    public GameObject gameoverPanel;

    public Boss1 boss;
    public GameObject bossPattern;
    public GameObject[] bossEye;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);


        bossPattern.SetActive(false);
    }


    private void Update()
    {
        // #. 플레이어 상태 정보 업데이트
        hpBar.fillAmount = (float)player.hp * 0.01f;
        if(player.gemPoint >= 5)
        {
            skillShadow[0].fillAmount = 0f;
            skillShadow[1].fillAmount = 0f;
            skillShadow[2].fillAmount = 0f;
        }
        else if(player.gemPoint >= 3)
        {
            skillShadow[0].fillAmount = 0f;
            skillShadow[1].fillAmount = 0f;
            skillShadow[2].fillAmount = 1f;
        }
        else if (player.gemPoint >= 1)
        {
            skillShadow[0].fillAmount = 0f;
            skillShadow[1].fillAmount = 1f;
            skillShadow[2].fillAmount = 1f;
        }
        else if (player.gemPoint == 0)
        {
            skillShadow[0].fillAmount = 1f;
            skillShadow[1].fillAmount = 1f;
            skillShadow[2].fillAmount = 1f;
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }

        if (GameObject.Find("Boss(Clone)") != null)
        {
            GameObject bossObject = GameObject.Find("Boss(Clone)");
            boss = bossObject.GetComponent<Boss1>();
            if (boss != null)
            {
                boss.medusaEye[0] = bossEye[0];
                boss.medusaEye[1] = bossEye[1];
                bossPattern.SetActive(true);
            }
        }
    }

    public void GemUpdate(int count)
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < count)
            {
                gemBar[i].SetActive(true);
            }
            else
            {
                gemBar[i].SetActive(false);
            }
           
        }
    }


    public void DieEvent()
    {
        TogglePauseSimple();
        gameoverPanel.SetActive(true); // 게임 오버 패널 활성화
    }

    private void TogglePauseSimple()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 일시정지
        }
        else
        {
            Time.timeScale = 1f; // 게임 진행
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
        if (gameoverPanel.activeSelf)
        {
            gameoverPanel.SetActive(false);
        }

        TogglePause();
        SceneManager.LoadScene("Play");
    }

    public void SceneToMainTitle() // Option 창에 "메인화면" 버튼이랑 연결되어 있음
    {
        if(gameoverPanel.activeSelf)
        {
            gameoverPanel.SetActive(false);
        }

        TogglePause();
        SceneManager.LoadScene("Menu");
    }

    public void ToggleUIPanel()
    {
        soundPanel.SetActive(!soundPanel.activeSelf);
    }
}

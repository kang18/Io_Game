using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public int waveCnt;

    private float spawnTimer = 0f;
    public float nextSpawnDelay;

    public List<SpawnTXT> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public bool readList; // List를 읽고 있는지 판단

    public int catchMonsters; // 잡은 몬스터 수
    public bool waveClear; // 해당 Wave를 클리어 했는지 판단

    public GameObject[] Monsters; // 몬스터를 담을 배열
    public Transform[] spawnPositionUpDown; // 위층 아래층


    public TMP_Text tmp;
    public TMP_Text wave;

    private void Awake()
    {
        spawnList = new List<SpawnTXT>();
        ReadSpaenTXT();
    }

    private void Update()
    {
        // #. UI 띄우기
        UiControl();

        // 나중에 최종 스테이지 정해지면 더 이상 다음 텍스트 파일 못 읽도록 수정해야 함
        if (GameManager.Instance.IsPaused) // GameManager의 IsPaused를 체크하여 일시정지 중인지 확인
        {
            spawnTimer = 0f; // 일시정지 중에는 스폰 타이머를 리셋하여 스폰이 멈춤
            return; // 일시정지 중이면 몬스터 스폰을 진행하지 않음
        }

        if (spawnEnd && catchMonsters == spawnList.Count)
        {
            catchMonsters = 0;
            spawnEnd = false;
            readList = true;
            waveCnt++;
            Invoke("ReadSpaenTXT", 3f);
        }

        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= nextSpawnDelay && !spawnEnd && !readList)
        {
            SpawnMonster();
            spawnTimer = 0f;
        }
    }




    private void UiControl()
    {
        tmp.text = "Wave" + waveCnt.ToString();
        wave.text = "Max Wave : " + waveCnt.ToString();
    }



    void ReadSpaenTXT()
    {
        // #1.변수 초기화
        spawnList.Clear(); // 텍스트 파일을 읽기 전 리스트를 초기화
        spawnIndex = 0; // 첫번째부터 읽어야 하니까


        // #2.변수 초기화
        TextAsset textFile = Resources.Load("Wave" + waveCnt) as TextAsset; // as TextAsset를 추가해줌으로써 텍스트 파일이 맞는지 아닌지 한 번 더 검증
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); // 텍스트 데이터를 한 줄씩 반환하는 함수
            Debug.Log(line);

            if (line == null)
                break;

            // #3.리스폰 데이터 저장
            SpawnTXT spawnData = new SpawnTXT();
            spawnData.delay = float.Parse(line.Split(',')[0]);  // float.Parse(@@) 괄호 안의 내용 파싱 
            spawnData.type = line.Split(',')[1];                // Split 특정 문자열을 기준으로 끊어서 텍스트 파일 읽기
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // #.텍스트 파일 닫기
        stringReader.Close();

        // #.첫번째 스폰 딜레이 적용
        nextSpawnDelay = spawnList[0].delay;

        readList = false;
    }


    private void SpawnMonster()
    {
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "Basic":
                enemyIndex = 0;
                break;

            case "Flying":
                enemyIndex = 1;
                break;

            case "Jump":
                enemyIndex = 2;
                break;

            case "Stone":
                enemyIndex = 3;
                break;

            case "Boss":
                enemyIndex = 4;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        GameObject monster = Instantiate(Monsters[enemyIndex], spawnPositionUpDown[enemyPoint].position, Quaternion.identity);

        // #.리스폰 인덱스 증가
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // #.다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

}

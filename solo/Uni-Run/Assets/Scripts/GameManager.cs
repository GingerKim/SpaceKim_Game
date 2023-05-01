using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    public Text timeText; // UI에 출력할 타이머 텍스트
    public GameObject gameoverUI;

    private float startTime = 31f; // 게임 시작 시간(3분)
    private float currentTime; // 현재 시간

    public AudioSource bgm; // BGM을 참조하기 위한 변수

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        // 3분에서 1초씩 감소하는 타이머 기능
        if (!isGameover)
        {
            float timeLeft = Mathf.Max(0f, 31f - Time.timeSinceLevelLoad);
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);

            // UI에 타이머 정보 출력
            timeText.text = string.Format("Time {0:00}:{1:00}", minutes, seconds);


            // 타이머가 0초가 되면 게임 오버 처리
            if (timeLeft <= 0f)
            {
                OnPlayerDead();
            }
        }

        if (isGameover) // 게임 오버 상태이면
        {
            bgm.Stop(); // BGM을 중지합니다.
        }

        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

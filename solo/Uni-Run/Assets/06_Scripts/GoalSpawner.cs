using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    public GameObject goalPrefab; // "goal" 프리팹을 할당할 변수
    public float spawnInterval = 3f; // 생성 간격
    public float deleteInterval = 1f; // 삭제 간격
    public int maxGoalCount = 1; // 최대 Goal 개수
    private int currentGoalCount = 0; // 현재 Goal 개수

    private GameManager gameManager; // GameManager 스크립트를 참조할 변수

    private void Start()
    {
        // GameManager 스크립트를 참조함
        gameManager = GameManager.instance;

        // 10초 뒤에 SpawnGoal() 메서드 호출 후, 일정 시간 간격으로 SpawnGoal() 메서드와 DeleteGoal() 메서드를 호출함
        Invoke("SpawnGoal", 5f);
        InvokeRepeating("SpawnGoal", 6f, spawnInterval);
        InvokeRepeating("DeleteGoal", 7f + deleteInterval, deleteInterval);
    }

    private void SpawnGoal()
    {
        // 게임 오버 상태일 때는 Goal을 생성하지 않음
        if (gameManager.isGameover) return;

        if (currentGoalCount >= maxGoalCount) return; // 현재 Goal 개수가 최대 Goal 개수 이상이면 더 이상 생성하지 않음

        // 화면의 오른쪽 끝에 위치한 좌표 계산
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 spawnPos = new Vector3(mainCamera.transform.position.x + cameraWidth / 2f, 0f, 0f);

        // platform 태그를 가진 오브젝트와 겹치지 않도록 y축을 0.2 정도 더한 위치에서 생성
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Platform"))
            {
                spawnPos += new Vector3(0f, +0.2f, 0f);
                break;
            }
        }

        Instantiate(goalPrefab, spawnPos, Quaternion.identity);
        currentGoalCount++;
    }

    private void DeleteGoal()
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        if (goals.Length > 0)
        {
            GameObject goalToDelete = goals[0];
            Destroy(goalToDelete);
            currentGoalCount--;
        }
    }
}
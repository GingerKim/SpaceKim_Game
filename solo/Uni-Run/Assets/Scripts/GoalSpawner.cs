using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    public GameObject goalPrefab; // "goal" �������� �Ҵ��� ����
    public float spawnInterval = 3f; // ���� ����
    public float deleteInterval = 1f; // ���� ����
    public int maxGoalCount = 1; // �ִ� Goal ����
    private int currentGoalCount = 0; // ���� Goal ����

    private GameManager gameManager; // GameManager ��ũ��Ʈ�� ������ ����

    private void Start()
    {
        // GameManager ��ũ��Ʈ�� ������
        gameManager = GameManager.instance;

        // 10�� �ڿ� SpawnGoal() �޼��� ȣ�� ��, ���� �ð� �������� SpawnGoal() �޼���� DeleteGoal() �޼��带 ȣ����
        Invoke("SpawnGoal", 5f);
        InvokeRepeating("SpawnGoal", 6f, spawnInterval);
        InvokeRepeating("DeleteGoal", 7f + deleteInterval, deleteInterval);
    }

    private void SpawnGoal()
    {
        // ���� ���� ������ ���� Goal�� �������� ����
        if (gameManager.isGameover) return;

        if (currentGoalCount >= maxGoalCount) return; // ���� Goal ������ �ִ� Goal ���� �̻��̸� �� �̻� �������� ����

        // ȭ���� ������ ���� ��ġ�� ��ǥ ���
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 spawnPos = new Vector3(mainCamera.transform.position.x + cameraWidth / 2f, 0f, 0f);

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
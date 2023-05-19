using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // 이동 속도
    private float timeElapsed = 0f;

    private void Update()
    {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        if (!GameManager.instance.isGameover) // 게임 오버가 아니라면
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 30f)
            {
                speed = 0f; // 이동 속도를 0으로 변경하여 멈춤
            }
            else
            {
                // 초당 speed의 속도로 왼쪽으로 평행이동
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;// 이동에 사용할 리지드바디 컴포넌트
    // public Rigidbody playerRigidbody;
    // 이렇게 퍼블릭의 경우 바깥에서 예를 들면 컴포넌트에서 수정하거나 접근할 수 있으나
    // Private의 경우엔 그렇지 않음.
    public float speed = 8f; // 이동 속력

    // Start is called before the first frame update
    void Start()
    {   // 게임 오브젝트에서 rigidbody 컴포넌트를 찾아 playerRigidbody에 할당
        // GetComponent는 원하는 타입의 컴포넌트를 자신의 게임 오브젝터에서 찾아오는 메서드
        playerRigidbody = GetComponent<Rigidbody>(); // < > 꺽쇠를 사용해 가져올 타입을 받는다
    }

    // Update is called once per frame
    void Update() // 업데이트 메서드는 한 프레임에 한 번 매 프레임 마다 실행이 된다.
                  // 따라서 60fps 주사율을 가진 주사율의 경우 매초 60회 실행된다.
                  // 굉장히 짧은 간격으로 반복실행 되는 것을 구현할 수 있음
    {
        /* 
        Input.GetKey() 메서드는 키보드의 식별자를 "(KeyCode. 특정 키 )" 타입으로 입력 받음
        if (Input.GetKey(KeyCode.UpArrow) == true) // 위쪽 키
        {
            playerRigidbody.AddForce(0f, 0f, speed);
            // x y z에서 0f, 0f, speed면 z축임
            //위쪽 방향키 입력이 감지된 경우 z 방향 힘 주기
        }


        if (Input.GetKey(KeyCode.DownArrow) == true) // 아래쪽 키
        {
            playerRigidbody.AddForce(0f, 0f, -speed);
            // x y z 에서 0f 0f speed이면 speed는 z축임 -니까 z축의 -로(-z) 향함
            //아래쪽 방향키 입력이 감지된 경우 -z 방향 힘 주기
        }


        if (Input.GetKey(KeyCode.RightArrow) == true) // 왼쪽 키
        {
            playerRigidbody.AddForce(speed, 0f, 0f);
            // x y z에서 speed 0f 0f면 speed는 x 축임
            //왼쪽 방향키 입력이 감지된 경우 x 방향 힘 주기
        }


        if(Input.GetKey(KeyCode.LeftArrow) == true) // 오른쪽 키
        {
            playerRigidbody.AddForce(-speed, 0f, 0f);
            // x y z에서 -speed 0f 0f면 x축인데 -임
            // 오른쪽 방향키 입력이 감지된 경우 -x 방향 힘 주기
        }
        */
        // 아래는 수정된 코드
        // 1. 수평축과 수직축의 입력값을 감지
        // 2. 속도를 나타낼 새로운 vector3를 생성
        // 3. 리지드바디 컴포넌트의 속도를 변경


        // 수평축과 수직축의 입력값을 감지하여 저장
        // Input.GetAxis 메서드는 Input.GetKey 대신 사용하게 된다.
        // 축에 대한 입력값을 숫자로 반환하는 메서드임
        // 해당되는 축(zInput = input.GetAxis의 경우는 Z축임)의 감지된 만큼 입력을 받는다
        // 이 메서드 원형은 float Input.GetAxis(string axisName);

        // 해당하는 키는 입력 매니저를 통해 설정되어있다.
        // 기본 설정으로 추가 되어있는 Horizontal 축과 Vertical 축의 대응 입력키와 출력키는 다음과 같은데
        // 아래는 Horizontal과 Input.GetAxis("Horizontal")의 출력값으로
        // Horizontal 수평축의 경우           ← 왼쪽 방향키,  A키는 -(음)의 방향
        //                                    →오른쪽 방향키, D키는 +(양)의 방향
        // Input.GetAxis("Horizontal")의 경우 ← 왼쪽 방향키,  A키는 -1.0
        //                                    →오른쪽 방향키, D키는 +1.0

        // Vertical 수직축의 경우             ↓아래쪽 방향키, S키는 -(음)의 방향
        //                                    ↑ 윗쪽 방향키,  W키는 +(양)의 방향
        // Input.GetAxis("Vertical")의 경우   ↓아래쪽 방향키, S키는 -1.0
        //                                    ↑ 윗쪽 방향키,  W키는 +1.0
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // 실제 이동 속도를 입력값과 이동 속력을 사용해 결정
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        
        //Vector3 속도를 (xSpeed, 0, zSpeed)로 생성
        //Vector3는 x y z를 가지는 타입이다. 위치 크기 속도 방향 등을 나타날때 사용한다.
        //원형은 vector3 vector = new Vector3(x, y , z);
        Vector3 newVelocity = new Vector3 (xSpeed, 0f, zSpeed);
        //이 코드의 경우 new Vector3에 x 방향으로 xSpeed 만큼
        //                             y 방향으로 0 만큼
        //                             Z 방향으로 zSpeed 만큼 가는 속도를 표현한 코드했는데
        // 이 Vector3를 newVelocity라는 변수로 저장한 것.

        // 리지드바디의 속도에 newVelocty 할당
        playerRigidbody.velocity = newVelocity;
    }

    public void Die()
    {   // 게임 오브젝트는 Player 오브젝트임
        // 자신의 게임 오브젝트를 비활성화 한다
        // 인스펙터 창에 있는 체크박스를 활성화 비활성화 하는 것처럼 SetActive로 설정할 수 있음
        gameObject.SetActive(false);

        // 씬에 존재하는 GameManager 타입의 오브젝트를 찾아서 가져오기
        GameManager gameManager = FindObjectOfType<GameManager>();
        // 가져온 GameManager 오브젝트의 EndGame()에서도 실행
        gameManager.EndGame();
    }
}

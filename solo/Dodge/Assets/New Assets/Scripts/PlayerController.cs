using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;// �̵��� ����� ������ٵ� ������Ʈ
    // public Rigidbody playerRigidbody;
    // �̷��� �ۺ��� ��� �ٱ����� ���� ��� ������Ʈ���� �����ϰų� ������ �� ������
    // Private�� ��쿣 �׷��� ����.
    public float speed = 8f; // �̵� �ӷ�

    // Start is called before the first frame update
    void Start()
    {   // ���� ������Ʈ���� rigidbody ������Ʈ�� ã�� playerRigidbody�� �Ҵ�
        // GetComponent�� ���ϴ� Ÿ���� ������Ʈ�� �ڽ��� ���� �������Ϳ��� ã�ƿ��� �޼���
        playerRigidbody = GetComponent<Rigidbody>(); // < > ���踦 ����� ������ Ÿ���� �޴´�
    }

    // Update is called once per frame
    void Update() // ������Ʈ �޼���� �� �����ӿ� �� �� �� ������ ���� ������ �ȴ�.
                  // ���� 60fps �ֻ����� ���� �ֻ����� ��� ���� 60ȸ ����ȴ�.
                  // ������ ª�� �������� �ݺ����� �Ǵ� ���� ������ �� ����
    {
        /* 
        Input.GetKey() �޼���� Ű������ �ĺ��ڸ� "(KeyCode. Ư�� Ű )" Ÿ������ �Է� ����
        if (Input.GetKey(KeyCode.UpArrow) == true) // ���� Ű
        {
            playerRigidbody.AddForce(0f, 0f, speed);
            // x y z���� 0f, 0f, speed�� z����
            //���� ����Ű �Է��� ������ ��� z ���� �� �ֱ�
        }


        if (Input.GetKey(KeyCode.DownArrow) == true) // �Ʒ��� Ű
        {
            playerRigidbody.AddForce(0f, 0f, -speed);
            // x y z ���� 0f 0f speed�̸� speed�� z���� -�ϱ� z���� -��(-z) ����
            //�Ʒ��� ����Ű �Է��� ������ ��� -z ���� �� �ֱ�
        }


        if (Input.GetKey(KeyCode.RightArrow) == true) // ���� Ű
        {
            playerRigidbody.AddForce(speed, 0f, 0f);
            // x y z���� speed 0f 0f�� speed�� x ����
            //���� ����Ű �Է��� ������ ��� x ���� �� �ֱ�
        }


        if(Input.GetKey(KeyCode.LeftArrow) == true) // ������ Ű
        {
            playerRigidbody.AddForce(-speed, 0f, 0f);
            // x y z���� -speed 0f 0f�� x���ε� -��
            // ������ ����Ű �Է��� ������ ��� -x ���� �� �ֱ�
        }
        */
        // �Ʒ��� ������ �ڵ�
        // 1. ������� �������� �Է°��� ����
        // 2. �ӵ��� ��Ÿ�� ���ο� vector3�� ����
        // 3. ������ٵ� ������Ʈ�� �ӵ��� ����


        // ������� �������� �Է°��� �����Ͽ� ����
        // Input.GetAxis �޼���� Input.GetKey ��� ����ϰ� �ȴ�.
        // �࿡ ���� �Է°��� ���ڷ� ��ȯ�ϴ� �޼�����
        // �ش�Ǵ� ��(zInput = input.GetAxis�� ���� Z����)�� ������ ��ŭ �Է��� �޴´�
        // �� �޼��� ������ float Input.GetAxis(string axisName);

        // �ش��ϴ� Ű�� �Է� �Ŵ����� ���� �����Ǿ��ִ�.
        // �⺻ �������� �߰� �Ǿ��ִ� Horizontal ��� Vertical ���� ���� �Է�Ű�� ���Ű�� ������ ������
        // �Ʒ��� Horizontal�� Input.GetAxis("Horizontal")�� ��°�����
        // Horizontal �������� ���           �� ���� ����Ű,  AŰ�� -(��)�� ����
        //                                    ������� ����Ű, DŰ�� +(��)�� ����
        // Input.GetAxis("Horizontal")�� ��� �� ���� ����Ű,  AŰ�� -1.0
        //                                    ������� ����Ű, DŰ�� +1.0

        // Vertical �������� ���             ��Ʒ��� ����Ű, SŰ�� -(��)�� ����
        //                                    �� ���� ����Ű,  WŰ�� +(��)�� ����
        // Input.GetAxis("Vertical")�� ���   ��Ʒ��� ����Ű, SŰ�� -1.0
        //                                    �� ���� ����Ű,  WŰ�� +1.0
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // ���� �̵� �ӵ��� �Է°��� �̵� �ӷ��� ����� ����
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        
        //Vector3 �ӵ��� (xSpeed, 0, zSpeed)�� ����
        //Vector3�� x y z�� ������ Ÿ���̴�. ��ġ ũ�� �ӵ� ���� ���� ��Ÿ���� ����Ѵ�.
        //������ vector3 vector = new Vector3(x, y , z);
        Vector3 newVelocity = new Vector3 (xSpeed, 0f, zSpeed);
        //�� �ڵ��� ��� new Vector3�� x �������� xSpeed ��ŭ
        //                             y �������� 0 ��ŭ
        //                             Z �������� zSpeed ��ŭ ���� �ӵ��� ǥ���� �ڵ��ߴµ�
        // �� Vector3�� newVelocity��� ������ ������ ��.

        // ������ٵ��� �ӵ��� newVelocty �Ҵ�
        playerRigidbody.velocity = newVelocity;
    }

    public void Die()
    {   // ���� ������Ʈ�� Player ������Ʈ��
        // �ڽ��� ���� ������Ʈ�� ��Ȱ��ȭ �Ѵ�
        // �ν����� â�� �ִ� üũ�ڽ��� Ȱ��ȭ ��Ȱ��ȭ �ϴ� ��ó�� SetActive�� ������ �� ����
        gameObject.SetActive(false);
    }
}

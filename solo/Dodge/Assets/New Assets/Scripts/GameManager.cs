using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ���� ���̺귯��
using UnityEngine.SceneManagement; // �� ���� ���� ���̺귯��

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText; // ���ӿ����� Ȱ��ȭ�� �ؽ�Ʈ ���� ������Ʈ
    public Text timeText; //
                          //   ���� �ð��� ǥ���� �ؽ�Ʈ ������Ʈ 
    public Text recordText; // �ְ� ����� ǥ���� �ؽ�Ʈ ������Ʈ

    private float surviveTime; // ���� ����
    private bool isGameover;     // ���ӿ��� ����     

    // Start is called before the first frame update
    void Start()
    {
        //�����ð��� ���ӿ��� ���� �ʱ�ȭ
        surviveTime = 0;
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ӿ����� �ƴ� ����
        if (!isGameover)
        {
            // ���� �ð� ����
            surviveTime += Time.deltaTime;
            // ������ �����ð��� timeText �ؽ�Ʈ ������Ʈ�� �̿��� ǥ��
            timeText.text = "Time: " + (int)surviveTime;
        }
        else
        {
            // ���ӿ��� ���¿��� RŰ�� ���� ���
            if(Input.GetKeyDown(KeyCode.R))
            {
                //SimpleScene ���� �ε�
                SceneManager.LoadScene("NewScene");
            }
        }
            
    }

    //���� ������ ���ӿ��� ���·� �����ϴ� �޼���
    public void EndGame()
    {
        // ���� ���¸� ���ӿ��� ���·� ��ȯ
        isGameover = true;
        // ���ӿ��� �ؽ�Ʈ ���� ������Ʈ�� Ȱ��ȭ
        gameoverText.SetActive(true);

        //BestTime Ű�� ����� ���������� �ְ� ��� ��������
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        //���������� �ְ� ��Ϻ��� ���� ���� �ð��� �� ũ�ٸ�
        if(surviveTime > bestTime)
        {   
            // �ְ� ��� ���� ���� ���� �ð� ������ ����
            bestTime = surviveTime;
            // ����� �ְ� ����� BestTime Ű�� ����
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        //�ְ� ����� recordText �ؽ�Ʈ ������Ʈ�� �̿��� ǥ��
        recordText.text = "Best Time: " + (int)bestTime;
    }
}
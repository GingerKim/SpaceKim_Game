using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // �÷��̾��� �⺻ �Ӽ�
    public float _speed; // �÷��̾��� �̵� �ӵ�
    public float _hp; // �÷��̾��� ü��

    public TextMesh _HpVal; // �÷��̾� ü�� �� �ؽ�Ʈ
    public GameObject _hpBar; // �÷��̾� ü�¹� �̹���
    public Animator _rabbit; // �÷��̾��� �ִϸ��̼��� ����ϴ� Animator

    public bool _gameWin; // �¸� ���θ� üũ�ϴ� bool
    public bool _playerLive = true; // �÷��̾� ���� ���θ� üũ�ϴ� bool

    public GameObject _uiResult; // ��� UI ������Ʈ
    public Text _resultText; // ��� UI�� �¸� �Ǵ� �й� �ؽ�Ʈ
    public BoxCollider _attackChkCol; // �÷��̾� ���� �� Ȱ��ȭ/��Ȱ��ȭ�� �ڽ� �ݶ��̴�

    private float _damTimer; // ������ ���� üũ�� Ÿ�̸�

    public GameObject _DamEffect; // ������ ���¿��� ����� ����Ʈ
    public GameObject _DamText; // ������ ���¿��� ǥ�õ� ������ �� �ؽ�Ʈ

    private float _timerForAttack;
    private float _timerForAttackSnd;
    private bool _attackChkbool; // ���� ���� üũ�� bool

    // Use this for initialization
    void Start()
    {
        // ����� �ҽ��� �ִ� ��� ���
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }

        // _rabbit�� �����ϴ� ���
        if (_rabbit != null)
        {
            _rabbit.speed = 2.0f;
        }
        // _rabbit�� ���� ���, �ڽ� ������Ʈ���� Animator ������Ʈ�� ã�Ƽ� speed�� ����
        else
        {
            _rabbit = gameObject.GetComponentInChildren<Animator>();
            _rabbit.speed = 2.0f;
        }

        // _uiResult�� �����ϴ� ���, _resultText�� ã�Ƽ� ������
        if (_uiResult != null)
        {
            _resultText = _uiResult.transform.Find("3_Result_Text").gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerLive) // �÷��̾ ����ִ� ���ȸ� ����
        {
            if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)) // Ű������ ���� �Ǵ� ���� ����Ű�� ������
            {
                float _verticalPos = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
                float _horizonPos = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;

                transform.position += new Vector3(-1 * _horizonPos, 0, -1 * _verticalPos); // �÷��̾ �̵���Ű��
                transform.forward = (new Vector3(-1 * _horizonPos, 0, -1 * _verticalPos)); // �÷��̾ �ٶ󺸴� ������ �̵� �������� ����
                if (_rabbit != null) _rabbit.SetBool("runChk", true); // �䳢 ĳ���� �ִϸ��̼ǿ��� �ٱ� �ִϸ��̼��� ����
            }
            else
            {
                if (_rabbit != null) _rabbit.SetBool("runChk", false); // �̵����� ������ �ٱ� �ִϸ��̼��� ����
            }

            if (_rabbit != null) // �䳢 ĳ���Ͱ� �����ϴ� ���
            {
                if (Input.GetButtonDown("Fire1") && !_rabbit.GetCurrentAnimatorStateInfo(0).IsName("1_attack")) // ���� ���콺 ��ư�� ������, ���� ���� ���� �ִϸ��̼��� ���� �ִϸ��̼����� �ƴ��� Ȯ��
                {
                    _rabbit.SetBool("attackChk", true); // ���� �ִϸ��̼��� ����
                    if (_attackChkCol != null)
                    {
                        _attackChkCol.enabled = true; // ���� ���� �ö��̴��� Ȱ��ȭ
                    }
                    _attackChkbool = true;
                }
                else
                {
                    if (_attackChkCol != null) _attackChkCol.enabled = false; // ���� ���� �ö��̴��� ��Ȱ��ȭ
                }

                if (_rabbit.GetBool("damageChk")) // ���� �������� ���� �������� Ȯ��
                {
                    if (_rabbit.GetCurrentAnimatorStateInfo(0).IsName("3_damage")) // ������ �ִϸ��̼��� ���� ������ Ȯ��
                    {
                        if (_rabbit.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.0f) _rabbit.SetBool("damageChk", false); // ������ �ִϸ��̼��� ������ ������ ���¸� ����
                    }
                }
            }
        }
    }


    void Damaged(float _dam)
    {
        // _hp �������� ������ ���� ����.
        _hp -= _dam;

        // �䳢 �ִϸ��̼��� ���°� "3_damage"�� �ƴ� ���, "damageChk" bool ���� true�� �����Ѵ�.
        if (!_rabbit.GetCurrentAnimatorStateInfo(0).IsName("3_damage"))
        {
            _rabbit.SetBool("damageChk", true);
        }

        // _DamEffect ���� ������Ʈ�� �����Ѵٸ�, �ش� ���� ������Ʈ�� �����Ѵ�.
        if (_DamEffect != null)
        {
            Instantiate(_DamEffect, new Vector3(transform.position.x, 1.0f, transform.position.z), Quaternion.identity);
        }

        // _DamText ���� ������Ʈ�� �����Ѵٸ�, �ش� ���� ������Ʈ�� �����Ѵ�.
        if (_DamText != null)
        {
            Instantiate(_DamText, new Vector3(transform.position.x, 1.2f, transform.position.z + 0.2f), Quaternion.identity);
        }

        // _hp�� 0���� ū ���, ü�¹ٿ� ü�°��� ������Ʈ�Ѵ�.
        if (_hp > 0)
        {
            if (_hpBar != null)
            {
                _hpBar.transform.localScale = new Vector3(_hp * 0.01f, 1, 1);
            }
            if (_HpVal != null)
            {
                _HpVal.text = _hp.ToString();
            }
        }
        // _hp�� 0 ������ ���, ü�¹ٿ� ü�°��� 0���� �����ϰ�, ���� ���� ó���� �����Ѵ�.
        else if (_hp <= 0)
        {
            if (_hpBar != null)
            {
                _hpBar.transform.localScale = new Vector3(0, 1, 1);
            }
            _playerLive = false;
            if (_HpVal != null)
            {
                _HpVal.text = "0";
            }
            _gameWin = false;
            GameOver();
        }
    }

    public void GameOver()
	{
		//game over
        if (_gameWin)
        {
            if(_resultText != null) _resultText.text = "WIN";
        }
        else
        {
            if(_resultText != null)_resultText.text = "LOSE";
        }
        
        //
		Time.timeScale = 0.0f;
		if(_uiResult != null) _uiResult.SetActive(true);
		
	}

    void Regame()
    {
        Time.timeScale = 1.0f;
        //Application.LoadLevel("1_play");
        SceneManager.LoadScene("1_play");
    }
}

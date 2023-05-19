using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // 플레이어의 기본 속성
    public float _speed; // 플레이어의 이동 속도
    public float _hp; // 플레이어의 체력

    public TextMesh _HpVal; // 플레이어 체력 값 텍스트
    public GameObject _hpBar; // 플레이어 체력바 이미지
    public Animator _rabbit; // 플레이어의 애니메이션을 담당하는 Animator

    public bool _gameWin; // 승리 여부를 체크하는 bool
    public bool _playerLive = true; // 플레이어 생존 여부를 체크하는 bool

    public GameObject _uiResult; // 결과 UI 오브젝트
    public Text _resultText; // 결과 UI의 승리 또는 패배 텍스트
    public BoxCollider _attackChkCol; // 플레이어 공격 시 활성화/비활성화할 박스 콜라이더

    private float _damTimer; // 데미지 상태 체크용 타이머

    public GameObject _DamEffect; // 데미지 상태에서 재생할 이펙트
    public GameObject _DamText; // 데미지 상태에서 표시될 데미지 값 텍스트

    private float _timerForAttack;
    private float _timerForAttackSnd;
    private bool _attackChkbool; // 공격 상태 체크용 bool

    // Use this for initialization
    void Start()
    {
        // 오디오 소스가 있는 경우 재생
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }

        // _rabbit이 존재하는 경우
        if (_rabbit != null)
        {
            _rabbit.speed = 2.0f;
        }
        // _rabbit이 없는 경우, 자식 컴포넌트에서 Animator 컴포넌트를 찾아서 speed를 설정
        else
        {
            _rabbit = gameObject.GetComponentInChildren<Animator>();
            _rabbit.speed = 2.0f;
        }

        // _uiResult이 존재하는 경우, _resultText를 찾아서 가져옴
        if (_uiResult != null)
        {
            _resultText = _uiResult.transform.Find("3_Result_Text").gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerLive) // 플레이어가 살아있는 동안만 실행
        {
            if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)) // 키보드의 수직 또는 수평 방향키를 누르면
            {
                float _verticalPos = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
                float _horizonPos = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;

                transform.position += new Vector3(-1 * _horizonPos, 0, -1 * _verticalPos); // 플레이어를 이동시키고
                transform.forward = (new Vector3(-1 * _horizonPos, 0, -1 * _verticalPos)); // 플레이어가 바라보는 방향을 이동 방향으로 설정
                if (_rabbit != null) _rabbit.SetBool("runChk", true); // 토끼 캐릭터 애니메이션에서 뛰기 애니메이션을 실행
            }
            else
            {
                if (_rabbit != null) _rabbit.SetBool("runChk", false); // 이동하지 않으면 뛰기 애니메이션을 멈춤
            }

            if (_rabbit != null) // 토끼 캐릭터가 존재하는 경우
            {
                if (Input.GetButtonDown("Fire1") && !_rabbit.GetCurrentAnimatorStateInfo(0).IsName("1_attack")) // 왼쪽 마우스 버튼을 누르고, 현재 실행 중인 애니메이션이 공격 애니메이션인지 아닌지 확인
                {
                    _rabbit.SetBool("attackChk", true); // 공격 애니메이션을 실행
                    if (_attackChkCol != null)
                    {
                        _attackChkCol.enabled = true; // 공격 판정 컬라이더를 활성화
                    }
                    _attackChkbool = true;
                }
                else
                {
                    if (_attackChkCol != null) _attackChkCol.enabled = false; // 공격 판정 컬라이더를 비활성화
                }

                if (_rabbit.GetBool("damageChk")) // 현재 데미지를 받은 상태인지 확인
                {
                    if (_rabbit.GetCurrentAnimatorStateInfo(0).IsName("3_damage")) // 데미지 애니메이션이 실행 중인지 확인
                    {
                        if (_rabbit.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.0f) _rabbit.SetBool("damageChk", false); // 데미지 애니메이션이 끝나면 데미지 상태를 해제
                    }
                }
            }
        }
    }


    void Damaged(float _dam)
    {
        // _hp 변수에서 데미지 값을 뺀다.
        _hp -= _dam;

        // 토끼 애니메이션의 상태가 "3_damage"가 아닌 경우, "damageChk" bool 값을 true로 설정한다.
        if (!_rabbit.GetCurrentAnimatorStateInfo(0).IsName("3_damage"))
        {
            _rabbit.SetBool("damageChk", true);
        }

        // _DamEffect 게임 오브젝트가 존재한다면, 해당 게임 오브젝트를 생성한다.
        if (_DamEffect != null)
        {
            Instantiate(_DamEffect, new Vector3(transform.position.x, 1.0f, transform.position.z), Quaternion.identity);
        }

        // _DamText 게임 오브젝트가 존재한다면, 해당 게임 오브젝트를 생성한다.
        if (_DamText != null)
        {
            Instantiate(_DamText, new Vector3(transform.position.x, 1.2f, transform.position.z + 0.2f), Quaternion.identity);
        }

        // _hp가 0보다 큰 경우, 체력바와 체력값을 업데이트한다.
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
        // _hp가 0 이하인 경우, 체력바와 체력값을 0으로 설정하고, 게임 오버 처리를 수행한다.
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





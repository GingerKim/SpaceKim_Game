// 필요한 Unity 라이브러리를 가져옵니다
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// EnemyScript 클래스를 정의합니다
public class EnemyScript : MonoBehaviour
{

    // 유니티 에디터에서 설정할 public 변수를 선언합니다
    public List<string> _animationName = new List<string>(); // 애니메이션 이름 목록
    public float _speed; // 이동 속도
    public float _attack; // 공격력

    public GameObject _target; // 플레이어 GameObject

    public float _hp; // 체력
    public GameObject _hpBar; // 체력바 GameObject
    public TextMesh _HpVal; // 체력값을 표시하는 텍스트

    public GameObject _HpObj; // 체력바와 체력값을 포함하는 GameObject

    public GameObject _DamEffect; // 피격 이펙트 GameObject
    public GameObject _DamText; // 피격 시 나타나는 데미지 텍스트 GameObject

    public float _timerForLv; // 레벨업 타이머
    public float _timerForLvLim; // 레벨업 타이머 한계치

    public AudioClip _damageSnd; // 피격 사운드

    // Use this for initialization
    void Start()
    {

        // 애니메이션 레이어를 설정합니다
        GetComponent<Animation>()[_animationName[0]].layer = 0;
        GetComponent<Animation>()[_animationName[1]].layer = 1;
        GetComponent<Animation>()[_animationName[2]].layer = 3;
        GetComponent<Animation>()[_animationName[3]].layer = 4;

        // 애니메이션을 CrossFade로 시작합니다
        GetComponent<Animation>().CrossFade(_animationName[0], 0.1f);

        // 2, 3번 애니메이션의 속도를 설정합니다
        GetComponent<Animation>()[_animationName[2]].speed = 2.0f;
        GetComponent<Animation>()[_animationName[3]].speed = 2.0f;

        // 플레이어 GameObject를 찾습니다
        _target = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        // 타이머 변수에 경과한 시간을 더합니다.
        _timerForLv += Time.deltaTime;

        // 타이머 변수가 일정 시간 이상 경과하면 속도를 증가시킵니다.
        if (_timerForLv > _timerForLvLim)
        {
            _speed += 0.5f;
            _timerForLv = 0;
        }

        // 적 캐릭터가 공격 대상인 플레이어 캐릭터를 바라보면서 이동합니다.
        if (_target != null)
        {
            transform.position += (_target.transform.position - transform.position).normalized * _speed * Time.deltaTime;
            GetComponent<Animation>().CrossFade(_animationName[1], 0.1f);
            transform.LookAt(_target.transform);

            // 플레이어 캐릭터와의 거리가 10.0f보다 작을 때, 공격 애니메이션을 재생합니다.
            if ((_target.transform.position - transform.position).magnitude < 10.0f)
            {
                GetComponent<Animation>().CrossFade(_animationName[2], 0.1f);
            }
            else
            {
                // 그렇지 않으면 공격 애니메이션을 정지합니다.
                GetComponent<Animation>().Stop(_animationName[2]);
            }
        }
    }

    // 적 캐릭터가 데미지를 받았을 때 호출되는 함수
    void Damaged(float _dam)
    {
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().PlayOneShot(_damageSnd); // 데미지 효과음 재생
        _hp -= _dam; // 체력 감소
        GetComponent<Animation>().CrossFade(_animationName[3], 0.1f); // 데미지 애니메이션 재생
        if (_DamEffect != null) Instantiate(_DamEffect, new Vector3(transform.position.x, 2.0f, transform.position.z), Quaternion.identity); // 데미지 이펙트 생성
        if (_DamText != null) Instantiate(_DamText, new Vector3(transform.position.x, 1.2f, transform.position.z + 0.2f), Quaternion.identity); // 데미지 텍스트 생성
        if (_hp > 0)
        {
            if (_hpBar != null) _hpBar.transform.localScale = new Vector3(_hp * 0.01f, 1, 1); // 체력바 스케일 조정
            if (_HpVal != null) _HpVal.text = _hp.ToString(); // 체력 텍스트 갱신
        }
        else if (_hp <= 0)
        {
            if (_hpBar != null) _hpBar.transform.localScale = new Vector3(0, 1, 1); // 체력바 스케일 조정
            if (_HpVal != null) _HpVal.text = "0"; // 체력 텍스트 갱신
            _target.GetComponent<PlayerScript>()._gameWin = true; // 게임 승리 변수 true로 변경
            _target.GetComponent<PlayerScript>().GameOver(); // 게임 오버 처리
            DestroyThis(); // 적 캐릭터 제거
        }
    }

    // 적 캐릭터 제거 함수
    void DestroyThis()
    {
        Destroy(gameObject);
        if (_HpObj != null) Destroy(_HpObj);
    }
}
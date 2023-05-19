// �ʿ��� Unity ���̺귯���� �����ɴϴ�
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// EnemyScript Ŭ������ �����մϴ�
public class EnemyScript : MonoBehaviour
{

    // ����Ƽ �����Ϳ��� ������ public ������ �����մϴ�
    public List<string> _animationName = new List<string>(); // �ִϸ��̼� �̸� ���
    public float _speed; // �̵� �ӵ�
    public float _attack; // ���ݷ�

    public GameObject _target; // �÷��̾� GameObject

    public float _hp; // ü��
    public GameObject _hpBar; // ü�¹� GameObject
    public TextMesh _HpVal; // ü�°��� ǥ���ϴ� �ؽ�Ʈ

    public GameObject _HpObj; // ü�¹ٿ� ü�°��� �����ϴ� GameObject

    public GameObject _DamEffect; // �ǰ� ����Ʈ GameObject
    public GameObject _DamText; // �ǰ� �� ��Ÿ���� ������ �ؽ�Ʈ GameObject

    public float _timerForLv; // ������ Ÿ�̸�
    public float _timerForLvLim; // ������ Ÿ�̸� �Ѱ�ġ

    public AudioClip _damageSnd; // �ǰ� ����

    // Use this for initialization
    void Start()
    {

        // �ִϸ��̼� ���̾ �����մϴ�
        GetComponent<Animation>()[_animationName[0]].layer = 0;
        GetComponent<Animation>()[_animationName[1]].layer = 1;
        GetComponent<Animation>()[_animationName[2]].layer = 3;
        GetComponent<Animation>()[_animationName[3]].layer = 4;

        // �ִϸ��̼��� CrossFade�� �����մϴ�
        GetComponent<Animation>().CrossFade(_animationName[0], 0.1f);

        // 2, 3�� �ִϸ��̼��� �ӵ��� �����մϴ�
        GetComponent<Animation>()[_animationName[2]].speed = 2.0f;
        GetComponent<Animation>()[_animationName[3]].speed = 2.0f;

        // �÷��̾� GameObject�� ã���ϴ�
        _target = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ�̸� ������ ����� �ð��� ���մϴ�.
        _timerForLv += Time.deltaTime;

        // Ÿ�̸� ������ ���� �ð� �̻� ����ϸ� �ӵ��� ������ŵ�ϴ�.
        if (_timerForLv > _timerForLvLim)
        {
            _speed += 0.5f;
            _timerForLv = 0;
        }

        // �� ĳ���Ͱ� ���� ����� �÷��̾� ĳ���͸� �ٶ󺸸鼭 �̵��մϴ�.
        if (_target != null)
        {
            transform.position += (_target.transform.position - transform.position).normalized * _speed * Time.deltaTime;
            GetComponent<Animation>().CrossFade(_animationName[1], 0.1f);
            transform.LookAt(_target.transform);

            // �÷��̾� ĳ���Ϳ��� �Ÿ��� 10.0f���� ���� ��, ���� �ִϸ��̼��� ����մϴ�.
            if ((_target.transform.position - transform.position).magnitude < 10.0f)
            {
                GetComponent<Animation>().CrossFade(_animationName[2], 0.1f);
            }
            else
            {
                // �׷��� ������ ���� �ִϸ��̼��� �����մϴ�.
                GetComponent<Animation>().Stop(_animationName[2]);
            }
        }
    }

    // �� ĳ���Ͱ� �������� �޾��� �� ȣ��Ǵ� �Լ�
    void Damaged(float _dam)
    {
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().PlayOneShot(_damageSnd); // ������ ȿ���� ���
        _hp -= _dam; // ü�� ����
        GetComponent<Animation>().CrossFade(_animationName[3], 0.1f); // ������ �ִϸ��̼� ���
        if (_DamEffect != null) Instantiate(_DamEffect, new Vector3(transform.position.x, 2.0f, transform.position.z), Quaternion.identity); // ������ ����Ʈ ����
        if (_DamText != null) Instantiate(_DamText, new Vector3(transform.position.x, 1.2f, transform.position.z + 0.2f), Quaternion.identity); // ������ �ؽ�Ʈ ����
        if (_hp > 0)
        {
            if (_hpBar != null) _hpBar.transform.localScale = new Vector3(_hp * 0.01f, 1, 1); // ü�¹� ������ ����
            if (_HpVal != null) _HpVal.text = _hp.ToString(); // ü�� �ؽ�Ʈ ����
        }
        else if (_hp <= 0)
        {
            if (_hpBar != null) _hpBar.transform.localScale = new Vector3(0, 1, 1); // ü�¹� ������ ����
            if (_HpVal != null) _HpVal.text = "0"; // ü�� �ؽ�Ʈ ����
            _target.GetComponent<PlayerScript>()._gameWin = true; // ���� �¸� ���� true�� ����
            _target.GetComponent<PlayerScript>().GameOver(); // ���� ���� ó��
            DestroyThis(); // �� ĳ���� ����
        }
    }

    // �� ĳ���� ���� �Լ�
    void DestroyThis()
    {
        Destroy(gameObject);
        if (_HpObj != null) Destroy(_HpObj);
    }
}
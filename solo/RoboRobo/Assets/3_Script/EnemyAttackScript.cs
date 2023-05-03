using UnityEngine;
using System.Collections;

public class EnemyAttackScript : MonoBehaviour {
	
	public EnemyScript _EnemySt;
	private float _Attack;

	// Use this for initialization
	void Start () {
		_Attack = _EnemySt._attack;


    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "player")
        {
            Vector3 _Vec = (other.transform.position - transform.position).normalized;
            _Vec.y=0;
            other.transform.localPosition += _Vec * 0.5f;
			other.SendMessage("Damaged",_Attack);
        }
        
    }
}

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	public GameObject _target;
	public float _attackTimer;
	public float _coolDown;
	
	// Use this for initialization
	void Start () {
		_attackTimer = 0;
		_coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(_attackTimer > 0)
			_attackTimer -= Time.deltaTime;
		
		if(_attackTimer < 0)
			_attackTimer = 0;
		
		if(_attackTimer == 0)
		{
			Attack();
			_attackTimer = _coolDown;
		}
	}
	
	private void Attack()
	{
		float distance = Vector3.Distance(_target.transform.position, transform.position);		
		Vector3 dir = (_target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
		if(distance <= 2.5f && direction > 0)
		{		
			PlayerHealthBar eh = (PlayerHealthBar)_target.GetComponent("PlayerHealthBar");
			eh.AddjustCurrentHealth(-10);
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform _target;
	public int _moveSpeed;
	public int _rotationSpeed;
	public int _maxDistance;
	
	private Transform _myTransform;
	
	void Awake()
	{
		_myTransform = transform;
	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		_target = go.transform;
		
		_maxDistance = 2;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(_target.position, _myTransform.position, Color.yellow);	
		
		//Look at Target
		_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, Quaternion.LookRotation(_target.position - _myTransform.position), _rotationSpeed * Time.deltaTime);
		
		if(Vector3.Distance(_target.position, _myTransform.position) > _maxDistance)
		{
			//Move towards Target
			_myTransform.position += _myTransform.forward * _moveSpeed * Time.deltaTime;
		}
	}
}

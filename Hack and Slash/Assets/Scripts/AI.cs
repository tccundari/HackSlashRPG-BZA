using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(SphereCollider))]
public class AI : MonoBehaviour {
	
	private enum State
	{
		Idle,
		Init,
		Setup,
		Search,
		Attack,
		Retreat,
		Flee
	}
	
	public float baseMeleeRange = 3.5f;
	public Transform target;
	public float perceptionRadius = 10;
	
	private Transform _myTransform;
	private State _state;
	private bool _alive = true;
	private SphereCollider _sphereCollider;
	private Transform _home;
	
	private const float ROTATION_DAMP = .1f;
	private const float FORWARD_DAMP = .9f;
			
	void Start()
	{		
		_state = State.Init;
		StartCoroutine("FSM");						
	}
	
	private IEnumerator FSM()
	{
		while(_alive)
		{
			switch(_state)
			{
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Search:
				Search();
				break;
			case State.Attack:
				Attack();
				break;
			case State.Retreat:
				Retreat();
				break;
			case State.Flee:
				Flee();
				break;
			}
			
			yield return null;
		}
	}
	
	private void Init()
	{
		_myTransform = transform;
		_home = transform.parent.transform;
		
		_sphereCollider = GetComponent<SphereCollider>();
		
		if(_sphereCollider == null)
		{
			Debug.LogError("There is no SphereCollidor on this mob");
			return;
		}
		
		_state = State.Setup;
	}
	
	private void Setup()
	{				
		_sphereCollider.isTrigger = true;		
		_sphereCollider.center = GetComponent<CharacterController>().center;
		_sphereCollider.radius = perceptionRadius;
		
		_state = State.Search;
		_alive = false;
	}
	
	private void Search()
	{
		Move();
		_state = State.Attack;
	}
	
	private void Attack()
	{
		Move();
		_state = State.Retreat;
	}
	
	private void Retreat()
	{
		_myTransform.LookAt(target);
		Move();
		_state = State.Search;
	}
	
	private void Flee()
	{
		Move();
		_state = State.Search;
	}
	
	void Update()
	{
	}
	
	private void Move()
	{
		if(target)
		{
			float dist = Vector3.Distance(target.position, _myTransform.position);
							
			#region Move
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot(dir, transform.forward);
			
			if(direction > FORWARD_DAMP && dist > baseMeleeRange)
			{
				SendMessage("MoveMeForward", Movement.Forward.forward);
			}
			else
			{
				SendMessage("MoveMeForward", Movement.Forward.none);				
			}			
			#endregion
			
			#region Rotate
			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot(dir, transform.right);
			
			if(direction > ROTATION_DAMP && dist > baseMeleeRange)
			{
				SendMessage("RotateMe", Movement.Turn.right);
			}
			else if (direction < -ROTATION_DAMP)
			{
				SendMessage("RotateMe", Movement.Turn.left);				
			}
			else
			{
				SendMessage("RotateMe", Movement.Turn.none);
			}
			#endregion			
		}
		else
		{
			SendMessage("MoveMeForward", Movement.Forward.none);
			SendMessage("RotateMe", Movement.Turn.none);
		}
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			target = other.transform;
			_alive = true;
			
			StartCoroutine("FSM");
		}
	}
	
	public void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			target = _home;
			//_alive = false;
		}
	}	
}

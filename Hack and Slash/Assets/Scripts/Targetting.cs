/// <summary>
/// Targetting.cs
/// Tiago Cundari
/// 2012/12/10
/// 
/// * Equivale a TargetMob do Tutorial - Video 51 aos 08:00 min
/// 
/// This script can be attach to any permanent Game Object and it is responsable for allowing the player to target different mobs in any range
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {
	
	public List<Transform> _targets;	
	public Transform _selectedTarget;
	
	private Transform _myTransform;
	
	// Use this for initialization
	void Start () {
		_targets = new List<Transform>();	
		_selectedTarget = null;
		
		_myTransform = transform;
		AddAllEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			TargetEnemy();
		}
	}
	
	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		if(go != null)
		{
			foreach(GameObject enemy in go)
			{
				AddTarget(enemy.transform);
			}
		}
	}
	
	public void AddTarget(Transform enemy)
	{
		_targets.Add(enemy);
	}
	
	private void TargetEnemy()
	{
		if(_targets.Count == 0)
		{
			AddAllEnemies();			
		}
		else
		{
			if(_selectedTarget == null)
			{
				SortTargetByDistancy();
				_selectedTarget = _targets[0];
			}
			else
			{
				int index = _targets.IndexOf(_selectedTarget);
				
				if(index < _targets.Count - 1)
					index ++;
				else
					index = 0;
				
				DeselectTarget();
				_selectedTarget = _targets[index];			
			}
			SelectTarget();
		}		
	}
	
	private void SortTargetByDistancy()
	{
		_targets.Sort(delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, _myTransform.position).CompareTo(Vector3.Distance(t2.position, _myTransform.position));
		});
	}
	
	private void SelectTarget()
	{
		Transform name = _selectedTarget.FindChild("Name");
		
		if(name == null)
		{
			Debug.LogError("Could not find the Name on " + _selectedTarget.name);
			return;
		}
		
		name.GetComponent<TextMesh>().text = _selectedTarget.GetComponent<Mob>().Name;
		name.GetComponent<MeshRenderer>().enabled = true;
		_selectedTarget.GetComponent<Mob>().DisplayHealth();
		
		Messenger<bool>.Broadcast("show mob vitalBars", true);
	}
	
	private void DeselectTarget()
	{
		_selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
		_selectedTarget = null;
		Messenger<bool>.Broadcast("show mob vitalBars", false);		
	}
}

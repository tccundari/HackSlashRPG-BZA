using UnityEngine;
using System.Collections;

public class PlayerHealthBar: MonoBehaviour {
	
	public int _maxHealth;
	public int _currentHealth;
	public float _healthBarLenght;
		
	
	// Use this for initialization
	void Start () {	
	  	_maxHealth = 100;
		_currentHealth = _maxHealth;		
		_healthBarLenght = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurrentHealth(0);
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(10,10,_healthBarLenght,20), _currentHealth + "/" + _maxHealth);	
	}
	
	public void AddjustCurrentHealth(int adj)
	{
		_currentHealth += adj;
		
		if(_currentHealth < 0)
			_currentHealth = 0;
		
		if(_currentHealth > _maxHealth)
			_currentHealth = _maxHealth;
		
		if(_maxHealth < 1)
			_maxHealth = 1;
		
		_healthBarLenght = (Screen.width / 2) * (_currentHealth / (float)_maxHealth);
	}
}

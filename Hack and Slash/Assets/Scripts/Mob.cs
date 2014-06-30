using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter {
	public int curHealth;
	public int maxHelath;
	
	// Use this for initialization
	void Start () {
		//GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
		//GetVital((int)VitalName.Health).Update();
		
		Name = "Slug Mob";
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	public void DisplayHealth()
	{
		Messenger<int, int>.Broadcast("mob health update", curHealth, maxHelath);		
	}
}

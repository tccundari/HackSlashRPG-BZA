using UnityEngine;

public class Consumable : BuffItem {
	private Vital[] _vital;		//Vitals to heal
	private int[] _amountToHeal;	//the amount to heal each vital
	
	private float _buffTime;		//how long the buff lasts if there is one
	
	public float BuffTime {
		get {
			return this._buffTime;
		}
		set {
			_buffTime = value;
		}
	}
	public Consumable()
	{
		Reset ();
	}
	
	public Consumable(Vital[] v, int[] a, float b)
	{
		_vital = v;
		_amountToHeal = a;
		_buffTime = b;
	}
	
	public  void Reset()
	{
		_buffTime = 0;
		
		for(int cnt = 0; cnt < _vital.Length; cnt++)
		{
			_vital[cnt] = new Vital();
			_amountToHeal[cnt] = 0;
		}
	}
	
	public int VitalCount()
	{
		return _vital.Length;
	}
	
	public Vital VitalAtIndex(int index)
	{
		if (index < _vital.Length && index > -1)
			return _vital[index];
		else
			return new Vital();
	}
	
	public int HealAtIndex(int index)
	{
		if (index < _amountToHeal.Length && index > -1)
			return _amountToHeal[index];
		else
			return 0;
	}
	
	public void SetVitalAt(int index, Vital vital)
	{
		if (index < _vital.Length && index > -1)
			_vital[index] = vital;
	}
	
	public void SetHealAt(int index, int heal)
	{
		if (index < _amountToHeal.Length && index > -1)
			_amountToHeal[index] = heal;
	}
	
	public void SetVitalAndHealAt(int index, Vital vital, int heal)
	{
		SetVitalAt(index, vital);
		SetHealAt(index, heal);
	}
	
}

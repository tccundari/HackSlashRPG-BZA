using UnityEngine;

public class Armor : Clothing {
	private int _armorLevel;		//the armor level of this piece of armor
	
	public Armor()
	{
		_armorLevel = 0;
	}
	
	public Armor(int al)
	{
		_armorLevel = al;
	}
	
	public int ArmorLevel {
		get {
			return this._armorLevel;
		}
		set {
			_armorLevel = value;
		}
	}
}


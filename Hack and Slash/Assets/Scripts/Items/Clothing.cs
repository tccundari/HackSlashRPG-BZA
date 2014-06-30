using UnityEngine;

public class Clothing : BuffItem {
	private ArmorSlot _slot;		//store the slot the armor will be in
		
	public Clothing()
	{
		_slot = ArmorSlot.Head;
	}
	
	public Clothing(ArmorSlot slot)
	{
		_slot = slot;
	}
	
	public ArmorSlot Slot {
		get {
			return this._slot;
		}
		set {
			_slot = value;
		}
	}	
}

public enum ArmorSlot
{
	Head,
	Shoulders,
	UpperBody,
	Torso,
	Legs,
	Hands,
	Feet,
	Back
}

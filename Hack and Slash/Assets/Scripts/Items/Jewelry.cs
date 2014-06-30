using UnityEngine;
using System.Collections;

public class Jewelry : BuffItem {
	private JewelrySlot _slot;		//store the slot the Jewelry is in

	public Jewelry()
	{
		_slot = JewelrySlot.PocketItem;
	}
	public Jewelry(JewelrySlot slot)
	{
		_slot = slot;
	}
	
	public JewelrySlot Slot {
		get {
			return this._slot;
		}
		set {
			_slot = value;
		}
	}		
}

public enum JewelrySlot
{
	EarRings,
	Necklaces,
	Bracelets,
	Rings,
	PocketItem
}
using UnityEngine;

public class Item {
	private string _name;
	private int _value;
	private RarityTypes _rarity;
	private int _curDur;
	private int _maxDur;

	public Item()
	{
		_name = "Need Name";
		_value = 0;
		_rarity = RarityTypes.Common;
		_maxDur = 50;
		_curDur = _maxDur;		
	}
	
	public Item(string name, int value, RarityTypes rare, int maxDur, int curDur)
	{
		_name = name;
		_value = value;
		_rarity = rare;
		_maxDur = maxDur;
		_curDur = curDur;		
	}
	
#region Set/Get
	public string Name {
		get {
			return this._name;
		}
		set {
			_name = value;
		}
	}

	public RarityTypes Rarity {
		get {
			return this._rarity;
		}
		set {
			_rarity = value;
		}
	}

	public int Value {
		get {
			return this._value;
		}
		set {
			_value = value;
		}
	}
	
	public int CurDurability {
		get {
			return this._curDur;
		}
		set {
			_curDur = value;
		}
	}

	public int MaxDurability {
		get {
			return this._maxDur;
		}
		set {
			_maxDur = value;
		}
	}	
#endregion
	
	
}

public enum RarityTypes
{
	Common,
	Uncommon,
	Rare
}

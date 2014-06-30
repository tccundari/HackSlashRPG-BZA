public class Weapon : BuffItem {
	private int _maxDamage;
	private float _dmgVar;
	private float _maxRange;
	private DamageType _dmgType;
	
#region Get/Set	
	public DamageType TypeOfDamage {
		get {
			return this._dmgType;
		}
		set {
			_dmgType = value;
		}
	}

	public float DmgVar {
		get {
			return this._dmgVar;
		}
		set {
			_dmgVar = value;
		}
	}

	public int MaxDamage {
		get {
			return this._maxDamage;
		}
		set {
			_maxDamage = value;
		}
	}

	public float MaxRange {
		get {
			return this._maxRange;
		}
		set {
			_maxRange = value;
		}
	}	
#endregion
	
	public Weapon()
	{
		_maxRange = 0;
		_dmgVar = 0;
		_maxDamage = 0;
		_dmgType = DamageType.Bludgeon;
	}
	
	public Weapon(int mDmg, float dmgV, float mRange, DamageType dt)
	{
		_maxDamage = mDmg;
		_dmgVar = dmgV;
		_maxRange = mRange;
		_dmgType = dt;
	}
}

public enum DamageType
{
	Bludgeon,
	Piece,
	Slash,
	Fire,
	Ice,
	Lighting,
	Acid
}

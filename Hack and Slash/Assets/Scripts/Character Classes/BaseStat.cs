/// <summary>
/// Base stat.cs
/// Tiago Cundari
/// 2012/12/07
/// 
/// This is the base class for all stats in game
/// </summary>
public class BaseStat {
	
	public const int STARTTING_EXP_COST = 100;	//public accesable value for all base stats to start at
	
	private int _baseValue;						//The Base value of this stat
	private int _buffValue;						//The mount of the buff to this stat
	private int _expToLevel;					//The total amount of exp needed to raise this skill
	private float _levelModifier;				//Modifier applied to the exp needed to raise the skill
	
	private string _name;							//This is the name of the attribute
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat()
	{
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTTING_EXP_COST;
		_name = string.Empty;
	}
	
#region Propperties
	/// <summary>
	/// Gets or sets the _baseValue.
	/// </summary>
	/// <value>
	/// The _baseValue.
	/// </value>
	public int BaseValue
	{
		get { return _baseValue; }
		set { _baseValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _buffValue.
	/// </summary>
	/// <value>
	/// The _buffValue.
	/// </value>
	public int BuffValue
	{
		get { return _buffValue; }
		set { _buffValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _expToLevel.
	/// </summary>
	/// <value>
	/// The _expToLevel.
	/// </value>
	public int ExpToLevel
	{
		get { return _expToLevel; }
		set { _expToLevel = value; }
	}
	
	/// <summary>
	/// Gets or sets the _levelModifier.
	/// </summary>
	/// <value>
	/// The _levelModifier.
	/// </value>
	public float LevelModifier
	{
		get { return _levelModifier; }
		set { _levelModifier = value; }
	}
	
	/// <summary>
	/// Gets or sets the _name.
	/// </summary>
	/// <value>
	/// The _name.
	/// </value>
	public string Name
	{
		get{ return _name;}
		set{ _name = value; }
	}
#endregion
	
	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int CalculateExpToLevel()
	{
		return (int)(_expToLevel * _levelModifier);
	}
	
	/// <summary>
	/// Assign a new value to _expToLevel and then increase the _baseLevel by one.
	/// </summary>
	public void LevelUp()
	{
		_expToLevel = CalculateExpToLevel();
		_baseValue ++;
	}
	
	/// <summary>
	/// Recalculate the adjusted base value and return it
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int AdjustedBaseValue
	{
		get {return _baseValue + _buffValue;}
	}
}

/// <summary>
/// Attribute.cs
/// Tiago Cundari
/// 2012-12-07
/// 
/// This is the class for all character attributes in-game
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTTING_EXP_COST = 50;	//This is the startting cost for all of our attributes
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute()
	{
		ExpToLevel = STARTTING_EXP_COST;
		LevelModifier = 1.05f;
	}
}

/// <summary>
/// This is a list of all attributes that we will have in-game for all characters
/// </summary>
public enum AttributeName
{
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	WillPower,
	Charisma
}
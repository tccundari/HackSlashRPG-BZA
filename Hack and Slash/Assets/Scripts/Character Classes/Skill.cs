/// <summary>
/// Skill.cs
/// Tiago Cundari
/// 2012/12/07
/// 
/// This class contains all the extra functions that are needed for a skill
/// </summary>
public class Skill : ModifiedStat {
	private bool _know;				//a boolean variable to toggle if a character knows the skill
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill()
	{
		_know = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is know.
	/// </summary>
	/// <value>
	/// <c>true</c> if know; otherwise, <c>false</c>.
	/// </value>
	public bool Know
	{
		get { return _know; }
		set { _know = value; }
	}
}

/// <summary>
/// This enumeration is just a list of skills the player can learn
/// </summary>
public enum SkillName
{
	Melee_Offense,
	Melee_Defense,
	Range_Offense,
	Range_Defense,
	Magic_Offense,
	Magic_Defense
}

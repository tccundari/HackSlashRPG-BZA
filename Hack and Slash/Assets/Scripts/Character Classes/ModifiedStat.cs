/// <summary>
/// Modified stat.cs
/// Tiago Cundari
/// 2012/12/07
/// 
/// This is the base class for all stats that will be modifiable by attributes
/// </summary>
using System.Collections.Generic;			//Generic was added so we can use the List<>

public class ModifiedStat : BaseStat {
	private List<ModifyingAttibrute> _mods; //List of attributes tha modify this stat
	private int _modValue;					//The amount to added baseValue from this modifiers
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat()
	{
		_mods = new List<ModifyingAttibrute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Add a ModifyingAttribute to our list of mods for this ModifiedStat
	/// </summary>
	/// <param name='mod'>
	/// Mod.
	/// </param>
	public void AddModifier(ModifyingAttibrute mod)
	{
		_mods.Add(mod);
	}
	
	/// <summary>
	/// Reset _modValue to 0.
	/// Check to see if we have at least one ModifyingAttribute in our list os mods.
	/// If we do, then interate through the list and add the AdjustedBaseValue * ratio to our modValue
	/// </summary>
	private void CalculateModValue()
	{
		_modValue = 0;
		
		if(_mods.Count > 0)
		{
			foreach(ModifyingAttibrute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ration);
		}
	}
	
	/// <summary>
	/// This function is overrinding the AdjustedBaseValue in the BaseStat class.
	/// Calculate the AdjustedBaseValue from the BaseValue + BuffValue + _modValue
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int AdjustedBaseValue
	{
		get {return BaseValue + BuffValue + _modValue;}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update()
	{
		CalculateModValue();
	}
	
	public string GetModifyingAttributesString()
	{
		string temp = string.Empty;
		
		for(int cnt = 0; cnt < _mods.Count; cnt++)
		{
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ration;
			
			if(cnt < _mods.Count - 1)
			{
				temp += "|";
			}
		}
		
		return temp;
	}
}

/// <summary>
/// A structure that will hold an Attribute and a ratio that will be added as modifying attribute to a ModifiedStat
/// </summary>
public struct ModifyingAttibrute {
	public Attribute attribute;		//the attribute to be used as a modifier
	public float ration;			//the percent of the attributes AdjustedBaseValue that will be applied to the ModifiedStat 
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttibrute"/> struct.
	/// </summary>
	/// <param name='att'>
	/// Attribute to be used
	/// </param>
	/// <param name='rat'>
	/// Ratio to be used
	/// </param>
	public ModifyingAttibrute(Attribute att, float rat)
	{
		attribute = att;
		ration = rat;
	}
}
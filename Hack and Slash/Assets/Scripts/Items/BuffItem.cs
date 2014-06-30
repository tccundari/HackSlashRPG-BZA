using UnityEngine;
using System.Collections;

public class BuffItem : Item {
	//private int[] buffMod;
	//private BaseStat[] stat;
	
	private Hashtable buffs;
	
	public BuffItem()
	{
		buffs = new Hashtable();
	}
	
	public BuffItem(Hashtable ht)
	{
		buffs = ht;
	}
	
	public void AddBuff(BaseStat stat, int mod)
	{
		buffs.Add(stat.Name, mod);
	}
	
	public void RemoveBuff(BaseStat stat)
	{
		buffs.Remove(stat.Name);
	}
	
	public int BuffCount()
	{
		return buffs.Count;
	}
	
	public Hashtable GetBuffs()
	{
		return buffs;
	}
}

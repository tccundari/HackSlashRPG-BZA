using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";	//This is the name of the Game object that player will apwn at the start of the level
	
	void Awake()
	{
		DontDestroyOnLoad(this);
	}	
	
	public void SaveCharacterData()
	{		
		GameObject pc = GameObject.Find("pc");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		//Limpa Cache
		PlayerPrefs.DeleteAll();
			
		PlayerPrefs.SetString("Player Name", pcClass.Name);
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)			
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp To Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)			
		{
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp To Level", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Cur Value", pcClass.GetVital(cnt).CurValue);
			
			//PlayerPrefs.SetString(((VitalName)cnt).ToString() + " - Mods", pcClass.GetVital(cnt).GetModifyingAttributesString());
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)			
		{
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp To Level", pcClass.GetSkill(cnt).ExpToLevel);
			
			//PlayerPrefs.SetString(((SkillName)cnt).ToString() + " - Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());
		}
	}
	
	public void LoadCharacterData()
	{	
		GameObject pc = GameObject.Find("pc");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		if(pcClass == null)
			Debug.LogError("O personagem configurado nao possui o script 'PlayerCharacter'");
		
		pcClass.Name = PlayerPrefs.GetString("Player Name", "Name Me");		
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)			
		{
			pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp To Level", Attribute.STARTTING_EXP_COST);
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)			
		{
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp To Level", 0);
			
			//make sure you call this so that the AdjustedValue will be updated before you try to call to get de CurValue
			pcClass.GetVital(cnt).Update();
			
			//get the stored value for the CurValue for each vital
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Cur Value", 1);			
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)			
		{
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Exp To Level", 0);			
		}
	}
}

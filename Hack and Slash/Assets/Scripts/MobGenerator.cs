using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	public enum State
	{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	
	public GameObject[] mobPrefabs;		//an array to hold all of the prefabs of mobs we want to spawn
	public GameObject[] spawnPoints;	//this array will hold a reference to all the spawnPoints in the scene
	
	public State state;					//this is our local variable that holds our current state
	
	
	void Awake()
	{
		state = MobGenerator.State.Initialize;
	}
	
	// Use this for initialization
	IEnumerator Start () {
		while(true)
		{
			switch(state)
			{
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;				
			}
			
			yield return 0;
		}
	}
	
	private void Initialize()
	{		
		if(!CheckForMobPrefabs() || !CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	private void Setup()
	{
		state = MobGenerator.State.SpawnMob;
	}
	
	private void SpawnMob()
	{		
		GameObject[] gos = AvaliableSpawnPoints();
		
		for(int cnt = 0; cnt < gos.Length; cnt++)
		{
			GameObject go = Instantiate(mobPrefabs[	Random.Range(0, mobPrefabs.Length)],
													gos[cnt].transform.position, 
													Quaternion.identity
										) as GameObject;
			
			go.transform.parent = gos[cnt].transform;
		}
		
		state = MobGenerator.State.Idle;
	}
	
	//Check to see if we have at least one mob prefab to spawn
	private bool CheckForMobPrefabs()
	{
		if(mobPrefabs.Length > 0)
			return true;
		else
		{
			Debug.LogError("Could not find any mob to spawn");
			return false;
		}
	}	
	
	//Check to see if we have at least one spawn point to spawn mobs at
	private bool CheckForSpawnPoints()
	{
		if(spawnPoints.Length > 0)
			return true;
		else
		{
			Debug.LogError("Could not find any spawn point");
			return false;
		}
	}
	
	//Generate a list of avaliable spawnpoints that do not have any mobs childred to it
	private GameObject[] AvaliableSpawnPoints()
	{
		List<GameObject> gos = new List<GameObject>();
		
		//interate though our spawn points and add the ones that do not have a mob under it to the list
		for(int cnt = 0; cnt < spawnPoints.Length; cnt++)
		{
			if(spawnPoints[cnt].transform.childCount == 0)
			{
				gos.Add(spawnPoints[cnt]);
			}
		}
		
		return gos.ToArray();
	}
}

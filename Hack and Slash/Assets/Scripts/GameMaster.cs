using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	public GameObject playerCharacter;
	public GameObject gameSettings;
	public Camera mainCamera;
	
	public float zOffset;
	public float yOffset;
	public float xRotOffset;
	
	private GameObject _pc;
	private PlayerCharacter _pcScript;
	
	private Vector3 _playerSpawnPointPos;	//This is the place in 3D Space where I want the player to spawn
		
	// Use this for initialization
	void Start () {
		_playerSpawnPointPos = new Vector3(1082,295,1343);		//Default Position for our player spawn point
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);				
		
		if(go == null)
		{
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			
			go.transform.position = _playerSpawnPointPos;
		}
		
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "pc";
		
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		zOffset = -2.5f;
		yOffset = 2.5f;
		xRotOffset = 18f;
		
		mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset, 0, 0);
		
		LoadCharacter();
	}
	
	public void LoadCharacter()
	{
		GameObject gs = GameObject.Find("__GameSettings");
		
		if(gs == null)
		{
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}
		
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
			
			//load the Character data
			gsScript.LoadCharacterData();
	}	
}

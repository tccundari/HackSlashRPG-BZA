using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]
public class Chest : MonoBehaviour {
	
	public enum State
	{
		Open,
		Close,
		inbetween
	}
	
	public AudioClip openSound;
	public AudioClip CloseSound;
	public State state;
	public GameObject[] parts;
	public GameObject particleEffect;
	public float maxDistance = 4;	
	public List<Item> loot = new List<Item>();
	
	private Color[] _defaultColors;
	private GameObject _player;
	private Transform _myTransform;
	private bool inUse = false;
	private bool _used = false;
	
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		
		state = Chest.State.Close;
		
		particleEffect.SetActive(false);
		
		if(parts.Length > 0)
		{			
			_defaultColors = new Color[parts.Length];
			
			for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
			{
				_defaultColors[cnt] = parts[cnt].renderer.material.GetColor("_Color");
			}
		}
	}
	
	void Update()
	{
		if(!inUse)
			return;
		
		if(_player == null)
			return;
		
		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
		{
			MyGUI.chest.ForceClose();
			//Messenger.Broadcast("CloseChest");
		}
	}
	
	public void OnMouseEnter()
	{
		HighLight(true);
	}
	
	public void OnMouseExit()
	{
		HighLight(false);
	}	
	
	public void OnMouseUp()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		if(go == null)
			return;		
		
		if(Vector3.Distance(_myTransform.position, go.transform.position) > maxDistance && !inUse)
			return;
		
		switch(state)
		{
		case State.Open:
			state = State.inbetween;
			//StartCoroutine("Close");
			ForceClose();
			break;
		case State.Close:
			if(MyGUI.chest != null)
			{
				MyGUI.chest.ForceClose();
			}
			state = State.inbetween;
			StartCoroutine("Open");
			break;
		}		
	}
	
	private IEnumerator Open()
	{	
		MyGUI.chest = this;
		inUse = true;
		_player = GameObject.FindGameObjectWithTag("Player");
		animation.Play("Open");				
		audio.PlayOneShot(openSound);
		particleEffect.SetActive(true);
		if(!_used)
			PopulateChest(5);		
		
		yield return new WaitForSeconds(animation["Open"].length);	
		
		state = Chest.State.Open;
		Messenger.Broadcast("DisplayLoot");
	}
	
	private void PopulateChest(int x)
	{
		for(int cnt = 0; cnt < x; cnt++)
		{
			loot.Add(new Item());
			loot[cnt].Name = "I:" + Random.Range(0, 100);
		}
		
		_used = true;
	}
	
	private IEnumerator Close()
	{	
		inUse = false;
		_player = null;
		particleEffect.SetActive(false);
		animation.Play("Close");		
		yield return new WaitForSeconds(animation["Close"].length);
		audio.PlayOneShot(CloseSound);
		
		state = Chest.State.Close;
		
		if(loot.Count == 0)
			Destroy (gameObject);
	}
	
	public void ForceClose()
	{
		Messenger.Broadcast("CloseChest");		
		
		StopCoroutine("Open");
		StartCoroutine("Close");
	}			
	
	private void HighLight(bool glow)
	{
		if(glow)
		{
			for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
			{
				parts[cnt].renderer.material.SetColor("_Color", Color.yellow);
			}
		}
		else
		{
			for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
			{
				parts[cnt].renderer.material.SetColor("_Color", _defaultColors[cnt]);
			}
		}
	}
}

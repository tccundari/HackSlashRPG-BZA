using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGUI : MonoBehaviour {
	
	public float lootWindowHeight = 90;
	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;	
	private float _offSet = 10;
	
	//Loot
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;	
	private bool _displayLootWindow = false;
	public static Chest chest;
	
	//Inventory
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _InventoryWindowRect = new Rect(10,10,170,265);	
	private bool _displayInventoryWindow = true;
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;
	
	
	// Use this for initialization
	void Start () {
	}
	
	private void OnEnable()
	{
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("CloseChest", ClearWindow);
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
	}
	
	private void OnDisable()
	{				
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI()
	{
		if(_displayInventoryWindow)
			_InventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _InventoryWindowRect, InventoryWindow, "Inventory");		
		
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offSet, Screen.height - (_offSet + lootWindowHeight), Screen.width - (_offSet * 2), lootWindowHeight), LootWindow, "Loot Window", "box");		
	}
	
	public void LootWindow(int id)
	{				
		if(GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "X"))
		{
			ClearWindow();
		}		
		
		if(chest == null)
			return;
		
		if(chest.loot.Count == 0)
		{
			ClearWindow();
			return;
		}			
		
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offSet * .5f, 15, (_lootWindowRect.width - 10), 70),_lootWindowSlider, new Rect(0,0, _offSet +(chest.loot.Count * buttonWidth),buttonHeight + _offSet));
				
		for(int cnt = 0; cnt < chest.loot.Count; cnt++)
		{
			if(GUI.Button(new Rect(_offSet + (buttonWidth * cnt), _offSet, buttonWidth, buttonHeight), chest.loot[cnt].Name))
			{
				chest.loot.RemoveAt(cnt);
			}
		}
		
		GUI.EndScrollView();
	}
	
	private void DisplayLoot()
	{
		_displayLootWindow = true;			
	}
	
	private void ClearWindow()
	{
		chest.OnMouseUp();
		
		chest = null;
		_displayLootWindow = false;		
	}
	
	public void InventoryWindow(int id)
	{
		for(int y = 0; y < _inventoryRows; y++)
		{
			for(int x = 0; x < _inventoryCols; x++)
			{
				GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString());  
			}
		}
		GUI.DragWindow();
	}
	
	public void ToggleInventoryWindow()
	{
		_displayInventoryWindow = !_displayInventoryWindow;
	}
}


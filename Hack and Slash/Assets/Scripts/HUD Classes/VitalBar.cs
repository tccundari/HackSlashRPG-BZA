/// <summary>
/// VitalBar.cs
/// Tiago Cundari
/// 2012/12/07
/// 
/// This class is responsable for displaying a vital for the player or a mob
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar;		//This boolean value tells us if this is the player health bar or the mod health bar
	private int _maxBarLenght;				//This is how large the vital bar can be if the target is at 100% health
	private int _curBarLenght;				//This is the current lenght of the vital bar
	
	private GUITexture _display;
	
	void Awake()
	{
		_display = gameObject.GetComponent<GUITexture>();
	}
	
	// Use this for initialization
	void Start () {
		_maxBarLenght = (int)_display.pixelInset.width;
		
		OnEnable();
	}
		
	// Update is called once per frame
	void Update () {
	
	}
	
	//This method is called when the GamoObject is enabled
	public void OnEnable()
	{
		if(_isPlayerHealthBar)
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		else
		{
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalBars", ToggleDisplay);			
		}
	}
	
	//This method is called when the GamoObject is disabled
	public void OnDisable()
	{
		if(_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		else
		{
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalBars", ToggleDisplay);			
		}
			
	}
	
	//This method will calculate the total size of health bar in relation to percentage health that target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth)
	{	
		_curBarLenght = (int)((curHealth / (float)maxHealth) * _maxBarLenght); // this calculate the current bar lenght based on the players health %
		_display.pixelInset = CalculatePosition();
		
	}
	
	//Setting the health bar to the player or a mob
	public void SetPlayerHealthBar(bool b)
	{
		_isPlayerHealthBar = b;
	}
	
	private Rect CalculatePosition()
	{
		float yPos = _display.pixelInset.y / 2 - 10;
		
		if(!_isPlayerHealthBar)
		{
			float xPos = (_maxBarLenght - _curBarLenght) - (_maxBarLenght / 4 + 10);
			return new Rect(xPos,yPos, _curBarLenght, _display.pixelInset.height);
		}
		else
			return _display.pixelInset = new Rect(_display.pixelInset.x,yPos, _curBarLenght, _display.pixelInset.height);
	}
	
	private void ToggleDisplay(bool show)
	{
		_display.enabled = show;
	}
}

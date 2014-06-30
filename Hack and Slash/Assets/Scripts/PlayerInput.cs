using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Movement))]
public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {		
		if(Input.GetButtonUp("Toggle Inventory"))
		{
			Messenger.Broadcast("ToggleInventory");
		}
		
#region Move Forward
		if(Input.GetButton("Move Forward"))
		{
			if(Input.GetAxis("Move Forward") > 0)
			{
				SendMessage("MoveMeForward", Movement.Forward.forward);
			}
			else
			{
				SendMessage("MoveMeForward", Movement.Forward.back);
			}
		}		
		
		if(Input.GetButtonUp("Move Forward"))
		{
			SendMessage("MoveMeForward", Movement.Forward.none);
		}		
#endregion
		
#region Rotate Player
		if(Input.GetButton("Rotate Player"))
		{
			if(Input.GetAxis("Rotate Player") > 0)
			{
				SendMessage("RotateMe", Movement.Turn.right);
			}
			else
			{
				SendMessage("RotateMe", Movement.Turn.left);
			}
		}
		
		if(Input.GetButtonUp("Rotate Player"))
		{
			SendMessage("RotateMe", Movement.Turn.none);
		}
#endregion
		
#region Strafe
		if(Input.GetButton("Strafe"))
		{
			if(Input.GetAxis("Strafe") > 0)
			{
				SendMessage("StrafeMe", Movement.Turn.right);
			}
			else
			{
				SendMessage("StrafeMe", Movement.Turn.left);
			}
		}
		
		if(Input.GetButtonUp("Strafe"))
		{
			SendMessage("StrafeMe", Movement.Turn.none);
		}
#endregion			
		
		if(Input.GetButtonDown("Run"))
		{
			SendMessage("ToggleRun");
		}
				
		if(Input.GetButtonDown("Jump"))
		{
			SendMessage("JumpUp");
		}
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Water"))
		{
			SendMessage("IsSwimming", true);
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Water"))
		{
			SendMessage("IsSwimming", false);
		}
	}
}

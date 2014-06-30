using UnityEngine;
using System.Collections;

public class HackAndSlashCamera : MonoBehaviour {
	
	public Transform target;
	public float walkDistance;
	public float renDistance;
	public float height;
	public string playerTagName = "Player";
	
	public float xSpeed;
	public float ySpeed;
	public float heightDamping;
	public float rotationDamping;
			
	private Transform _myTransform;
	private float _x;
	private float _y;
	private bool _camButtonDown = false; 
	private bool _rotateCameraPressed = false;
	
	void Awake()
	{
		_myTransform = transform;
		
		xSpeed = 250.0f;
		ySpeed = 120.0f;
		
		heightDamping = 2.0f;
		rotationDamping = 3.0f;
	}	
	
	// Use this for initialization
	void Start () {		
		if(target == null)
			return;
			//Debug.LogWarning("We do not have a target");
		else
			CameraSetup();				
	}
	
	void Update()
	{
		if(Input.GetButtonDown("Rotate Camera Button")) //0 = Left Button, 1 = Right Button
			_camButtonDown = true;
		
		if(Input.GetButtonUp("Rotate Camera Button")) //0 = Left Button, 1 = Right Button
		{
			_x = _myTransform.rotation.x;
			_y = _myTransform.rotation.y;
			_camButtonDown = false;
		}
		
		
		if(Input.GetButtonDown("Rotate Camera Horizontal Button") || Input.GetButtonDown("Rotate Camera Vertical Button")) 
			_rotateCameraPressed = true;
		
		
		if(Input.GetButtonUp("Rotate Camera Horizontal Button") || Input.GetButtonUp("Rotate Camera Vertical Button"))
		{
			_x = 0;
			_y = 0;
			_rotateCameraPressed = false;
		}
	}
	
	void LateUpdate()
	{		
		if(target != null)
		{		
			if(_rotateCameraPressed)
			{
	        	_x += Input.GetAxis("Rotate Camera Horizontal Button") * xSpeed * 0.02f;
	        	_y -= Input.GetAxis("Rotate Camera Vertical Button") * ySpeed * 0.02f;
	 		 	
				RotateCamera();
			}			
			else if(_camButtonDown)
			{
	        	_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
	        	_y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
	 		 	
				RotateCamera();
			}
			else
			{
				// Calculate the current rotation angles
				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
					
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * walkDistance;
			
				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt (target);
			}
		}
		else
		{
			GameObject go = GameObject.FindGameObjectWithTag(playerTagName);
			
			if(go == null)
				return;
			
			target = go.transform;
		}
	}
	
	public void RotateCamera()
	{
		Quaternion rotation = Quaternion.Euler(_y, _x, 0);
	    Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;
	        
	    _myTransform.rotation = rotation;
	    _myTransform.position = position;
	}
	
	public void CameraSetup()
	{
		_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
		_myTransform.LookAt(target);
	}
}

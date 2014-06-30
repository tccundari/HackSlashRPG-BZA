
var ForwardSpeed : float = 2.5;
var BackwardSpeed : float = 2.0;
var JumpForce : float = 2.0;

private var ForwardDirection : Vector3 = Vector3.zero;
private var charController : CharacterController;
private var gravity : float = 9.81;
private var RunSpeed : float = 5.0;




function Start()
{
    charController = GetComponent(CharacterController);
	
	//Ajust the animation speed to ForwardSpeed and BackwardSpeed
	animation["walk7"].speed = ForwardSpeed/1.5;
	animation["back"].speed = BackwardSpeed/4*3;
	
}


function Update () 
{

    if(charController.isGrounded == true)
    {
		
		//Moving forward
        if(Input.GetAxis("Vertical") > .1) {
		
			animation.CrossFade("walk7");
			RunSpeed = ForwardSpeed;
		
		//Moving backward
        } else if(Input.GetAxis("Vertical") < -.1){
		
			animation.CrossFade("back");
			RunSpeed = BackwardSpeed;
		
		} else {
			
			if(Input.GetAxis("Horizontal") && !Input.GetAxis("Vertical")) animation.CrossFade("turn");
			else animation.CrossFade("idle16", 0.1);		
	
        }
		
		
        transform.eulerAngles.y += 2*Input.GetAxis("Horizontal");
        ForwardDirection = Vector3(0,0, Input.GetAxis("Vertical"));
        ForwardDirection = transform.TransformDirection(ForwardDirection);

		//Jump
		if (Input.GetButton ("Jump")) {
			
			animation.CrossFade("jump");
            ForwardDirection.y += JumpForce;
        }
		
    }

    ForwardDirection.y -= gravity * Time.deltaTime;
    charController.Move(ForwardDirection * (Time.deltaTime * RunSpeed));
}























function Update () {
}

var CharacterNum : int = 1;
var CurrentCharacter : GameObject;

var Character1 : GameObject;
var Character2 : GameObject;
var Character3 : GameObject;
var Character4 : GameObject;
var Character5 : GameObject;
var Character6 : GameObject;

function Start(){
	
	CurrentCharacter = Instantiate(Character1);

}

function OnGUI(){
	
	GUI.Label ( Rect( 200, 420, 500, 25), "Move with arrows andhold down space bar for action.");
	
	if (GUI.Button ( Rect( 10, 420, 150, 25), "Next Character")){
		
		
		if (CharacterNum == 1){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);
			CurrentCharacter = Instantiate(Character2);
			CharacterNum = 2;
			
			
		} else if (CharacterNum == 2){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);	
			CurrentCharacter = Instantiate(Character3);
			CharacterNum = 3;
			
			
		} else if (CharacterNum == 3){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);	
			CurrentCharacter = Instantiate(Character4);
			CharacterNum = 4;
			
		} else if (CharacterNum == 4){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);	
			CurrentCharacter = Instantiate(Character5);
			CharacterNum = 5;
			
		} else if (CharacterNum == 5){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);	
			CurrentCharacter = Instantiate(Character6);
			CharacterNum = 6;
			
		} else if (CharacterNum == 6){
			
			if (CurrentCharacter) Destroy(CurrentCharacter);	
			CurrentCharacter = Instantiate(Character1);
			CharacterNum = 1;
		}
	}
}
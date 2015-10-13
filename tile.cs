using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	Renderer rend;

	Vector2 playerPosition;
	public Vector2 tilePosition;

	bool chosen;
	bool current;
	bool inTarget;
	bool rightHold;
	[SerializeField]bool valid;

	[SerializeField] Color mouseOverColor;
	[SerializeField] Color chosenColor;
	[SerializeField] Color attackColor;	
	[SerializeField] Color invalidColor;
	[SerializeField] Color jumpColor;
	
	[SerializeField] bool mouseOn;

	[SerializeField] inputManager iManager;
	[SerializeField] statsManager dataBase;
	[SerializeField] functionManager SS;
	
	int weapon;
	player myPlayer;

	void Start() {
		// This part is necessary for any spawned prefab
		// This will change to "gameController(Clone)" if we decide to instantiate the gameController
		GameObject gameController = GameObject.Find ("gameController");
		iManager = gameController.GetComponent<inputManager> ();
		//tManager = gameController.GetComponent<turnManager> ();
		SS = gameController.GetComponent<functionManager> ();
		dataBase = gameController.GetComponent<statsManager> ();

		rend = GetComponent<Renderer>();
	}

	void OnMouseEnter() {
		if (!iManager.isCommandable())
						return;
		if (iManager.isInTargetMode() && iManager.hasAttackLeft()) {     // Target selection mode
	 		iManager.attackCommand(tilePosition);// Everytime the mouse enters another tile the attack position is refreshed
		}
		mouseOn = true;
	}
	
	void OnMouseExit() {  // Clears the state on previous tile
		mouseOn = false;
		rightHold = false;		
	}

	void OnMouseOver(){
		if (!iManager.isCommandable())
			return;
		if (Input.GetButton ("Fire2") && (current)) {   // Detect right MouseUp event manually
			rightHold = true;
		} else {
			if(rightHold){
				iManager.cancelCommand();   // Right click cancels one command at a time
				rightHold = false;
			}
		}
	}

	void OnMouseDown() {
		if (!iManager.isCommandable())
			return;
		if (current) {
			iManager.startTargeting(tilePosition);					// Enters targeting mode
		}
	}

  // The MouseUp event only works on left mouse button
	void OnMouseUp(){     // Mouseup implies a Mousedown action, which means the player clicked on the tile
		iManager.endTargeting();
		if (valid && mouseOn) {   // MouseUp always trigger on the tile where MouseDown happens!
			iManager.moveCommand(tilePosition);   // Left mouse click choses a new movement step
		}
	}

	void Update(){ 								//this part is what determines the display on each tile
    	if (!iManager.isCommandable())
			return;
		playerPosition = iManager.getPlayerPosition();	
			//in the case of weapons with recoil effect, fireposition!=playerposition

	    setState();
	    setAppearance();
	}
	
	void setState(){  // Sets the states of the current tile
	  	if (SS.isNear(playerPosition, tilePosition) && iManager.hasMoveLeft()) {
			valid = true;
		} else {
			valid = false;
		}
		// Check if the tile is in movement path
		if (tilePosition == playerPosition){  
			current = true;
			chosen = true;
		}else{
			current=false;
			if(iManager.inMovement(tilePosition)){ // Mark the trail with mouseover color
				chosen = true;
			}else{
				chosen = false;
			}
		}

		if (iManager.inTarget (tilePosition)) {
			inTarget = true;
		} else {
			inTarget=false;
		}
	}
	
	void setAppearance(){
		clear ();
	  	if (inTarget) {
			setTarget();
		}
		if (current) {
			setCurrent();
		}else if(chosen||(mouseOn&&valid)){
			setMouseOver();
		}
		// In target selection mode
		if (iManager.isInTargetMode()) {
		  int wpnId = iManager.getWeaponId();
		  //weapon wpn = myPlayer.getWeapon(wpnId);
		  //if (wpn.isInRange(SS.getDistance(tilePosition,playerPosition))){
		  //  setInvalid();
		  //}
		}
	}

	// We can do something fancy here in the future
	void setTarget(){
		rend.material.color = attackColor;
	}

	void setCurrent(){
		rend.material.color = chosenColor;
	}

	void setMouseOver(){
		rend.material.color = mouseOverColor;
	}

	void setInvalid(){
		rend.material.color = invalidColor;
	}

	void clear(){
		rend.material.color = Color.white;
	}
}

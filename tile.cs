using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	private Renderer rend;

	private Vector2 playerPosition;
	public Vector2 tilePosition;

	private bool chosen;
	private bool current;
	private bool rightHold;

	[SerializeField] Color mouseOverColor;
	[SerializeField] Color chosenColor;
	[SerializeField] Color attackColor;	
	[SerializeField] Color invaildColor;
	[SerializeField] Color jumpColor;

	[SerializeField] bool shootable;
	[SerializeField] bool mouseOn;

	[SerializeField] inputManager iManager;
	[SerializeField] statsManager dataBase;
	[SerializeField] functionManager SS;
	
	int weapon;
	playerAction player;

	void Start() {
		rend = GetComponent<Renderer>();

		//establish connection with other scripts
	}

	void OnMouseEnter() {
		if (!iManager.isCommandable)
						return;
		if (iManager.hold && shootable) {     // Target selection mode
	 		iManager.attackCommand(tilePosition);// Everytime the mouse enters another tile the attack position is refreshed
		}
		mouseOn = true;
	}
	
	void OnMouseExit() {  // Clears the state on previous tile
		mouseon = false;
		rightHold = false;		
	}

	void OnMouseOver(){
		if (!iManager.commandable)
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
		if (!iManager.commandable)
			return;

		if (current) {
			iManager.startHold(tilePosition);					// Enters targeting mode
		}
	}

  // The MouseUp event only works on left mouse button
	void OnMouseUp(){     // Mouseup implies a Mousedown action, which means the player clicked on the tile
		iManager.stopHold();
		if (valid && mouseOn) {   // MouseUp always trigger on the tile where MouseDown happens!
			iManager.moveCommand(tilePosition);   // Left mouse click choses a new movement step
		}
	}

	void Update(){ 								//this part is what determines the display on each tile
    if (!iManager.commandable)
			return;
		playerPosition = iManager.playerPosition;	
		//in the case of weapons with recoil effect, fireposition!=playerposition

    setState();
    setAppearance();
    
	}
	
	void setState(){  // Sets the states of the current tile
	  if (SS.near(playerPosition, tilePosition) && iManager.hasMoveLeft()) {
			valid = true;
		} else {
			valid = false;
		}
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
	}
	
	void setAppearance(){
	  if (iManager.inTarget(tilePosition)) {
			setTarget();
		}
		if (current) {
			setCurrent();
		}else if(chosen||(mouseon&&valid)){
			setMouseOver();
		}else{
			clear();
		}
		// In target selection mode
		if (gctrl.hold) {
		  wpnId = iManager.getWeapon();
		  weapon wpn=player.getWeapon(wpnId);
		  if (wpn.isInRange(SS.getDistance(tilePosition))){
		    setInvalid();
		  }
		}
	}
	void setTarget(){
	}
	void setCurrent(){
	}
	void setMouseOver(){
	}
	void setInvaild(){
	}
	void clear(){
	}
}

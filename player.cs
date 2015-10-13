using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {
	
	[SerializeField]inputManager iManager;
	[SerializeField]turnManager tManager;
	[SerializeField]functionManager SS;
	[SerializeField]statsManager dataBase;
	
	GameObject myPlayer;
	
	Vector2 playerPosition;
	int energy;
	int exp;
	int time;
	int turn;
	//LinkedList<action> actStack; 	// I don't know how it works, it keeps on giving me bugs!

  	[SerializeField] string playerName;
	[SerializeField] int playerIndex;

	void Start () {
		// This part is necessary for any spawned prefab
		// This will change to "gameController(Clone)" if we decide to instantiate the gameController
		GameObject gameController = GameObject.Find ("gameController");
		iManager = gameController.GetComponent<inputManager> ();
		tManager = gameController.GetComponent<turnManager> ();
		SS = gameController.GetComponent<functionManager> ();
		dataBase = gameController.GetComponent<statsManager> ();

    	myPlayer = this.gameObject;
    	time=-1;
    	turn=0;
    	//actStack = new LinkedList<action>();
	}
	
	void Update(){
		if (tManager.getTurn()==turn) return;
		if (tManager.getTime()==time) return;
		// Everything happens in between!
		while(turn < tManager.getTurn()){
			while(time < tManager.getTime() && time < 4){

				time++;
			}
			turn++;
		}
		// What about making a list of delegates?
		time=tManager.getTime();
		if (time==-1) turn=tManager.getTurn();
	}
	
	void prepareToMove (){
	  
	}
	
	void receiveDamage(){
		
	}
  	
  	IEnumerator moveStep(Vector2 target, float time){
		Vector3 startPos = SS.hexPositionTransform(playerPosition);
		Vector3 endPos =  SS.hexPositionTransform(target);
		float d = Vector3.Distance(startPos,endPos);
		
		float v = d/time * Time.fixedDeltaTime;
		int step = Mathf.FloorToInt(d/v)+1;
		float currTime = Time.time;
		for(int i = 0;i<step;i++){    
			myPlayer.transform.position = Vector3.MoveTowards(startPos,endPos,v);
			yield return new WaitForSeconds (Time.fixedDeltaTime);
		}
		//Debug.Log("Difference:");       // Trying to see if it works!
		//Debug.Log(time.Time-currTime-time);
	}

	public weapon getWeapon(int weaponId){
		return null;
	}
}

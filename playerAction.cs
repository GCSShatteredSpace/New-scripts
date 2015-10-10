using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerAction : MonoBehaviour {
	
	inputManager iManager;
	turnManager tManager;
	functionManager SS;
  	statsManager dataBase;
	
	GameObject player;
	
	Vector2 playerPosition;
	int energy;
	int exp;
	int time;

  	[SerializeField] string playerName;
	[SerializeField] int playerIndex;

	void Start () {
    	player = this.GameObject;
    	time=-1;
	}
	
	void Update(){
		if (tManager.getTurn()==turn) return;
		if (tManager.getTime()==time) return;
		// Everything happens in between!
		
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
		int step = Mathf.Floor(d/v)+1;
		currTime = Time.time;
		for(int i=0;i++;i<step){    
			player.transform.position = Vector3.MoveTowards(startPos,endPos,v);
			yield return new WaitForSeconds (Time.fixedDeltaTime);
		}
		Debug.Log("Difference:");       // Trying to see if it works!
		Debug.Log(time.Time-currTime-time);
	}
	
	void
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerAction : MonoBehaviour {
	
	inputManager iManager;
	functionManager SS;
  statsManager dataBase;
	
	GameObject player;
	
	Vector2 playerPosition;
	int energy;
	int exp;

  [SerializeField] string playerName;
	[SerializeField] int playerIndex;

	void Start () {
    player=this.GameObject
	}
	
	void prepareToMove (){
	  
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
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class turnManager : MonoBehaviour {
	
	int readyPlayers;
	int time;
	int turn;
	
	void getReady(){ 
		readyPlayers+=1;
	}
	
	int getReadyPlayers(){	// Let's keep it an int for now
		return readyPlayers;
	}
	
	void Update(){
		if (readyPlayers == PhotonNetwork.playerList.Length && !action) {
			SendNetworkMessage("Turn starts!");
			turn+=1;
			StartCoroutine(startTurn());
		}
	}
	
	IEnumerator startTurn(){
		time+=1;
		yield return new WaitForSeconds(stepTime);
	}
	
	void endTurn(){
		time=-1;
	}
	
	int getTime(){
		return time;
	}
	
	int getTurn(){
		return turn;
	}
	
	public list<Vector2>[] calculateTurnSequence(list<Vector2> movement1,list<Vector2> movement2){
		list<Vector2>[] velocitySequences= new list<Vector2>[2];
		for(int i=1; i<Math.Max(movement1.Count, movement2.Count); i++)
		{
			Vector2 vel1, vel2;
			bool recheck=false;
			if(i<movement1.Count) vel1=movement1[i]-movement1[i-1];  //If either player stops moving, add zero vectors to their velocity sequence 
			else vel1=Vector2.zero;
			if(i<movement2.Count) vel2=movement2[i]-movement2[i-1];
			else vel2=Vector2.zero;
			if(boardManager.isOccupied(movement1[i])){ 
				velocitySequences[0][i]=0-vel1;
				recheck=true;
			}  //If collides with obstacle, reverse velocity vector
			else velocitySequences[0][i]=vel1;
			if(boardManager.isOccupied(movement2[i])){
				velocitySequences[1][i]=0-vel2;
				recheck=true;
			}
			else velocitySequences[1][i]=vel2;
	
			movement1[i]=movement1[i-1]+vel1;  //update position vectors according to velocity vector
			movement2[i]=movement2[i-1]+vel2;
	
			if(movement1[i]==movement2[i]){  //If collides with other player, swap velocity vector
				Vector2 temp=velocitySequences[0][i];
				velocitySequences[0][i]=velocitySequences[1][i];
				velocitySequences[1][i]=temp;
				recheck=true;
			}	
			movement1[i]=movement1[i-1]+vel1;  //update position vectors according to velocity vector
			movement2[i]=movement2[i-1]+vel2;  //how should the remaining movement positions be updated?
			
			if(recheck) i--; //rechecks for collisions if changes were made
		}
		return velocitySequences;
	}

}

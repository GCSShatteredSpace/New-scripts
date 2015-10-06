getReady(): readyPlayers+=1;
getReadyPlayers(): return readyPlayers;
void Update(){
	if (readyPlayers == PhotonNetwork.playerList.Length && !action) {
		SendNetworkMessage("Turn starts!");
		turn+=1;
		StartCoroutine(startTurn());
	}
}
IEnumerator startTurn(){
	yield return new WaitForSeconds (stepTime);
	time+=1
}
endTurn(){
	time=-1
}
getTime(): return time;
getTurn(): return turn;

public list<Vector2>[] calculateTurnSequence(list<Vector2> movement1,list<Vector2> movement2)
{
	list<Vector2>[] velocitySequences= new list<Vector2>[2];
	for(int i=1; i<Math.Max(movement1.Count, movement2.Count); i++)
	{
		Vector2 vel1, vel2;
		if(i<movement1.Count) vel1=movement1[i]-movement1[i-1];
		else vel1=Vector2.zero;
		if(i<movement2.Count) vel2=movement2[i]-movement2[i-1];
		else vel2=Vector2.zero;
		if(boardManager.isOccupied(movement1[i])){
			velocitySequences[0][i]=0-vel1;}
		else{velocitySequences[0][i]=vel1;}
		if(boardManager.isOccupied(movement2[i])){
			velocitySequences[1][i]=0-vel2;}
		else{velocitySequences[1][i]=vel2;}
		if(movement1==movement2)
		{
			Vector2 temp=velocitySequences[0][i];
			velocitySequences[0][i]=velocitySequences[1][i];
			velocitySequences[1][i]=temp;
		}
		
	}

	return velocitySequences;
}
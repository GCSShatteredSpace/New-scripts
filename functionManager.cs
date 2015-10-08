using UnityEngine;
using System.Collections;
public class functionManager : MonoBehaviour {
	public Vector2[] direction;
	const float vertPart = 0.8666f; // sqrt(3)/2
	public int tileSize;
	[SerializeField] statsManager dataBase
	
	void Start () {
		//Debug.Log (getDistance (new Vector2 (2f, -1f), new Vector2 (0f, 0f)));
		//Debug.Log (getDistance (new Vector2 (3f, -2f), new Vector2 (2f, 0f)));
		//Debug.Log (getDistance (new Vector2 (-2f, 1f), new Vector2 (0f, 0f)));
		//Debug.Log (getDistance (new Vector2 (-2f, 1f), new Vector2 (1f, 0f)));
		//Debug.Log (isNear (new Vector2 (2f, 0f), new Vector2 (-1f, 1f)));
		//Debug.Log (isNear (new Vector2 (2f, 0f), new Vector2 (2f, -1f)));
		//Debug.Log (isOutOfBounds (new Vector2 (4f, -4f)));
		//Debug.Log (isOutOfBounds (new Vector2 (5f, -4f)));
		//Debug.Log (hexPositionTransform (new Vector2 (3f, -2f)));
		//Debug.Log (hexPositionTransform (new Vector2 (2f, 0f)));
		direction = new Vector2[6];
		Vector2 right = new Vector2 (1f, 0f);
		Vector2 left = new Vector2 (-1f, 0f);
		Vector2 up = new Vector2 (0f, 1f);
		Vector2 down = new Vector2 (0f, -1f);
		Vector2 forward = new Vector2 (1f, -1f);
		Vector2 back = new Vector2 (-1f, 1f);
		direction [0] = right;
		direction [1] = left;
		direction [2] = up;
		direction [3] = down;
		direction [4] = forward;
		direction [5] = back;
		tileSize = dataBase.tileSize; // This could vary according to our board
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Vector3 hexPositionTransform(Vector2 v){
		return new Vector3 (tileSize * (v.x + (0.5f * v.y)), tileSize * vertPart * v.y, 0f);
	}
	public bool isNear(Vector2 v1, Vector2 v2){
		return getDistance(v1,v2) == 1;
	}
	public int getDistance(Vector2 v1, Vector2 v2){
		int x2 = (int)(v2.x - v1.x);
		int y2 = (int)(v2.y - v1.y);
		int z = 0 - x2 - y2;
		int dist = Mathf.Abs(x2) + Mathf.Abs(y2) + Mathf.Abs(z);
		dist = dist/2;
		return dist;
	}
	
	public bool isOutOfBounds(Vector2 v){
		Vector2 origin = new Vector2(0f,0f);
		return getDistance(origin,v) <= 4;
	}
	
	public bool areAligned(Vector2 v){	// See if direction is along one of the six ones
		if(v.x==0||v.y==0||v.x==-v.y){
			return true;
		}else{
			return false;
		}
	}
	
	public Vector2 getNormal(Vector2 v){ //return the identity vector along the 6 directions if areAligned
		int x, y;
		if (v.x == 0) {
			x = 0;		
		} else if (v.x > 0) {
			x = 1;
		} else {
			x=-1;		
		}
		if (v.y == 0) {
			y = 0;		
		} else if (v.y > 0) {
			y = 1;
		} else {
			y =-1;		
		}
		return new Vector2 (x, y);
	}
	
}

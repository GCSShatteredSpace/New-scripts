using UnityEngine;
using System.Collections;
public class functionManager : MonoBehaviour {
	public Vector2[] direction;
	// Use this for initialization
	void Start () {
		//Debug.Log (getDistance (new Vector2 (2f, -1f), new Vector2 (0f, 0f)));
		//Debug.Log (getDistance (new Vector2 (3f, -2f), new Vector2 (2f, 0f)));
		//Debug.Log (getDistance (new Vector2 (-2f, 1f), new Vector2 (0f, 0f)));
		//Debug.Log (getDistance (new Vector2 (-2f, 1f), new Vector2 (1f, 0f)));
		//Debug.Log (isNear (new Vector2 (2f, 0f), new Vector2 (-1f, 1f)));
		//Debug.Log (isNear (new Vector2 (2f, 0f), new Vector2 (2f, -1f)));
		//Debug.Log (isOutOfBounds (new Vector2 (4f, -4f)));
		//Debug.Log (isOutOfBounds (new Vector2 (5f, -4f)));
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
	}
	
	// Update is called once per frame
	void Update () {
	
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

}

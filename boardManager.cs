using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


/*
 * boardManager - contains functions that manages the board
 */
public class boardManager : MonoBehaviour {

    public Vector2[] direction;
    const float vertPart = 0.8666f; // sqrt(3)/2
    public int tileSize;
    [SerializeField] float piecesize //I assume this is the diameter of the circle as well?
    [SerializeField] statsManager dataBase;
    [SerializeField] functionManager funcManager;
    [SerializeField] Vector2[] turretSpawnPoint = new Vector2[5];
    [SerializeField] Vector2[] barrierSpawnPoint = new Vector2[12]; //barrier numbers may vary

    void Start(){
        tileSize = dataBase.tileSize;
    }

    /*
     * Copied from http://answers.unity3d.com/questions/62644/distance-between-a-ray-and-a-point.html
     * Calculates the distance between a point and a line
     */
    public float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd){
        return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
    }
    public Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd){
        Vector3 rhs = point - lineStart;
        Vector3 vector2 = lineEnd - lineStart;
        float magnitude = vector2.magnitude;
        Vector3 lhs = vector2;
        if (magnitude > 1E-06f)
        {
            lhs = (Vector3)(lhs / magnitude);
        }
        float num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
        return (lineStart + ((Vector3)(lhs * num2)));
    }

    /*
     * Returns whether the projectile is blocked by barriers
     */
    public bool isBlocked(Vector2 firePosition, Vection2 targetPosition){
        int count=0;
        Vector3 firePos = funcManager.hexPositionTransform(firePosition);
        Vector3 targetPos = funcManager.hexPositionTransform(targetPosition);
        Vector3 barrierPos;
        float dis;
        for (int i=0;i<barrierSpawnPoint.Length;i++){
            barrierPos = funcManager.hexPositionTransform(barrierSpawnPoint[i]);
            if ((firePos.x<=barrierPos.x) && 
                (targetPos.x>=barrierPos.x) &&
                (firePos.y<=barrierPos.y) && 
                (targetPos.y>=barrierPos.y)){ //make sure the barrier is inside the parallelogram
                dis = DistancePointLine(barrierPos, firePos,targetPos);
                if (dis<piecesize/2){
                    return true; //the projectile cut through one 
                }
                else if (dis == piecesize/2){
                    count++;
                }
            }
        if (count>1){ //the projectile cuts through the tangent of multiple barriers
                      //actually this idea is bugged in some barrier formations
            return true;
        }
        return false;
    }

    /*
     * Returns whether the tile is occupied by barriers or turrets
     */
    public bool isOccupied(Vector2 pos){
        for (int i=0;i<turretSpawnPoint.Length;i++){
            if (pos.x == turretSpawnPoint[i].x && pos.y == turretSpawnPoint[i].y){
                return true;
            }
        }
        for (int i=0;i<barrierSpawnPoint.Length;i++){
            if (pos.x == barrierSpawnPoint[i].x && pos.y == barrierSpawnPoint[i].y){
                return true;
            }
        }
        return false;
    }

    /*
     * Check if pos is in range of any turrets(statsManager.turretRange)
     */
    public bool isDangerous(Vector2 pos){
        int range = dataBase.turretRange;
        float turretX;
        float turretY;
        for (int i=0;i<turretSpawnPoint.Length;i++){
            turretX = turretSpawnPoint[i].x;
            turretY = turretSpawnPoint[i].y;
            if (pos.x >= turretX-range && 
                pos.x <= turretX+range && 
                pos.y >= turretY-range &&
                pos.y <= turretY+range){
                return true;
            }
        }
        return false;        
    }
}
/**
File Created May 12th 2017 - File name = ShootingRangeMovingTarget.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

public class ShootingRangeMovingTarget : MonoBehaviour {
    //far left is z = 12.56f
    //far right is z = 2.07f
    //those values are relative to the ShootingRange GameObject

    private enum Direction { RIGHT, LEFT };
    private Direction state;
    private float randomizationTimer;
    private float speed;
    private float farLeft, farRight;

    /// <summary>
    /// Randomizes direction using random value between 0 and 1
    /// </summary>
    void RandomizeDirection() {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            state = Direction.RIGHT;
        else
            state = Direction.LEFT;
    }

    /// <summary>
    /// Initializes variables values and calls the first RandomizeDirection
    /// to set an initial direction to the moving targets
    /// </summary>
	void Start () {
        speed = 5.0f;
        farLeft = 12.56f;
        farRight = 2.07f;
        randomizationTimer = 5.0f;
        RandomizeDirection();
	}
	
    /// <summary>
    /// Update method, runs every frame, handles random direction change timer.
    /// checks for when the target needs to start moving the other direction because it reached a side bound.
    /// Handles movement of the moving target based on enum state.
    /// </summary>
	void Update () {
        randomizationTimer -= Time.deltaTime;
        //random direction change timer
        if(randomizationTimer < 0.0f) {
            RandomizeDirection();
            randomizationTimer = 5.0f;
        }
        //checks far left bound
        if (transform.position.z > farLeft)
            state = Direction.LEFT;
        //checks far right bound
        else if (transform.position.z < farRight)
            state = Direction.RIGHT;
        //selects appropriate movement based on state.
        if (state == Direction.RIGHT) 
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        else if (state == Direction.LEFT)
            transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
}
//IDEA:
//Target despawns when hit and respawns after a short delay? use same technique as wandering npc of hiding mesh to keep proper moving position

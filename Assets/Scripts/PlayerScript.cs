/**
File Created May 22nd 2017 - File name = PlayerScript.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float speed, jumpSpeed, gravitySpeed;
    private CharacterController CC;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    GameObject bullet, casing;
    
    /// <summary>
    /// Start function, initializes variables
    /// </summary>
	void Start () {
        CC = GetComponent<CharacterController>();
        speed = 9.0f;
        jumpSpeed = 10.0f;
        gravitySpeed = 30.0f;
	}

    /// <summary>
    /// Update function, runs every frame, handles all directional movement as well as jumping and gravity.
    /// </summary>
    void Update () {
        //Fire Button is here, locks cursor if it is not already locked.
        if (Input.GetButton("Fire")) {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
        }
        //Calculating character movement based on input axis
        if (CC.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Transforming to world space
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            }
        }
        //Gravity
        moveDirection.y -= gravitySpeed * Time.deltaTime;
        CC.Move(moveDirection * Time.deltaTime);
    }
}

//BUGS:
//movement considers mouse orientation when calculating forward direction.
//no air strafing
//raise barriers on bridge etc?


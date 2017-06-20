/**
File Created May 22nd 2017 - File name = PlayerScript.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float speed, jumpSpeed, gravitySpeed, fireTimer, fireRate;
    private CharacterController CC;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    GameObject bullet, casing, shootingPoint, casingPoint;
    
    /// <summary>
    /// Start function, initializes variables
    /// </summary>
	void Start () {
        CC = GetComponent<CharacterController>();
        speed = 9.0f;
        jumpSpeed = 10.0f;
        gravitySpeed = 30.0f;
        fireTimer = fireRate = 0.25f;
	}

    /// <summary>
    /// Update function, runs every frame, handles all directional movement as well as jumping and gravity.
    /// </summary>
    void Update () {
        //Fire Button is here, locks cursor if it is not already locked.
        if (Input.GetButton("Fire") && fireTimer < 0) {
            if (Cursor.lockState == CursorLockMode.None) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            //ray cast @ cursor
            //get point to point angle?
            //aim bullet using ^ angle
            //add force
            //bullet script
            //casing
            Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity); //wrong orientation
            fireTimer = fireRate;
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
        fireTimer -= Time.deltaTime;
    }
}

//BUGS:
//movement considers mouse orientation when calculating forward direction.
//no air strafing
//raise barriers on bridge etc?


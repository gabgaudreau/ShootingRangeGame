/**
File Created May 22nd 2017 - File name = PlayerScript.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float speed, jumpSpeed, gravitySpeed;
    private CharacterController CC;
    private Vector3 moveDirection = Vector3.zero;

	void Start () {
        CC = GetComponent<CharacterController>();
        speed = 16.0f;
        jumpSpeed = 15.0f;
        gravitySpeed = 40.0f;
	}
	
	void Update () {
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

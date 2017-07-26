/**
File Created May 22nd 2017 - File name = PlayerScript.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private float speed, jumpSpeed, gravitySpeed, fireTimer, fireRate, weaponRange;
    private CharacterController CC;
    private Vector3 moveDirection = Vector3.zero;
    private WaitForSeconds duration = new WaitForSeconds(0.05f);
    private LineRenderer laserLine;
    private AudioSource gunSound;
    private Vector3 rayOrigin;
    [SerializeField]
    GameObject shootingPoint, bullet;
    
    /// <summary>
    /// Start function, initializes variables
    /// </summary>
	void Start () {
        CC = GetComponent<CharacterController>();
        laserLine = GetComponent<LineRenderer>();
        gunSound = GetComponent<AudioSource>();
        speed = 9.0f;
        jumpSpeed = 10.0f;
        gravitySpeed = 30.0f;
        fireTimer = fireRate = 0.25f;
        weaponRange = 150.0f;
        rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    }

    /// <summary>
    /// Shooting effect duration
    /// </summary>
    /// <returns>Returns waitforseconds</returns>
    private IEnumerator ShotEffect() {
        gunSound.Play();
        laserLine.enabled = true;
        yield return duration;
        laserLine.enabled = false;
    }

    /// <summary>
    /// Update function, runs every frame, handles all directional movement as well as jumping and gravity.
    /// </summary>
    void Update () {
        //Origin of the ray, center of camera (0.5, 0.5)
        rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        //Sets points to determine the line of the laser to be drawn
        laserLine.SetPosition(0, shootingPoint.transform.position);
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit)) {
            laserLine.SetPosition(1, hit.point);
        }
        else { //If nothing is hit by the ray, just show the full distance of the ray
            laserLine.SetPosition(1, rayOrigin + (Camera.main.transform.forward * weaponRange));
        }
        laserLine.enabled = true;
        //Fire Button is here, locks cursor if it is not already locked.
        if (Input.GetButton("Fire") && fireTimer < 0) {
            //Lock the cursor back in the event that it isn't (i.e. used menus)
            if (Cursor.lockState == CursorLockMode.None) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            //Debug.Log(hit.collider.name);
            Instantiate(bullet, shootingPoint.transform.position, Camera.main.transform.rotation);
            gunSound.Play();
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
//character collision is wonky, going halfway through walls
//movement considers mouse orientation when calculating forward direction.
//no air strafing
//raise barriers on bridge etc?


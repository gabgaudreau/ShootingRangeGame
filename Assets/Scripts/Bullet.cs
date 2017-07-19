/**
File Created June 19th 2017 - File name = Bullet.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float force;
    private WaitForSeconds destroyTimer = new WaitForSeconds(6.0f);
    private bool _hit;
    private Rigidbody rb;
    /// <summary>
    /// 
    /// </summary>
    void Start() {
        _hit = false;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        StartCoroutine(Destroy());
    }

    /// <summary>
    /// 
    /// </summary>
    void Update() {
        //looking ahead to the next frame to see if a collision is incoming
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (transform.position - transform.position + (transform.forward * (rb.velocity.magnitude * Time.deltaTime))).normalized, out hit) && !_hit) {
            _hit = true;
            transform.position = hit.point; //temp fix
            //problem for another day, all info needed is in the hit variable
            //apply physics???
        }
    }

    /// <summary>
    /// Destroys the bullet after x amount of time.
    /// </summary>
    /// <returns>Returns WaitforSeconds</returns>
    IEnumerator Destroy() {
        yield return destroyTimer;
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles bullet collision, sets gravity and mass for realistic look.
    /// </summary>
    void HandleBulletCollision() {
        if (!_hit)
            _hit = true;
        rb.useGravity = true;
        rb.mass = 10000;
    }

    /// <summary>
    /// Collision function.
    /// </summary>
    /// <param name="col">col is the object that the bullet collides with.</param>
    void OnCollisionEnter(Collision col) {
        HandleBulletCollision();
    }
}

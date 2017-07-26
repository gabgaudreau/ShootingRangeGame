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
    private bool _once, _hit; //Keeps a bool on if the bullet has already hit something or not
    private Rigidbody rb;

    /// <summary>
    /// 
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        StartCoroutine(Destroy());
    }

    /// <summary>
    /// 
    /// </summary>
    void Update() {
        //looking ahead to the next frame to see if a collision is incoming
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, (transform.position - transform.position + (transform.forward * (rb.velocity.magnitude * Time.deltaTime))).normalized, out hit) && !_hit) {
        //    _hit = true;
        //    transform.position = hit.point;
        //}
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
        if(col.gameObject.layer == LayerMask.NameToLayer("Shootable") && !_once) {
            _once = true;
            //Using IShootable interface to send a call to any object hit that has the Shootable layer.
            IShootable shootable = col.gameObject.GetComponentInParent<IShootable>();
            if(shootable != null) {
                shootable.GotShot();
            }
        }
    }
}

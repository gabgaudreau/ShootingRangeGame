/**
File Created June 26th 2017 - File name = ShootingTarget.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable {
    /// <summary>
    /// Method from the IShootable interface, called when the target is hit by a projectile.
    /// </summary>
    /// <param name="objectHit">name of the object hit.</param>
    public void GotShot(string objectHit) {
        Debug.Log(objectHit);
        Debug.Log("target got shot");
    }
}

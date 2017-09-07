/**
File Created Sept 7th 2017 - File name = Destroy.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class Destroy : MonoBehaviour {
    /// <summary>
    /// Destroys the object after 10 sec.
    /// </summary>
    void Start () {
        Destroy(gameObject, 10.0f);
	}
}

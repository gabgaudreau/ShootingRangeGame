/**
File Created June 26th 2017 - File name = ShootingTarget.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable {

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void GotShot(string objectHit) {
        Debug.Log(objectHit);
        Debug.Log("target got shot");
    }
}

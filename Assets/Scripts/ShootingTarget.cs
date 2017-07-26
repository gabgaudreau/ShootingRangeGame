/**
File Created June 26th 2017 - File name = ShootingTarget.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable {

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void GotShot() {
        Debug.Log("target got shot");
    }
}

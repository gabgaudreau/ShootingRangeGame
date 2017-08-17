﻿/**
File Created June 26th 2017 - File name = ShootingTarget.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable {
    private float hp, middleDMG, middlePlusOneDMG, middlePlusTwoDMG, middlePlusThreeDMG, middlePlusFourDMG, middlePlusFiveDMG;

    /// <summary>
    /// Start function, initializes variables.
    /// </summary>
    void Start() {
        hp = 100;
        middleDMG = 100;
        middlePlusOneDMG = 85;
        middlePlusTwoDMG = 65;
        middlePlusThreeDMG = 50;
        middlePlusFourDMG = 30;
        middlePlusFiveDMG = 15;
    }

    /// <summary>
    /// Method from the IShootable interface, called when the target is hit by a projectile.
    /// Will assign damage and score based on the object hit by the bullet.
    /// </summary>
    /// <param name="objectHit">name of the object hit.</param>
    public void GotShot(string objectHit) {
        if(objectHit == "MiddlePlus5") {
            hp -= middlePlusFiveDMG;
            GameManager.gm.AddScore(middlePlusFiveDMG);
        }
        else if(objectHit == "MiddlePlus4") {
            hp -= middlePlusFourDMG;
            GameManager.gm.AddScore(middlePlusFourDMG);
        }
        else if (objectHit == "MiddlePlus3") {
            hp -= middlePlusThreeDMG;
            GameManager.gm.AddScore(middlePlusThreeDMG);
        }
        else if (objectHit == "MiddlePlus2") {
            hp -= middlePlusTwoDMG;
            GameManager.gm.AddScore(middlePlusTwoDMG);
        }
        else if (objectHit == "MiddlePlus1") {
            hp -= middlePlusOneDMG;
            GameManager.gm.AddScore(middlePlusOneDMG);
        }
        else if (objectHit == "Middle") {
            hp -= middleDMG;
            GameManager.gm.AddScore(middleDMG);
        }
    }
}

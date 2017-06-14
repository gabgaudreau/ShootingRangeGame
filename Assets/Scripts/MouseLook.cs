/**
File Created May 26th 2017 - File name = MouseLook.cs
Author: Gabriel Gaudreau
Project: The1v1Game
**This script is based on an answer found in Unity3d Forums.**
*/
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

    public enum Axes { XANDY, X, Y}
    public Axes axes = Axes.XANDY;
    public float sensitivityX, sensitivityY;
    private float maxX, minX, maxY, minY, rotationY;

    /// <summary>
    /// Start function, initializes variables
    /// </summary>
    void Start() {
        sensitivityX = 1.2f;
        sensitivityY = 1.2f;
        minX = -360.0f;
        maxX = 360.0f;
        minY = -60.0f;
        maxY = 60.0f;
        rotationY = 0.0f;
    }

    /// <summary>
    /// Update function, runs every frame, handles mouse delta inputs and converts them into rotation for the camera and player gameobjects
    /// </summary>
    void Update() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            //this if is only in the event that inside the inspector, I decide to only rotate one object, on both axes.
            if (axes == Axes.XANDY) {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minY, maxY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            //These next 2 elses work separately, X moves the body horizontally 
            else if (axes == Axes.X) {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            //This else (Y) moves the head only, makes it feel more realistic.
            else {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minY, maxY);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
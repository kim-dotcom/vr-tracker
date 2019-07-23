using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationLimiter : MonoBehaviour {
    public float minXRotation;
    public float maxXRotation;

    Vector3 currentRotation = Vector3.zero;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        currentRotation = transform.localRotation.eulerAngles;

        if (currentRotation.x > minXRotation && currentRotation.x < 90)
        {
            transform.rotation = Quaternion.Euler(minXRotation-1, currentRotation.y, 0.0f); ;
        }
        if (currentRotation.x > 270 && currentRotation.x < maxXRotation)
        {
            transform.rotation = Quaternion.Euler(maxXRotation+1, currentRotation.y, 0.0f); ;
        }
    }
}

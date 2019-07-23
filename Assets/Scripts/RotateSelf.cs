using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

    public float rotationXAxis = 2f;
    public float rotationYAxis = 0f;
    public float rotationZAxis = 0f;
    public bool rotateGlobal = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateGlobal)
        {
            transform.Rotate(rotationXAxis * Time.deltaTime,
                             rotationYAxis * Time.deltaTime,
                             rotationZAxis * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(rotationXAxis * Time.deltaTime,
                             rotationYAxis * Time.deltaTime,
                             rotationZAxis * Time.deltaTime, Space.Self);
        }
        
    }
}

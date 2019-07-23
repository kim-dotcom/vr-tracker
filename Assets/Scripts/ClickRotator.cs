using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRotator : MonoBehaviour {
    //rotation toggles/speed
    public bool horizontalRotation = true;
    public bool verticalRotation;
    [Range (50,1000)]
    public float rotationSpeed = 100f;

    //if reverts to origin after said time
    public bool returnToOrigin = true;
    public float returnInSeconds = 30f;
    private Quaternion originalRotation;
    private Quaternion customRotation;
    private float returnTimer;
    private bool returnCountdown;
    private bool transitionOverride;

    void Start ()
    {
        originalRotation = transform.rotation;
    }

    //drag the item
    void OnMouseDrag()
    {        
        if (!transitionOverride)
        {
            returnTimer = 0f;
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            if (horizontalRotation)
            {
                transform.Rotate(Vector3.up, -rotationX, Space.World);
            }
            if (verticalRotation)
            {
                transform.Rotate(Vector3.right, rotationY, Space.World);
            }
        }
    }

    //start the reset count on mouse release
    void OnMouseUp()
    {
        if (!transitionOverride && returnToOrigin && !Input.GetMouseButton(0))
        {
            returnTimer = 0f;
            customRotation = transform.rotation;
            returnCountdown = true;
        }        
    }

    void Update()
    {
        //count and return the object
        if (returnCountdown)
        {
            returnTimer += Time.deltaTime;
            //before rotating back, lock user input
            if (returnTimer >= returnInSeconds)
            {
                transitionOverride = true;
            }
            //if countdown is up, reset rotation
            if (transitionOverride)
            { 
                transform.rotation = Quaternion.Slerp(customRotation, originalRotation, (returnTimer - returnInSeconds));
                if ((transform.rotation == originalRotation) || (returnTimer > (returnInSeconds + 3)))
                //the 3-second transition timeout is a dirty fix for this sometimes getting stuck
                {
                    returnCountdown = false;
                    transitionOverride = false;
                    returnTimer = 0f;
                }
            }
        }
    }
}

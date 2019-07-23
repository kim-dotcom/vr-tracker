using UnityEngine;
using System.Collections;

public class ClickRotatorRigidbody : MonoBehaviour {
	public float rotTorque;
    private Rigidbody rb;
	
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	bool mouseButtonDown;
	Vector3 prevTouchPoint;
	
	void FixedUpdate () {
		if (Input.GetMouseButton(0)) {
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(mouseRay, out hit)) {
			//	Get the direction of the clicked point from the object's centre.
				Vector3 currTouchPoint = (hit.point - transform.position).normalized;
			
			//	Cross product gives a vector perpendicular to the directions
			//	of the last two clicks, with length proportional to the sine
			//	of the angle between them. Amazingly, this is all the information
			//	we need to apply the right amount of torque!
				Vector3 axis = Vector3.Cross(prevTouchPoint, currTouchPoint);
				rb.AddTorque(axis * rotTorque);
			
			//	Remember the point where the click occurred and note that
			//	the mouse button is still down.
				prevTouchPoint = currTouchPoint;
				mouseButtonDown = true;
			}
		} else {
		//	Forget the last clicked point direction - it is meaningless after
		//	dragging has stopped.
			mouseButtonDown = false;
			prevTouchPoint = Vector3.zero;
		}
	}
}

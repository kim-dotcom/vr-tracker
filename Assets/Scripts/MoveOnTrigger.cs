using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour {



	public GameObject ReferenceObject; // The object that will activate the movement
	public GameObject MovingObject; // The object that will be moved
	public float Proximity; // Distance between ReferenceObject and MovingObject, at which the MovingObject will start moving
	public float Speed; // Speed at which the MovingObject will be moving
	public float MoveToX; // X coordinate to which the MovingObject will move
	public float MoveToY; // Y coordinate to which the MovingObject will move
	public float MoveToZ; // Z coordinate to which the MovingObject will move

	private float ActiveDistance; // To calculate the distance between ReferenceObject and MovingObject on each frame
	private Vector3 startPos; // To get the starting position of MovingObject



	void Awake() {
		startPos = transform.position; // This gets the starting position of MovingObject
	}

	void Update () {
		Vector3 movePos = new Vector3(MoveToX, MoveToY, MoveToZ); // This creates a new 3D vector with coordinate values the user has entered
		ActiveDistance = Vector3.Distance (ReferenceObject.transform.position, MovingObject.transform.position); // This calculates the distance between the two objects
		if (ActiveDistance < Proximity) { // If the distance between the ReferenceObject and the MovingObject is lower than the set proximity, the MovingObject will start to move towards the set coordinates
			transform.position = Vector3.MoveTowards (MovingObject.transform.position, movePos, Speed * Time.deltaTime);
		} else { // If the distance between the ReferenceObject and the MovingObject is greater than the set proximity, the MovingObject will start to return to it's starting position
			transform.position = Vector3.MoveTowards (MovingObject.transform.position, startPos, Speed * Time.deltaTime);
		}
	}
}

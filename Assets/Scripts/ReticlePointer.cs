using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))] // (Main)Camera is required for this script to work
public class ReticlePointer : MonoBehaviour {
	private GameObject hitObject = null; // Base value (object has been/has not been hit)
	private Vector3 reticlePosition = Vector3.zero; // Setting a center (base value) of 3D space coordinates 
	private Camera mcamera; // To get mainCamera
	public float Distance = 10f; // Distance to which the ray is cast (changeable by user)
	public GameObject Reticle; // Input your custom reticle



	void Awake() {
		mcamera = GetComponent<Camera>(); // To get and inicialize mainCamera
	}
		
	void Update () {
		reticlePosition =  Reticle.transform.position; // Get reticle's 3D position in space 


		Ray ray = mcamera.ScreenPointToRay(reticlePosition); // Set reticle to cast a ray
		RaycastHit hit; // To get info back from the raycast

	
		if (Physics.Raycast(ray, out hit, Distance)) {
			if (hitObject != hit.transform.gameObject) {
				if (hitObject != null) {
					hitObject.SendMessage("OnReticleExit", SendMessageOptions.DontRequireReceiver); 
				}
				hitObject = hit.transform.gameObject;
				hitObject.SendMessage("OnReticleEnter", SendMessageOptions.DontRequireReceiver); 
			} else {
				hitObject.SendMessage("OnReticleHover", SendMessageOptions.DontRequireReceiver); 

			}
		} else {
			if (hitObject != null) {
				hitObject.SendMessage("OnReticleExit", SendMessageOptions.DontRequireReceiver); 
			}
			hitObject = null;
		}
	}
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIText : MonoBehaviour {

	public GameObject Visible; // Set an object that will become "visible" when the designated key is pressed
	public GameObject Invisible; // Set an object that will become "invisible" when the designated key is pressed
	public KeyCode ActivationKey; // Set the activation key
	private bool hasButtonBeenPressed; // Button press bool switch
	

	void Update () {
		if (Input.GetKeyDown (ActivationKey)) { // If the key is pressed
			hasButtonBeenPressed = !hasButtonBeenPressed; // Bool - pressed/not pressed

			if (!hasButtonBeenPressed) { // If the key hasn't been pressed
				Visible.SetActive(true); // Visible object will activate
				Invisible.SetActive(false); // Invisible object will deactivate
		
			} else { // If the key has been pressed
				Visible.SetActive(false); // Visible object will deactvivate
				Invisible.SetActive(true); // Invisible object will activate
			}
				
		}
	}
}

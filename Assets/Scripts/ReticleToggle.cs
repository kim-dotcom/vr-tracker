using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleToggle : MonoBehaviour {

	Image img;
	public Sprite myFirstImage; // Changeable img (starting/first img)
	public Sprite mySecondImage; // Changeable img (img you want to display when first toggled)
	public KeyCode ActivationKey; // Set the activation key
	private bool hasButtonBeenPressed; // Button press bool switch



	void Start () 
	{
		img = this.gameObject.GetComponent<Image> (); // Get the "image component" of the target object (in this case the reticle's)
	}

	void Update ()
	{

		if (Input.GetKeyDown (ActivationKey)) { // If the key is pressed
			hasButtonBeenPressed = !hasButtonBeenPressed; // Bool - pressed/not pressed

			if (hasButtonBeenPressed) {
				img.sprite = mySecondImage; // If true, the image changes (in this case is "made visible")
			} else {
				img.sprite = myFirstImage; // If false, the image changes back to it's "invisible" form
			}
		}
	}
}
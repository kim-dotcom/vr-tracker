using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CursorToggle
{
public class CursorToggle : MonoBehaviour 

{

	private bool isCursorLocked; // Cursor lock bool switch
	public KeyCode ActivationKey; // Set the activation key

	void Start () 
	{
			ToggleCursorState();
	}
	
	void Update() 
	{
		CheckForInput (); // Check if the defined key has been pressed
		CheckIfCursorShoudlBeLocked (); //Check if the cursor should be locked according to bool of "isCursorLocked"
		}
		void ToggleCursorState() // Bool toggle of cursor
		{
			isCursorLocked = !isCursorLocked; // Bool - locked/not locked
		}

		void CheckForInput()
		{
			if (Input.GetKeyDown(ActivationKey))  // If the key is pressed
			{
				ToggleCursorState(); // Change the bool toggle of cursor state
			}
		}

		void CheckIfCursorShoudlBeLocked()
		{
			if (isCursorLocked) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false; // If cursor should be locked, then make it invisible
			} 

			else 
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true; // If cursor shouldn't be locked, then make it visible
			}
		}
	}
}

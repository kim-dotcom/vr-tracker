using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour {


	public GameObject VisibleMap; // Set the map that should be set to active when the object is clicked
	public GameObject[] InvisibleMaps; // Set the map/s that should be set to inactive when the object is clicked
	

	public void OnReticleEnter()
	{	
		
	}
	public void OnReticleExit()
	{	
		
	}
	public void OnReticleHover()
	{
		if (Input.GetMouseButtonDown(0)) { // When the left mouse button is clicked
				VisibleMap.SetActive (true); // Object in VisibleMap is set to active 

			for (var i = 0; i < InvisibleMaps.Length; i++) {
				InvisibleMaps[i].SetActive (false); // Object/s in InvisibleMaps is/are set to inactive

			}
		}
	}
}

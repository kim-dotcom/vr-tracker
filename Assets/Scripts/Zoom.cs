using UnityEngine;

public class Zoom : MonoBehaviour {

	public float zoomSpeed; // Speed at which the FOV will be widened/narrowed
	public float fovMin; // Minimum FOV that can be achieved ("maximal zoom")
	public float fovMax = 60; // Maximum FOV that can be achieved ("minimal zoom" - 60 is default)
    private float defaultFOV;


	void Start()
	{
        defaultFOV = Camera.main.fieldOfView;
	}

    void LateUpdate()
    {
        float scrollwheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollwheel > 0) // If you scroll "forward", the camera will zoom in by the set speed till the zoomMax value is reached 
        {
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 1;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, fovMin, fovMax);
        }
        if (scrollwheel < 0) // If you scroll "backward", the camera will zoom out by the set speed till the zoomMin value is reached
        {
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 1;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, fovMin, fovMax);
        }

        if (Input.GetMouseButtonDown(2)) Camera.main.fieldOfView = defaultFOV;

    }
}
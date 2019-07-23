using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterCube : MonoBehaviour {

    public Camera raycastCamera;
    public float objectScale = 0.025f;
    public Color objectColor;
    public bool visibleCursor;
    public float logFrameDelay = 5;

    private int itemIterator = 0;
    private string iteratedCubeName;
    private int frameCounter;

	// Use this for initialization
	void Start () {
        Cursor.visible = visibleCursor;
    }

    void Update()
    {
        frameCounter++;
        if (frameCounter % logFrameDelay == 0)
        {
            frameCounter = 0;
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit))
                    return;

                GameObject hitObject = hit.collider.gameObject;
                iteratedCubeName = "cube" + itemIterator.ToString();
                itemIterator++;
                //Debug.Log("Hit " + hitObject.name, hit.collider.gameObject);

                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = iteratedCubeName;
                cube.GetComponent<Renderer>().material.color = objectColor;
                cube.GetComponent<Renderer>().transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                cube.GetComponent<Collider>().enabled = false; //so that they don't stack on each other
                cube.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; //not needed
                cube.GetComponent<Renderer>().receiveShadows = false; //not needed
                cube.transform.parent = hitObject.transform;
                cube.transform.position = hit.point;
                cube.tag = "Respawn"; //so that the script to recompute finds this
            }
        }        
    }
}
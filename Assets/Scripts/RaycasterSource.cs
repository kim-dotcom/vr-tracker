using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterSource : MonoBehaviour {
    public enum RayObjects { cube, sphere };
    public enum RayTypes { mouse, PupilLabs, HTC };

    public Camera raycastCamera;

    [Space(10)]    
    public RayObjects objectType;
    public float objectScale = 0.025f;
    public Color objectColor;
    public bool objectsVisibleOnRaycast;

    [Space(10)]
    public RayTypes raycasterType;    
    public float logFrameDelay = 5;
    public bool visibleCursor;

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
            //Raycaster: mouse (dummy, no HMD)
            if (raycasterType == RayTypes.mouse && Input.GetMouseButton(1))
            {
                RaycastHit hit;
                var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit))
                    return;

                GameObject hitObject = hit.collider.gameObject;
                iteratedCubeName = "cube" + itemIterator.ToString();
                itemIterator++;
                //Debug.Log("Hit " + hitObject.name, hit.collider.gameObject);

                GameObject cube;
                if (objectType == RayObjects.cube)
                {
                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                }
                else
                {
                    cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                placeFixation(cube, hit, hitObject);            
            }
            //Raycaster: PupilLabs
            else if (raycasterType == RayTypes.PupilLabs)
            {
               //...
            }
            //Raycaster: HTC (TBD)
            else if (raycasterType == RayTypes.HTC)
            {
                //...
            }
        }        
    }

    void placeFixation (GameObject cube, RaycastHit hit, GameObject hitObject)
    {
        cube.name = iteratedCubeName;
        cube.GetComponent<Renderer>().material.color = objectColor;
        cube.GetComponent<Renderer>().transform.localScale = new Vector3(objectScale, objectScale, objectScale);
        cube.GetComponent<Collider>().enabled = false; //so that they don't stack on each other
        cube.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; //not needed
        cube.GetComponent<Renderer>().receiveShadows = false; //not needed
        cube.transform.parent = hitObject.transform;
        cube.transform.position = hit.point;
        cube.tag = "Respawn"; //so that the script to recompute finds this
        cube.GetComponent<MeshRenderer>().enabled = objectsVisibleOnRaycast;
    }
}
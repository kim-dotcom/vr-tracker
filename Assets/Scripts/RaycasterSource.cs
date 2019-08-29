using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycasterSource : MonoBehaviour {
    public enum RayObjects { cube, sphere };
    public enum RayTypes { mouse, PupilLabs, HTC };

    //the (player)camera - raycast origin
    public Camera raycastCamera;
    [Space(10)]

    //how are they eye fixations visualized?
    public RayObjects objectType;
    public float objectScale = 0.025f;
    public Color objectColor;
    public bool visibleOnCast;
    [Space(10)]

    //raycast type - to allow for multiple devices/implementations
    public RayTypes raycasterType;    
    public float logFrameDelay = 5;
    public bool visibleCursor;

    //serivce variables - fixation count, fixation object naming
    private int itemIterator = 0;
    private string iteratedCubeName;
    private int frameCounter;
    [Space(10)]

    //output variables
    public bool logToFile;
    [Space(5)]

    // for Pupil ET alone
    [Header("PupilLabs Settings")]
    public string calibrationSCName;
    public KeyCode rayCastKey;
    private GameObject getGaze;
    static bool calibrationLoaded = false;
    private bool isRaycasting;

	// Use this for initialization
	void Start ()
    {
        if (raycasterType == RayTypes.mouse)
        {
            Cursor.visible = visibleCursor;
        }
        else if (raycasterType == RayTypes.PupilLabs)
        {
            if (calibrationLoaded == false)
            {
                calibrationLoaded = true;
                SceneManager.LoadScene(calibrationSCName);

            }
            else if (calibrationLoaded == true)
            {
                getGaze = GameObject.Find("Gaze_3D");
            }

        }
        else if (raycasterType == RayTypes.HTC)
        {
            //...
        }
    }
     
    void Update()
    {
        // Turning PupilLabs raycasting ON/OFF (outside of the FrameDelay)
        if (Input.GetKeyDown(rayCastKey))
        {
            isRaycasting = !isRaycasting;
        }

        //process this every n-th frame (for performance constraints / data filtration)
        frameCounter++;
        if (frameCounter % logFrameDelay == 0)
        {
            frameCounter = 0;
            //Raycaster: mouse, onClick (for testing/dummy, no HMD)
            if (raycasterType == RayTypes.mouse && Input.GetMouseButton(1))
            {
                RaycastHit hit;
                //ray: camera origin + mouse x/y coordinates, to worldspace
                var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
                //if ray hit something (a meshCollider), process this
                if (Physics.Raycast(ray, out hit))
                {
                    //create an object at the hit corrdinate
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

                    logFixation(cube);
                    placeFixation(cube, hit, hitObject);
                }                
            }
            //Raycaster: PupilLabs
            else if (raycasterType == RayTypes.PupilLabs)
            {
                if (isRaycasting)
                {
                    RaycastHit hit;
                    var direction = getGaze.transform.position - raycastCamera.transform.position;

                    // Visualize the rays (only in Scene window)
                    //UnityEngine.Debug.DrawRay(Camera.main.transform.position, direction * 100000f, Color.green, 5f, false);
                    
                    if (Physics.Raycast(raycastCamera.transform.position, direction, out hit, Mathf.Infinity))
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        iteratedCubeName = "cube" + itemIterator.ToString();
                        itemIterator++;

                        GameObject cube;
                        if (objectType == RayObjects.cube)
                        {
                            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        }
                        else
                        {
                            cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        }

                        logFixation(cube);
                        placeFixation(cube, hit, hitObject);
                    }
                }
            }

            //Raycaster: HTC (TBD)
            else if (raycasterType == RayTypes.HTC)
            {
                //...
            }
        }        
    }

    // ================================================================================================================
    void placeFixation (GameObject cube, RaycastHit hit, GameObject hitObject)
    {
        cube.name = iteratedCubeName;
        cube.GetComponent<Renderer>().material.color = objectColor;
        cube.GetComponent<Renderer>().transform.localScale = new Vector3(objectScale, objectScale, objectScale);
        cube.GetComponent<Collider>().enabled = false; //so that they don't stack on each other
        cube.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; //not needed
        cube.GetComponent<Renderer>().receiveShadows = false; //not needed
        cube.transform.parent = hitObject.transform; //group under the object it was fixated upon
        cube.transform.position = hit.point;
        cube.tag = "Respawn"; //so that the script to recompute finds this
        cube.GetComponent<MeshRenderer>().enabled = visibleOnCast;
    }

    void logFixation (GameObject cube)
    {
        if (logToFile)
        {
            raycastCamera.GetComponent<PathScript3>().logEtData2(cube.transform.position);
        }
    }
}
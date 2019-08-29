using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterEvaluator : MonoBehaviour {
    //algorithm controls
    public KeyCode evaluateKey; //for distance evaluation
    public bool evaluateGroups; //for computing AoIs
    public bool drawHulls;      //for visualizing AoIs
    [Space(10)]

    //algortihm precision
    public float maxPointDistance = 0.05f;
    public int minClusterSize = 5;
    [Space(10)]

    //coloring/visualization
    public Color defaultColorLow;
    public Color defaultColorHigh;
    public Color failedDistanceColor;

    //service variables
    private GameObject[] raycasterListAll;
    private float[] raycasterDistances;
    private List<List<GameObject>> fixationGroups;
        //distances from one objects to all others
        //since no currentObject mentioned will be computed 2n (a-to-b, and, with a latter object, b-to-a)
    public class raycastObjectDistanceToObject
    {
        public string targetObjectId { get; set; }
        public float targetDistance { get; set; }
    }
        //stores all computed properties per singular raycast object
    public class raycastObjectProperties
    {
        public string objectId { get; set; }
        public List<raycastObjectDistanceToObject> averageDistanceTo { get; set; }
        public float objectMinimumDistance { get; set; }
        public float objectAverageDistance { get; set; }
        public bool computedPreviously { get; set; } //true if already evaluated on this object (false if just added)
        public bool recomputeFlag { get; set; }      //if within cluster distance of newly added objects, recompute

        public float getMinimumDistance()
        {
            float minimumDistance = 1000;
            foreach (var distance in averageDistanceTo)
            {
                if (distance.targetDistance < minimumDistance)
                {
                    minimumDistance = distance.targetDistance;
                }
            }
            return minimumDistance;
        }

        public float getAverageDistance()
        {
            float cumulativeDistance = 0;
            foreach (var distance in averageDistanceTo)
            {
                cumulativeDistance += distance.targetDistance;
            }
            return cumulativeDistance / averageDistanceTo.Count;
        }
    }

    // ================================================================================================================
    void Start () {
		
	}

    // ================================================================================================================
    void Update () {
		if(Input.GetKeyDown(evaluateKey))
        {
            //get all raycasted objects (per raycasterSource)
            raycasterListAll = GameObject.FindGameObjectsWithTag("Respawn");
            float maxClusterSize = 0;
            //iterate on them
            foreach(GameObject thisObject in raycasterListAll)
            {
                float minimumDistance = 1000;
                float currentClusterSize = 0;
                //this raycasted object compared against all other raycasted objects, except itself
                foreach (GameObject anotherObject in raycasterListAll)
                {
                    if(thisObject != anotherObject)
                    {
                        //object distance to other objects, cluster size (is in cluster == within a specified distance)
                        //TODO: store these, and other vars/statistics, in a List, 1:1 to object amount
                            //now, not effective & recomputes every time
                            //smarter algorithm: recompute only the ones newly added & the ones in their vicinity
                        float distance = Vector3.Distance(thisObject.transform.position,
                                                          anotherObject.transform.position);
                        if (distance < minimumDistance)
                        {
                            minimumDistance = distance;
                            //TODO: avg distance
                        }
                        if (distance < maxPointDistance)
                        {
                            currentClusterSize++;
                        }
                    }
                }
                //determine max absolute cluster size in the raycasted objects
                    //so that cluster visualization density relatives are based on this
                    //TODO: again, having a 1:1 List to these objects would make this more efficient (get max val)
                if (currentClusterSize > maxClusterSize) { maxClusterSize = currentClusterSize; }
                if ((minimumDistance > maxPointDistance) || (currentClusterSize < minClusterSize))
                {
                    thisObject.GetComponent<Renderer>().material.color = failedDistanceColor;
                } else
                {
                    thisObject.GetComponent<Renderer>().material.color =
                        Color.Lerp(defaultColorLow, defaultColorHigh, currentClusterSize / maxClusterSize);
                }
                //make sure the raycast objects are visible upon processing (raycasterSource may have hid them)
                thisObject.GetComponent<MeshRenderer>().enabled = true;
                Debug.Log("Min distance of " + thisObject.name + ": " +  minimumDistance);
            }
        }
	}
}
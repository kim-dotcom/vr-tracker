using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterEvaluator : MonoBehaviour {

    public KeyCode evaluateKey;
    public float maxPointDistance = 0.05f;
    public int minClusterSize = 5;
    public Color defaultColorLow;
    public Color defaultColorHigh;
    public Color failedDistanceColor;

    private GameObject[] raycasterListAll;
    private float[] raycasterDistances;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(evaluateKey))
        {
            raycasterListAll = GameObject.FindGameObjectsWithTag("Respawn");
            float maxClusterSize = 0;
            foreach(GameObject thisObject in raycasterListAll)
            {
                float minimumDistance = 1000;
                float currentClusterSize = 0;
                foreach (GameObject anotherObject in raycasterListAll)
                {
                    if(thisObject != anotherObject)
                    {
                        float distance = Vector3.Distance(thisObject.transform.position, anotherObject.transform.position);
                        if (distance < minimumDistance)
                        {
                            minimumDistance = distance;
                        }
                        if (distance < maxPointDistance)
                        {
                            currentClusterSize++;
                        }
                    }
                }
                if (currentClusterSize > maxClusterSize) { maxClusterSize = currentClusterSize; }
                if ((minimumDistance > maxPointDistance) || (currentClusterSize < minClusterSize))
                {
                    thisObject.GetComponent<Renderer>().material.color = failedDistanceColor;
                } else
                {
                    thisObject.GetComponent<Renderer>().material.color =
                        Color.Lerp(defaultColorLow, defaultColorHigh, currentClusterSize / maxClusterSize);
                }
                Debug.Log("Min distance of " + thisObject.name + ": " +  minimumDistance);
            }
        }
	}
}
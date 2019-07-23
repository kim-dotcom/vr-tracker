using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClickActivator : MonoBehaviour {
    public int controlNumber;
    [Space(10)]

    public bool toggleOnlyOnce;
    private int toggleCount;
    [Space(10)]

    public List<GameObject> TargetObjects;

    //trigger the array of objects on click
    void OnMouseDown()
    {
        if (!(toggleOnlyOnce && (toggleCount > 0)))
        {
            foreach (GameObject obj in TargetObjects)
            {
                if (obj.GetComponent<ToggleScript>() != null)
                {
                    obj.GetComponent<ToggleScript>().Toggle(controlNumber);
                }
            }
        }
        toggleCount++;
    }
}

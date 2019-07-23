using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKeyActivator : MonoBehaviour {
    //the following structure is necessary, so as to have a 2D array (obejects per keypress) visible in the inspector
    [System.Serializable]
    public struct TargetObjectLists
    {
        //controlNumber is sent on a per key basis
        public int controlNumber;
        public bool toggleOnlyOnce;
        public GameObject[] thisKeyObjects;
        [HideInInspector]  public int triggerCount;
    }

    //public int controlNumber;
    public List<KeyCode> triggerKeys;
    public TargetObjectLists[] TargetObjects;

    // Update is called once per frame
    void Update () {
        foreach (KeyCode key in triggerKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (!TargetObjects[triggerKeys.IndexOf(key)].toggleOnlyOnce ||
                    (TargetObjects[triggerKeys.IndexOf(key)].toggleOnlyOnce &&
                     TargetObjects[triggerKeys.IndexOf(key)].triggerCount == 0))
                {
                    Debug.Log("Pressed " + key);
                    //on keyPress, trigeer all the objects in this key's list
                    foreach (GameObject subObject in TargetObjects[triggerKeys.IndexOf(key)].thisKeyObjects)
                    {
                        if (subObject.GetComponent<ToggleScript>() != null)
                        {
                            subObject.GetComponent<ToggleScript>()
                                .Toggle(TargetObjects[triggerKeys.IndexOf(key)]
                                .controlNumber);
                        }
                    }
                    TargetObjects[triggerKeys.IndexOf(key)].triggerCount++;
                }
            }
        }
    }
}

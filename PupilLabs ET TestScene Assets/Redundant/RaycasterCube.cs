using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;
using System;
using UnityEditor;

public class RaycasterCube : MonoBehaviour {

    public float objectScale = 0.025f;
    public Color objectColor;
    public float logFrameDelay = 5;

    public Camera raycastCamera;

    public KeyCode rayCastKey;

    private bool Raycasting;
    private int itemIterator = 0;
    private string iteratedCubeName;
    private int frameCounter;

    private GameObject getGaze;


	// Use this for initialization
	void Start () {

        getGaze = GameObject.Find("Gaze_3D");

    }

    void Update()
    {
        //PupilTools.SubscribeTo("gaze");
        //UnityEngine.Debug.Log(PupilData._3D.GazePosition);
                frameCounter++;
                if (frameCounter % logFrameDelay == 0) {

                    frameCounter = 0;

                    if (Input.GetKeyDown(rayCastKey)) {
                    
                        Raycasting = !Raycasting;
                    }

                    if (Raycasting) {

                        RaycastHit hit;

                        var direction = getGaze.transform.position - raycastCamera.transform.position;

                        //UnityEngine.Debug.DrawRay(Camera.main.transform.position, direction * 100000f, Color.green, 5f, false); //visualize the rays

                        if (Physics.Raycast(raycastCamera.transform.position, direction, out hit, Mathf.Infinity)) {

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

                        } else if (!Raycasting) {

                            return;  
                        }
                    }
                }
    }

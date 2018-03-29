using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Camera))]

public class GameInit : MonoBehaviour
{
	void Awake()
	{
		// TODO: which one is faster?
		//use camera width and height for calculation of ray length
		//Camera mainCamera = GetComponentInParent<Camera>();
		Camera mainCamera = Camera.main;
		float a2 = Mathf.Pow(mainCamera.pixelWidth, 2.0f); // 382 -> 145924
		float b2 = Mathf.Pow(mainCamera.pixelHeight, 2.0f); // 215 -> 46225
		Constants.Laser.RaycastDistance = Mathf.Sqrt(a2 + b2); // 192149
		Vector3 screenToWorldX = mainCamera.ScreenToWorldPoint(Vector3.zero);
		Vector3 screenToWorldY = mainCamera.ScreenToWorldPoint(Vector3.right);
		float pixelToWorldFactor = Vector3.Distance(screenToWorldX, screenToWorldY);
		Constants.Laser.RaycastDistance *= pixelToWorldFactor; //pixel to units
		//Debug.Log("screen width: " + Screen.width + " height: " + Screen.height);
		//Debug.Log("onGUI: raycast distance: " + m_raycastDistance);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

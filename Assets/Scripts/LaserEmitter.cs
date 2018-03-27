using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tizen;
using UnityEngine.UI;

public class LaserEmitter : MonoBehaviour
{
	//public struct Laser
	//{
	public LineRenderer laser;
	//public float orientation;
	//}

	//public Laser m_laser;
	//private bool m_isActive;

	private static float m_raycastDistance;
	//private bool m_isDirty;

	void Awake()
	{
		//m_isActive = false;
		//m_isDirty = true;
	}

	//void OnGUI()
	//{
	//}

	// Use this for initialization
	void Start()
	{
		float a2 = Mathf.Pow(Screen.width - 32.0f * 2.0f, 2.0f); // 382 -> 145924
		float b2 = Mathf.Pow(Screen.height - 32.0f * 2.0f, 2.0f); // 215 -> 46225
		m_raycastDistance = Mathf.Sqrt(a2 + b2); // 192149
		Debug.Log("screen width: " + Screen.width + " height: " + Screen.height);
		Debug.Log("onGUI: raycast distance: " + m_raycastDistance);
	}

	// Update is called once per frame
	void Update()
	{
		//if (m_isDirty)
		//laser.bounds
		if (laser.enabled)
		{
			RaycastHit2D result = Physics2D.Raycast(laser.transform.position, laser.transform.position - transform.position,
				m_raycastDistance);
			Debug.Log("raycast distance: " + m_raycastDistance);
			Debug.DrawLine(laser.transform.position,
				laser.transform.position + (laser.transform.position - transform.position) * m_raycastDistance);
			Debug.DrawLine(Vector3.zero,
				Vector3.zero + (new Vector3(1, 0, 0) * m_raycastDistance), Color.red);
			//laser.positionCount
			laser.positionCount = 2;
			laser.SetPosition(1, result.point);
			//result.transform
			//result.normal
		}
	}

	void OnMouseDown()
	{
		Debug.Log("LaserEmitter: OnMouseDown");
		laser.enabled = !laser.enabled;
		//m_isActive = !m_isActive;
	}
}
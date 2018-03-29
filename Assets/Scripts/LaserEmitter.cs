using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Tizen;
using UnityEngine.UI;

public class LaserEmitter : MonoBehaviour
{
	//public struct Laser
	//{
	public LineRenderer laser;

	public const int maxBounceCount = 5;
	//public float orientation;
	//}

	//public Laser m_laser;
	//private bool m_isActive;

	private static float m_raycastDistance;
	//private bool m_isDirty;

	private Camera myCamera;

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
		myCamera = Camera.main; //use camera width and height for calculation of ray length
		float a2 = Mathf.Pow(myCamera.pixelWidth, 2.0f); // 382 -> 145924
		float b2 = Mathf.Pow(myCamera.pixelHeight, 2.0f); // 215 -> 46225
		m_raycastDistance = Mathf.Sqrt(a2 + b2); // 192149
		Vector3 UnitsPerPixelX = myCamera.ScreenToWorldPoint(Vector3.zero);
		Vector3 UnitsPerPixelY = myCamera.ScreenToWorldPoint(Vector3.right);
		float UnitsPerPixel = Vector3.Distance(UnitsPerPixelX, UnitsPerPixelY);
		m_raycastDistance *= UnitsPerPixel; //pixel to units
											//Debug.Log("screen width: " + Screen.width + " height: " + Screen.height);
											//Debug.Log("onGUI: raycast distance: " + m_raycastDistance);
	}

	// Update is called once per frame
	void Update()
	{
		//if (m_isDirty)
		//laser.bounds
		if (laser.enabled)
		{
			Vector2 emitterPosition = transform.position;
			Vector2 laserPosition = laser.transform.position;
			Debug.DrawLine(laser.transform.position,
				laserPosition + (laserPosition - emitterPosition) * m_raycastDistance);
			//Debug.Log("raycast distance: " + m_raycastDistance);

			//Vector2 raycastHitPosition = raycastHit.point;
			//Vector2 raycastHitNormal = raycastHit.normal;
			//Debug.Log("raycast hit position: " + raycastHitPosition);

			var reflectionPoints = new List<Vector3>(maxBounceCount) {laserPosition};

			//NativeArray<Vector2> reflectionPoints = new NativeArray<Vector2>();
			//reflectionPoints.{ laserPosition };

			const float RAYCAST_EPSILON = 0.001f;
			Vector2 direction = laserPosition - emitterPosition;
			Reflector reflector;
			int bounceCount = 0;
			do
			{
				RaycastHit2D raycastHit = Physics2D.Raycast(laserPosition + direction * RAYCAST_EPSILON, direction,
					m_raycastDistance);
				laserPosition = raycastHit.point;
				reflectionPoints.Add(laserPosition);

				Debug.DrawLine(laserPosition, laserPosition + raycastHit.normal, Color.cyan);
				direction = Vector2.Reflect(direction, raycastHit.normal);

				reflector = raycastHit.transform.GetComponentInParent<Reflector>();

				//TODO: handle reflector properties

				++bounceCount;
			} while (reflector && bounceCount <= maxBounceCount);
			Debug.Log("bounceCount: " + --bounceCount);

			laser.positionCount = reflectionPoints.Count;
			laser.SetPositions(reflectionPoints.ToArray());
			/*
			laser.positionCount = 2;
			laser.SetPosition(0, emitterPosition);
			laser.SetPosition(1, raycastHit.point);
			Debug.DrawLine(emitterPosition, raycastHit.point, Color.green);

			Debug.DrawLine(emitterPosition, emitterPosition + direction.normalized, Color.blue);
			direction = Vector2.Reflect(direction, raycastHit.normal);
			Debug.DrawLine(raycastHit.point, raycastHit.point + raycastHit.normal, Color.cyan);
			Debug.DrawLine(raycastHit.point + direction.normalized * RAYCAST_EPSILON,
				raycastHit.point + direction.normalized, Color.blue);

			raycastHit = Physics2D.Raycast(raycastHit.point + direction.normalized * RAYCAST_EPSILON, direction,
				m_raycastDistance);
			//raycastHitPosition = raycastHit.point;

			laser.positionCount = 3;
			laser.SetPosition(2, raycastHit.point);
			//result.transform
			//result.normal
			*/
		}
	}

	void OnMouseDown()
	{
		Debug.Log("LaserEmitter: OnMouseDown");
		laser.enabled = !laser.enabled;
		//m_isActive = !m_isActive;
	}
}
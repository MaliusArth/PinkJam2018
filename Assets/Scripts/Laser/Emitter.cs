
#define use_the_cool_way

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Tizen;
using UnityEngine.UI;



public class Emitter : MonoBehaviour
{
	//public struct Laser
	//{
	private LineRenderer laser;

	//public const int maxBounceCount = 5;
	//public float orientation;
	//}

	//public Laser m_laser;
	//private bool m_isActive;

	//private static float m_raycastDistance;
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
		laser = GetComponentInParent<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//if (m_isDirty)
		//laser.bounds
		if (laser.enabled)
		{
			Vector2 emitterPosition = transform.position;
			Vector2 laserPosition = emitterPosition + new Vector2(transform.up.x, transform.up.y) * 0.7f;
			Debug.DrawLine(laser.transform.position,
				laserPosition + (laserPosition - emitterPosition) * Constants.Laser.RaycastDistance);
			//Debug.Log("raycast distance: " + m_raycastDistance);

			//Vector2 raycastHitPosition = raycastHit.point;
			//Vector2 raycastHitNormal = raycastHit.normal;
			//Debug.Log("raycast hit position: " + raycastHitPosition);

			var reflectionPoints = new List<Vector3>(Constants.Laser.MaxReflectionCount) {laserPosition};
			//var reflectionPoints = new NativeArray<Vector2>();

			Vector2 direction = laserPosition - emitterPosition;
			direction.Normalize();

#if use_the_cool_way
			RaycastHit2D raycastHit = Physics2D.Raycast(laserPosition + direction * Constants.Laser.Epsilon, direction,
				Constants.Laser.RaycastDistance);

			Debug.DrawLine(laserPosition, raycastHit.point, Color.blue);
			Debug.DrawLine(raycastHit.point, raycastHit.point + raycastHit.normal, Color.cyan);

			//laserPosition = raycastHit.point;
			//reflectionPoints.Add(laserPosition);

			reflectionPoints.Add(raycastHit.point);

			IReactor reactor = raycastHit.transform.GetComponentInParent<IReactor>();
			if (reactor != null)
			{
				//TODO: handle properties
				reactor.React(ref reflectionPoints, raycastHit);
			}
#else
			IReactor reactor;
			//RaycastHit2D raycastHit;
			int bounceCount = 0;
			do
			{
				RaycastHit2D raycastHit = Physics2D.Raycast(laserPosition + direction * LaserConstants.Epsilon, direction,
					LaserConstants.RaycastDistance);
				Debug.DrawLine(laserPosition, raycastHit.point, Color.blue);

				laserPosition = raycastHit.point;
				reflectionPoints.Add(laserPosition);

				Debug.DrawLine(laserPosition, laserPosition + raycastHit.normal, Color.cyan);
				direction = Vector2.Reflect(direction, raycastHit.normal);

				//reactor = raycastHit.transform.GetComponentInParent<IReactor>();
				//if (reactor != null)
				//{
				//	//TODO: handle properties
				//	reactor.React(ref reflectionPoints, raycastHit);
				//}

				++bounceCount;
			} while (/*reactor != null &&*/ bounceCount <= maxBounceCount);
			Debug.Log("bounceCount: " + --bounceCount);
#endif

			laser.positionCount = reflectionPoints.Count;
			laser.SetPositions(reflectionPoints.ToArray());
		}
	}

	void OnMouseDown()
	{
		Debug.Log("LaserEmitter: OnMouseDown");
		laser.enabled = !laser.enabled;
		//m_isActive = !m_isActive;
	}
}
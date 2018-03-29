using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, IReactor
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void React(ref List<Vector3> laserPositions, RaycastHit2D lastRaycastHit)
	{
		int currentIndex = laserPositions.Count - 1;
		Vector2 lastPosition = laserPositions[currentIndex-1];
		Vector2 currentPosition = laserPositions[currentIndex];
		Vector2 direction = new Vector2(currentPosition.x, currentPosition.y) - lastPosition;
		direction.Normalize();

		Debug.DrawLine(currentPosition, currentPosition + lastRaycastHit.normal, Color.cyan);
		direction = Vector2.Reflect(direction, lastRaycastHit.normal);

		RaycastHit2D raycastHit =
			Physics2D.Raycast(currentPosition + direction * Constants.Laser.Epsilon, direction,
			Constants.Laser.RaycastDistance);
		Debug.DrawLine(currentPosition, raycastHit.point, Color.blue);

		if (lastRaycastHit.transform == raycastHit.transform)
		{
			Debug.Log("hit same object twice: " + raycastHit.transform.name + " ?!?");
			return;
		}
		laserPositions.Add(raycastHit.point);

		IReactor reactor = raycastHit.transform.GetComponentInParent<IReactor>();
		if (reactor != null && currentIndex < Constants.Laser.MaxReflectionCount)
		{
			//TODO: handle properties
			reactor.React(ref laserPositions, raycastHit);
		}
	}
}

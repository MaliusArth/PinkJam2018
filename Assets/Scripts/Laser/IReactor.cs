using System.Collections.Generic;
using UnityEngine;

public interface IReactor
{
	void React(ref List<Vector3> laserPositions, RaycastHit2D lastRaycastHit);
}

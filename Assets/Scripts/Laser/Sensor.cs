using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteToggle))]

public class Sensor : MonoBehaviour, IReactor
{
	SpriteToggle spriteToggle;

	private bool m_dirty;
	// Use this for initialization
	void Start ()
	{
		spriteToggle = GetComponentInParent<SpriteToggle>();
		m_dirty = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!m_dirty)
		{
			spriteToggle.SetSprite(SpriteToggle.State.Off);
		}
	}

	void LateUpdate()
	{
		m_dirty = false;
		//spriteToggle.SetSprite();
	}

	public void React(ref List<Vector3> laserPositions, RaycastHit2D lastRaycastHit)
	{
		m_dirty = true;
		spriteToggle.SetSprite(SpriteToggle.State.On);
	}
}

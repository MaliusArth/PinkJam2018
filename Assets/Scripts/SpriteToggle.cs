using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
	public Sprite[] stateSprites;

	public enum State
	{
		Off = 0,
		On = 1
	}

	private State m_state;
	private SpriteRenderer m_renderer;

	// Use this for initialization
	void Start ()
	{
		m_renderer = GetComponentInParent<SpriteRenderer>();
		SetSprite(State.Off);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//SetState(State.Off);
	}

	public void SetSprite(State state)
	{
		m_state = state;
		m_renderer.sprite = stateSprites[(int)m_state];
	}

	public void SetSprite()
	{
		SetSprite(m_state);
	}
}

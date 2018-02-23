using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFloatingAnimation : MonoBehaviour {

	public float m_FloatingSpeed;
	public float m_Amplitude;

	Vector3 m_StartPos;

	private void Start() {
		m_StartPos = transform.position;
	}

	private void Update() {
		transform.position = m_StartPos + Mathf.Sin(Time.time * m_FloatingSpeed) * m_Amplitude * Vector3.up;
	}
}

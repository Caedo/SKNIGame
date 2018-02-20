using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectileTester : MonoBehaviour {

	public float m_Velocity = 10;
	public float m_MaxXPosition = 10;
	public float m_MinXPosition = -10;

	private ParticleSystem currentParticles;

	private void Awake() {
		currentParticles = GetComponent<ParticleSystem>();
	}

	private void Update() {
		transform.position += Vector3.right * m_Velocity * Time.deltaTime;

		if (transform.position.x > m_MaxXPosition) {
			transform.position = new Vector3(m_MinXPosition, transform.position.y, transform.position.z);
		} else if (transform.position.x < m_MinXPosition) {
			transform.position = new Vector3(m_MaxXPosition, transform.position.y, transform.position.z);
		}

	}
}
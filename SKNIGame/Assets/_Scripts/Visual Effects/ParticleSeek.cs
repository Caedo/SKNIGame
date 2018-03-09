using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSeek : MonoBehaviour {

	public Transform m_Attractor;
	public float m_Force;

	ParticleSystem m_Ps;
	ParticleSystem.Particle[] particles;

//	float m_StartDistToTarget;

	private void Awake() {
		m_Ps = GetComponent<ParticleSystem>();
	}

	private void Start() {
		//m_StartDistToTarget = Vector3.Distance(transform.position, m_Attractor.position);
	}

	void LateUpdate() {
		if (particles == null || particles.Length < m_Ps.particleCount) {
			particles = new ParticleSystem.Particle[m_Ps.particleCount];
		}

		m_Ps.GetParticles(particles);

		for (int i = 0; i < particles.Length; i++) {
			ParticleSystem.Particle p = particles[i];
			Vector3 particleWorldPos = Vector3.zero;

			switch (m_Ps.main.simulationSpace) {
				case ParticleSystemSimulationSpace.Local:
					{
						particleWorldPos = transform.TransformPoint(p.position);
						break;
					}
				case ParticleSystemSimulationSpace.World:
					{
						particleWorldPos = p.position;
						break;

					}
				case ParticleSystemSimulationSpace.Custom:
					{
						particleWorldPos = m_Ps.main.customSimulationSpace.TransformPoint(p.position);
						break;
					}
			}

			Vector3 dir = (m_Attractor.position - particleWorldPos);
			//float distToTarget = Vector3.Distance(particleWorldPos, m_Attractor.position);
			//Debug.DrawLine(p.position, m_Attractor.position, Color.green, .01f, true);

			p.velocity += dir.normalized * m_Force * Time.deltaTime;

			particles[i] = p;
		}

		m_Ps.SetParticles(particles, particles.Length);
	}
}
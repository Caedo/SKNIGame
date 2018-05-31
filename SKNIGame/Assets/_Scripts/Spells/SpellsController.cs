using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour {

	public SpellLibrary m_SpellLibrary;
	public Transform m_SpellOrigin;

	SpellData m_ActiveSpell;
	ParticleSystem m_ActualHandParticles;

	private void Awake() { }

	private void Start() {
		NewGestureController.Instance.OnGestureMatch += GestureMatched;
		InputController.Instance.SubscribeEventHandler("CastSpellDown", CastSpell);
	}

	void GestureMatched(Gesture gesture) {
		m_ActiveSpell = m_SpellLibrary.m_SpellDataList.Find(d => d.m_GestureToMatch == gesture);
		if (m_ActiveSpell != null) {

			if (m_ActualHandParticles != null) {
				Destroy(m_ActualHandParticles);
			}
			
			m_ActualHandParticles = Instantiate(m_ActiveSpell.m_HandParticles, m_SpellOrigin);
			m_ActualHandParticles.transform.localPosition = Vector3.zero;
		}
	}

	void CastSpell() {
		if (m_ActiveSpell != null) {
			m_ActualHandParticles.Stop();
			Destroy(m_ActualHandParticles.gameObject, m_ActualHandParticles.main.duration);
			SpellProjectile proj = Instantiate(m_ActiveSpell.m_ProjectilePrefab, m_SpellOrigin.position, m_SpellOrigin.rotation);
			proj.Initialize(m_ActiveSpell);

			m_ActiveSpell = null;
		}
	}
}
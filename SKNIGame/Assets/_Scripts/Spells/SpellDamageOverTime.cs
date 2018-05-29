using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageOverTime : SpellDamage {

	public int m_TickCount;
	public float m_TimeBetweenTicks;
	public bool m_UseTickEffects;
	public ParticleSystem m_TickEffectParticle;
	public int m_TickEffectBurstCount;

	protected override void Start() {
		StartCoroutine(DealDamageCourutine());
	}

	IEnumerator DealDamageCourutine() {
		var wait = new WaitForSeconds(m_TimeBetweenTicks);

		for (int i = 0; i < m_TickCount; i++) {
			DoDamage();
			if (m_UseTickEffects) {
				m_TickEffectParticle.Emit(m_TickEffectBurstCount);
			}
			yield return wait;
		}

		m_Particles.Stop();
		DestroyAfterParicleDuration();
	}

	/// <summary>
	/// Callback to draw gizmos only if the object is selected.
	/// </summary>
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, m_DamageRadius);
	}
}
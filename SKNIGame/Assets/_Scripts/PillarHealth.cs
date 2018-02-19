using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHealth : LivingEntity {

	public static System.Action<PillarHealth> OnPillarDestroy;

	public GameObject m_LivingModel; //Model that exists when HP is greater than 0
	public GameObject m_DestroyedModel;

	public System.Action<float, float> OnPillarTakeDamage;

	public override void Damage(float dmg, Element attackElement) {
		base.Damage(dmg, attackElement);

		if (OnPillarTakeDamage != null) {
			OnPillarTakeDamage(CurrentHealth, m_MaxHealth);
		}
	}

	protected override void Die() {
		this.enabled = false;

		if (OnPillarDestroy != null) {
			OnPillarDestroy(this);
		}

		if (m_LivingModel != null)
			m_LivingModel.SetActive(false);
		if (m_DestroyedModel != null)
			m_DestroyedModel.SetActive(true);

		//Destroy(this);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour {

	public Element m_Element;

	[HideInInspector]
	public float m_MaxHealth = 100;

	public float CurrentHealth {get; private set; }

	protected virtual void Start() {
		CurrentHealth = m_MaxHealth;
		//Debug.Log("Health: " +  m_MaxHealth);
	}

	public virtual void Damage(float dmg, Element attackElement) {
		float dealtDamage = dmg;
		if(attackElement != null)
			dealtDamage *= attackElement.GetMultiplierAgainst(m_Element);

		//Debug.Log(dealtDamage);
			
		CurrentHealth -= dealtDamage;
		if (CurrentHealth <= 0) {
			Die();
		}
	}

	protected abstract void Die();
}
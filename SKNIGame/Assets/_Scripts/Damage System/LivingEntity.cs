using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour {

	public Element m_DefenseElement;

	public float m_MaxHealth = 100;

	public float CurrentHealth {get; private set; }

	protected virtual void Start() {
		CurrentHealth = m_MaxHealth;
	}

	public virtual void Damage(float dmg, Element attackElement) {
		if(attackElement != null)
			dmg *= attackElement.GetMultiplierAgainst(m_DefenseElement);
			
		CurrentHealth -= dmg;
		if (CurrentHealth <= 0) {
			Die();
		}
	}

	protected abstract void Die();
}
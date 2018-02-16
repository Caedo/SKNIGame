using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour {

	public Element m_DefenseElement;
	//[HideInInspector] uncomment after test
	public float m_MaxHealth = 100;

	private float m_CurrentHealth;

	protected virtual void Start() {
		m_CurrentHealth = m_MaxHealth;
	}

	public virtual void Damage(float dmg, Element attackElement) {
		m_CurrentHealth -= dmg * attackElement.GetMultiplierAgainst(m_DefenseElement);
		if (m_CurrentHealth <= 0) {
			Die();
		}
	}

	protected abstract void Die();
}
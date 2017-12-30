using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

	public Element m_DefenseElement;
	public float m_MaxHealth = 100;

	private float m_CurrentHealth;

	private void Start()
	{
		m_CurrentHealth = m_MaxHealth;
	}

	public void Damage(float dmg, Element attackElement)
	{
		m_CurrentHealth -= dmg * attackElement.GetMultiplierAgainst(m_DefenseElement);
		if (m_CurrentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealh : LivingEntity
{
	public static System.Action OnEnemyDeath;

	public void Initialize(EnemyStats stats){
		m_MaxHealth = stats.m_Health;
		m_DefenseElement = stats.m_Element;
	}
    protected override void Die()
    {
		if(OnEnemyDeath != null)
			OnEnemyDeath();
        Destroy(gameObject);
    }
}

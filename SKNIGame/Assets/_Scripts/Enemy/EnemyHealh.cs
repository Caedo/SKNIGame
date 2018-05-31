using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealh : LivingEntity
{
	public static System.Action<EnemyHealh> OnEnemyDeath;

	[HideInInspector]
	public EnemyStats m_Stats;

	public void Initialize(EnemyStats stats){
		m_MaxHealth = stats.m_Health;

		m_Stats = stats;
	}
    protected override void Die()
    {
		if(OnEnemyDeath != null)
			OnEnemyDeath(this);
        Destroy(gameObject);
    }
}

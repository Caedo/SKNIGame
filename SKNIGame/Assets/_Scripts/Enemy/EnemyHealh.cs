using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealh : LivingEntity
{
	public static System.Action OnEnemyDeath;

    protected override void Die()
    {
		if(OnEnemyDeath != null)
			OnEnemyDeath();
        Destroy(gameObject);
    }
}

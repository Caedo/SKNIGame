using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealh))]
public class EnemyController : MonoBehaviour {

	public EnemyStats m_Stats;

    EnemyHealh m_Health;

    private void Awake() {
        m_Health = GetComponent<EnemyHealh>();    
    }

    private void Start() {
        m_Health.m_MaxHealth = m_Stats.m_Health;
    }
}

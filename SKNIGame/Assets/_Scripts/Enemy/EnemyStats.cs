using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject {
	public string m_EnemyName;
	public Element m_Element;
	public float m_Health;
	public float m_Damage;
	public float m_AttacksPerSecond;
	public float m_AttackRange;
	public float m_MovementSpeed;
	public int m_ScoreValue;
	public string m_Description;

	public float TimeBetweenAttacks {
		get {
			if (m_AttacksPerSecond != 0) {
				return 1 / m_AttacksPerSecond;
			} else {
				Debug.LogWarning("Attacks per second set to 0 in enemy :" + m_EnemyName);
				return 1000;
			}
		}
	}

}
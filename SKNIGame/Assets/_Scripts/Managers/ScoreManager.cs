using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public int m_Score {get; private set;}
	public int m_Combo {get; private set;}

	private float m_TargetComboTime;

	void Awake()
	{
		EnemyHealh.OnEnemyDeath += OnEnemyDeath;
	}
	//Beautiful timer
	void Update()
	{
		//If we still have time to count, subtract deltaTime
		if(m_TargetComboTime > 0)
		{
			m_TargetComboTime -= Time.deltaTime;
		}
		//If we don't have more time, set combo to 0
		else
		{
			m_Combo = 0;
		}
	}
	void OnEnemyDeath(/*EnemyHealh health*/)
	{
		//Increment combo, this must be first to make sure this will be at least 1
		m_Combo++;
		//Add score
		m_Score += /*(int)health.m_MaxHealth*/ 10 * m_Combo;
		//Set combo timer
		m_TargetComboTime = CalculateComboTime();	
	}

	private void OnDisable()
	{
		EnemyHealh.OnEnemyDeath -= OnEnemyDeath;
	}

	//Calculate time for combo count. If higher combo then less time.
	private float CalculateComboTime()
	{
		return (float) Math.Pow(0.99, m_Combo) * 30;
	}

	//Calculate points with combo. If higher combo than more points.
	private int CalculateComboScore(int score)
	{
		return (int)(score + score * m_Combo * 1.02);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class WaveData {//: ScriptableObject {
	public List<SubWave> m_SubWaves;

	public int SubWavesCount { get { return m_SubWaves.Count; } }
}

[System.Serializable]
public class SubWave {
	public string m_Name;
	public float m_WaitTime;
	public List<EnemyCount> m_Enemies;

	[System.Serializable]
	public class EnemyCount {
		public EnemyController m_EnemyPrefab;
		public int m_SpawnCount;
	}

    public void AddEnemyList(List<EnemyController> enemyList)
    {
        while(enemyList.Count > 0)
        {
            EnemyController enemy = enemyList[0];
            int spawnCount = 0;
            while (enemyList.Contains(enemy))
            {
                spawnCount++;
                enemyList.RemoveAt(enemyList.LastIndexOf(enemy));
            }
            EnemyCount enemyCount = new EnemyCount
            {
                m_EnemyPrefab = enemy,
                m_SpawnCount = spawnCount
            };
            Debug.Log(enemy.name + " " + spawnCount);
            m_Enemies.Add(enemyCount); 
        }
    }
}
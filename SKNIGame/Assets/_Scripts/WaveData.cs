using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveData : ScriptableObject {
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
}
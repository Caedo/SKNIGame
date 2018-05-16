using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform[] m_SpawnPoints;
	public List<WaveData> m_Waves;
	public float m_WaveDelay = 15f; //This is time that has to pass between two waves
	public float m_SpawnDelay = 0.5f; //time between two spawns - prevent object clipping
	public float WaveTimer { get; private set; } // timer for waiting between waves 

	WaitForSeconds m_WaitForSpawn;
	int m_WaveNumber = -1; //current wave number
	int m_EnemiesAlive = 0;
	bool m_WaitingForNextWave = true; 
	Queue<Transform> m_SpawnPointsQueue = new Queue<Transform>();

	void Awake() {
		m_WaitForSpawn = new WaitForSeconds(m_SpawnDelay);
		EnemyHealh.OnEnemyDeath += OnEnemyDeath;
	}

	private void Start() {
		foreach (var item in m_SpawnPoints) {
			m_SpawnPointsQueue.Enqueue(item);
		}
	}

	private void Update() {
		if (CanSpawnNextWave()) {
			WaveTimer += Time.deltaTime;
			if (WaveTimer >= m_WaveDelay) {
				m_WaitingForNextWave = false;
				WaveTimer = 0;

				StartCoroutine(SpawnWave());
			}
		}
	}

	//To refactor...
	IEnumerator SpawnWave() {
		m_WaveNumber++;

		if (m_WaveNumber >= m_Waves.Count) {
			//End Game
			Debug.Log("Game ended");
		} else {
			Debug.Log("Spawning next Wave");		

			WaveData currentWave = m_Waves[m_WaveNumber];
			for (int i = 0; i < currentWave.SubWavesCount; i++) {

				Debug.Log("Spawning SubWave");
				SubWave currentSubWave = currentWave.m_SubWaves[i];
				for (int j = 0; j < currentSubWave.m_Enemies.Count; j++) { //Spawn all enemies in subwave

					for (int k = 0; k < currentSubWave.m_Enemies[j].m_SpawnCount; k++) { // this loop is for all enemies types in subwaves

						Transform spawnPoint = m_SpawnPointsQueue.Dequeue();
						var enemy = Instantiate(currentSubWave.m_Enemies[j].m_EnemyPrefab, spawnPoint.position, Quaternion.identity);

						enemy.InitializeTargets(GameManager.Instance.ActivePillars);

						m_SpawnPointsQueue.Enqueue(spawnPoint);
						m_EnemiesAlive++;

						yield return m_WaitForSpawn;					
					}
				}
				Debug.Log("Waiting for SubWave");
				yield return new WaitForSeconds(currentSubWave.m_WaitTime);
			}

			Debug.Log("Wave Ended");
		}

		m_WaitingForNextWave = true;
		yield return null;
	}

	//Called when some enemy was killed
	void OnEnemyDeath(EnemyHealh health) {
		m_EnemiesAlive--;
	}

	//Next wave can be created when all enemies were spawned and then killed
	public bool CanSpawnNextWave() { 
		return m_WaitingForNextWave && m_EnemiesAlive == 0;
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

	public List<ParticleSystem> m_ParticlesWhenWin;

	private void Start() {
		GameManager.Instance.OnGameWon += GameWon;
	}

	void GameWon() {
		foreach (var system in m_ParticlesWhenWin) {
			system.Play();
		}
	}
}
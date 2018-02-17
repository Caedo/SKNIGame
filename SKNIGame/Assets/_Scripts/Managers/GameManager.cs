using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }

	List<PillarHealth> m_ActivePillars;
	private void Awake() {
		//I'm evil so I'm creating singletons!
		if(Instance != null){
			Debug.LogWarning("Second GameManager on Scene, deleting old one!");
			DestroyImmediate(Instance.gameObject);
		}
		
		Instance = this;

		//Subscribe OnPillarDestroyed to static event
		PillarHealth.OnPillarDestroy += OnPillarDestroyed;
	}

	private void Start() {
		//Probably this should be changed
		m_ActivePillars = FindObjectsOfType<PillarHealth>().ToList();
	}

	void OnPillarDestroyed(PillarHealth pillar){
		m_ActivePillars.Remove(pillar);

		//When all pillars are destroyed game is over
		if(m_ActivePillars.Count <= 0){
			GameOver();
		}
	}

	void GameOver(){
		Debug.Log("GameOver");
	}
}
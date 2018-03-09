using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGestureTester : MonoBehaviour {

	public List<TesterPair> tests;

	private void Start() {
		FindObjectOfType<NewGestureController>().OnGestureMatch += OnGestureMatch;
	}

	void OnGestureMatch(NewGesture gesture, Vector3 position) {
		var pair = tests.Find(p => p.key == gesture.m_Name);
		if (pair != null) {
			Destroy(Instantiate(pair.particles, position, Quaternion.identity).gameObject, 5);
		}
	}

	[System.Serializable]
	public class TesterPair {
		public string key;
		public ParticleSystem particles;
	}
}
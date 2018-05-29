using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGestureTester : MonoBehaviour {

	public List<TesterPair> tests;

	private void Start() {
		//FindObjectOfType<NewGestureController>().OnGestureMatch += OnGestureMatch;
	}

	void OnGestureMatch(Gesture gesture, Vector3 position) {
		var pair = tests.Find(p => p.key == gesture.name);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gestures/Gesture")]
public class Gesture : ScriptableObject {
	new public string name;
	public List<int> m_Sequence = new List<int>();

	public bool CheckGestureValues(List<int> other) {
		if (other.Count != m_Sequence.Count) {
			return false;
		}

		int counter = 0;
		foreach (var item in other) {
			if (m_Sequence.Contains(item)) {
				counter++;
			}
		}

		return counter == m_Sequence.Count;
	}

	public bool CheckGestureSequence(List<int> other) {

		if (other.Count != m_Sequence.Count) {
			return false;
		}

		for (int i = 0; i < m_Sequence.Count; i++) {
			if (m_Sequence[i] != other[i]) {
				return false;
			}
		}

		return true;
	}
}

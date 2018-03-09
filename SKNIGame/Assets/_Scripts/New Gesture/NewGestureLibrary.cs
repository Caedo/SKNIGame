using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Values - check only values, order doesn't matter
//Sequence - order is important
public enum GestureCheckType { Values, Sequence }

[System.Serializable]
public class NewGesture {
	public string m_Name;
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

[CreateAssetMenu]
public class NewGestureLibrary : ScriptableObject {
	public GestureCheckType m_GestureCheckType;
	public NewGesture[] m_Gestures;

	public NewGesture MatchGesture(List<int> other) {

		foreach (var gesture in m_Gestures) {
			bool found = false;
			switch (m_GestureCheckType) {
				case GestureCheckType.Values:
					found = gesture.CheckGestureValues(other);
					break;
				case GestureCheckType.Sequence:
					found = gesture.CheckGestureSequence(other);
					break;
			}

			if (found) {
				return gesture;
			}
		}

		return null;
	}
}
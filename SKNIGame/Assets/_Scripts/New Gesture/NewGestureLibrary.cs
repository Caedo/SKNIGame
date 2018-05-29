using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Values - check only values, order doesn't matter
//Sequence - order is important
public enum GestureCheckType { Values, Sequence }


[CreateAssetMenu(menuName = "Gestures/Gesture Library")]
public class NewGestureLibrary : ScriptableObject {
	public GestureCheckType m_GestureCheckType;
	public Gesture[] m_Gestures;

	public Gesture MatchGesture(List<int> other) {

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
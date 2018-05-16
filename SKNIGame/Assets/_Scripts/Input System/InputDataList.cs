using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputDataList : ScriptableObject {
	public enum KeyEventType { Down, Pressed, Up }

	[System.Serializable]
	public class StringKeycodePair {
		public string inputName;
		public KeyCode keyCode;
		public KeyEventType eventType;
		public System.Action keyEvent;
	}

	public List<StringKeycodePair> m_KeyList;
}
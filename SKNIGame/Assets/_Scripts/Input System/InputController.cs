using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour {



	public static InputController Instance { get; private set; }

	public InputDataList m_InputDataList;

	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Update() {
		foreach (var item in m_InputDataList.m_KeyList) {
			if (item.keyEvent == null)
				continue;

			switch (item.eventType) {
				case InputDataList.KeyEventType.Down:
					if (Input.GetKeyDown(item.keyCode)) {
						item.keyEvent();
					}
					break;
				case InputDataList.KeyEventType.Pressed:
					if (Input.GetKey(item.keyCode)) {
						item.keyEvent();
					}
					break;
				case InputDataList.KeyEventType.Up:
					if (Input.GetKeyUp(item.keyCode)) {
						item.keyEvent();
					}
					break;

			}
		}
	}

	public void SubscribeEventHandler(string eventName, System.Action handler) {
		var item = m_InputDataList.m_KeyList.Where(k => k.inputName == eventName).FirstOrDefault();
		if (item == null) {
			throw new KeyNotFoundException(string.Format("Event with name {0} was not found when trying to subscribe", eventName));
		}
		
		item.keyEvent += handler;
	}

}
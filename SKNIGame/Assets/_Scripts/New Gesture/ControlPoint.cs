using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPoint : MonoBehaviour {

	public Text m_IndexText;

	GestureCircleController m_GestureController;
	int m_Index;

	public void Initialize(GestureCircleController gestControler, int index) {
		m_GestureController = gestControler;
		m_Index = index;

		m_IndexText.text = index.ToString();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Hand")) {
			m_GestureController.ControlPointTouched(m_Index, transform.position);
		}
	}
}
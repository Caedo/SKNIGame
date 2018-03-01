using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour {

	NewGestureController m_GestureController;
	int m_Index;

	public void Initialize(NewGestureController gestControler, int index){
		m_GestureController = gestControler;
		m_Index = index;
	}

	private void OnTriggerStay(Collider other) {
		m_GestureController.AddIndexToSequence(m_Index);
	}
}

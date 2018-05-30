using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameStartDamagePoint : MonoBehaviour {

	public UnityEvent m_StarColliderDestroyed;

	private void OnTriggerEnter(Collider other) {
		m_StarColliderDestroyed.Invoke();
		gameObject.SetActive(false);
	}
}

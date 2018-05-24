using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : MonoBehaviour {

	public Transform m_PlyerAnchor;
	public GameObject m_SelectionVisual;

	private void Start() {
		m_SelectionVisual.SetActive(false);
	}

	public void SetSelection(bool selection) {
		m_SelectionVisual.SetActive(selection);
	}

}
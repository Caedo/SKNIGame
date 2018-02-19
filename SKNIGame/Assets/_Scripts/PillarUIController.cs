using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillarUIController : MonoBehaviour {

	public Transform m_CanvasTransform;
	public Image m_HealthImage;

	Transform m_LookTarget;

	private void Awake() {
		GetComponent<PillarHealth>().OnPillarTakeDamage += OnPillarTakeDamage;
		m_LookTarget = Camera.main.transform;
	}

	private void Update() {
		m_CanvasTransform.LookAt(m_LookTarget);
	}

	private void OnPillarTakeDamage(float currentHealth, float maxHealth) {
		m_HealthImage.fillAmount = currentHealth / maxHealth;

		//second check... maybe this can be done better?
		if (currentHealth <= 0) {
			m_CanvasTransform.gameObject.SetActive(false);
		}
	}
}
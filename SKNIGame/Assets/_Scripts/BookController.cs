using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour {

	public float m_CreationTime = 1f;

	bool m_IsVisible;
	Material m_DissolveMaterial;

	float m_CreationSpeed;
	float m_VisibilityPercent;
	float m_TargetVisPercent;

	private void Awake() {
		m_DissolveMaterial = GetComponent<Renderer>().material;
	}

	private void Update() {
		//ONLY FOR TEST - we want this in some input manager, but later
		if (Input.GetMouseButtonDown(1)) {
			ToggleVisibility();
		}
	}

	public void ToggleVisibility() {
		m_IsVisible = !m_IsVisible;
		m_TargetVisPercent = m_IsVisible ? 1 : 0;

		StopCoroutine(ChangeVisibility());
		StartCoroutine(ChangeVisibility());
	}

	IEnumerator ChangeVisibility() {
		float timer = 0f;
		while (timer <= 1) {
			m_VisibilityPercent = Mathf.Lerp(1 - m_TargetVisPercent, m_TargetVisPercent, timer);
			timer += Time.deltaTime * m_CreationSpeed;

			m_DissolveMaterial.SetFloat("_DissolveStrength", m_VisibilityPercent);

			yield return null;
		}

		m_VisibilityPercent = m_TargetVisPercent;
		m_DissolveMaterial.SetFloat("_DissolveStrength", m_VisibilityPercent);

	}

	private void OnValidate() {
		m_CreationSpeed = 1 / m_CreationTime;
	}
}
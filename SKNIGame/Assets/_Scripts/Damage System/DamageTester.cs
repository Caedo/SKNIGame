using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour {
	public Element attackElement;
	public float m_Damage;

	Transform m_CamTransform;

	public LineRenderer m_Line;

	private void Start() {
		m_CamTransform = Camera.main.transform;
		InputController.Instance.SubscribeEventHandler("CastSpellDown", Fire);
	}

	void Fire() {

		Ray ray = new Ray(m_CamTransform.position, m_CamTransform.forward);
		RaycastHit hit;
		m_Line.SetPosition(0, m_CamTransform.position + Vector3.down * 0.5f);
		m_Line.SetPosition(1, ray.origin + ray.direction * 200f);
		if (Physics.Raycast(ray, out hit)) {
			var health = hit.collider.GetComponent<EnemyHealh>();
			if (health) {
				health.Damage(m_Damage, attackElement);
			}
			m_Line.SetPosition(1, hit.point);
		}

	}
}
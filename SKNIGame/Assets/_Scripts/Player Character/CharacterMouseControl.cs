using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseControl : MonoBehaviour {

	public Vector2 m_MouseSensitivity;
	public Vector2 m_VerticalClampMinMax;
	public Transform m_Camera;

	[Header("Position Selection")]
	public LayerMask m_TowerMask;
	public Transform m_PointerOriginTransform;
	private LineRenderer m_PointerLine;

	private Transform m_MoveTransform;

	float m_Pitch;
	float m_Yaw;

	void Awake() {
		m_PointerLine = GetComponent<LineRenderer>();
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		m_PointerLine.enabled = false;

	}

	void Update() {
		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		m_Pitch -= mouseDelta.y * m_MouseSensitivity.y;
		m_Yaw += mouseDelta.x * m_MouseSensitivity.x;

		m_Camera.localRotation = Quaternion.Euler(m_Pitch, m_Yaw, 0);

		if (Input.GetMouseButtonDown(1)) {
			m_PointerLine.enabled = true;
		}

		if (Input.GetMouseButton(1)) {
			Ray ray = new Ray(m_PointerOriginTransform.position, m_PointerOriginTransform.forward);
			RaycastHit hit;
			float dist = 200f;
			if (Physics.Raycast(ray, out hit, float.MaxValue, m_TowerMask)) {
				m_MoveTransform = hit.collider.GetComponent<PlayerTower>().m_PlyerAnchor;
				dist = hit.distance;
			} else {
				m_MoveTransform = null;
			}
			Vector3 hitPosition = ray.origin + ray.direction * dist;
			m_PointerLine.SetPositions(new [] { m_PointerOriginTransform.position, hitPosition });
		}

		if (Input.GetMouseButtonUp(1)) {
			m_PointerLine.enabled = false;

			if (m_MoveTransform != null) {
				transform.position = m_MoveTransform.position;
			}
		}
	}
}
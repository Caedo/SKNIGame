using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseControl : MonoBehaviour {

	public Vector2 m_MouseSensitivity;
	public Vector2 m_VerticalClampMinMax;
	public Transform m_Camera;

	float m_Pitch;
	
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;	
	}
	
	void Update () {
		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		transform.Rotate(Vector3.up * mouseDelta.x * m_MouseSensitivity.x);
		
		m_Pitch += mouseDelta.y * m_MouseSensitivity.y;
		m_Pitch = Mathf.Clamp(m_Pitch, m_VerticalClampMinMax.x, m_VerticalClampMinMax.y);

		Quaternion quat = Quaternion.AngleAxis(m_Pitch, Vector3.left);
		m_Camera.localRotation = quat;
	}
}

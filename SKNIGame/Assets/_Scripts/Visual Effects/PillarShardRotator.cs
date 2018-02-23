using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarShardRotator : MonoBehaviour {

	public Transform m_PillarTransform;
	public float m_RotationSpeed;
	public float m_SelfRotationSpeed;

	void LateUpdate() {

		transform.RotateAround(m_PillarTransform.position, Vector3.up, m_RotationSpeed * Time.deltaTime);
		transform.Rotate(Vector3.forward * m_SelfRotationSpeed * Time.deltaTime);
	}

}
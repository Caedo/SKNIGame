using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class SpellProjectile : MonoBehaviour {

	public float m_ProjectileSpeed;

	SpellData m_SpellData;
	Rigidbody m_Body;

	public void Initialize(SpellData spellData) {
		m_SpellData = spellData;
	}

	private void Awake() {
		m_Body = GetComponent<Rigidbody>();
	}

	void Start()
	{
		m_Body.velocity = transform.forward * m_ProjectileSpeed;
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other)
		Instantiate(m_SpellData.m_ImpactEffectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

		Destroy(gameObject);
	}
}
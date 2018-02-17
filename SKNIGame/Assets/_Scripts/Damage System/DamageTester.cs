using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{

	public Element attackElement;
	public float m_Damage;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log(hit.collider.name);
				LivingEntity entity = hit.collider.GetComponent<LivingEntity>();

				if (entity)
				{
					entity.Damage(m_Damage, attackElement);
				}
			}
		}
	}
}
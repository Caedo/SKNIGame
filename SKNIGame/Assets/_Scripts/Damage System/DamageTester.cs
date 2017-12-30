﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{

	public Element attackElement;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				LivingEntity entity = hit.collider.GetComponent<LivingEntity>();

				if (entity)
				{
					entity.Damage(50, attackElement);
				}
			}
		}
	}
}
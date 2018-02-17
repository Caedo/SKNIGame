using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHealth : LivingEntity {

	public static System.Action<PillarHealth> OnPillarDestroy;

	public GameObject m_LivingModel; //Model that exists when HP is greater than 0
	public GameObject m_DestroyedModel;

    protected override void Die()
    {
		this.enabled = false;
		
		if(OnPillarDestroy != null) {
			OnPillarDestroy(this);
		}

		if(m_LivingModel != null)
			m_LivingModel.SetActive(false);
		if(m_DestroyedModel != null)			
			m_DestroyedModel.SetActive(true);		

		//Destroy(this);
    }
}

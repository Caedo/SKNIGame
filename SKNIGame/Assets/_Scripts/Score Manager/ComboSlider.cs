using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSlider : MonoBehaviour {

	public ScoreManager m_scoreManager;
	Slider m_comboValueSlider;
	// Use this for initialization
	void Start () {
		m_comboValueSlider = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		float maxTime = m_scoreManager.m_CurrentMaxTargetComboTime;
		if(maxTime != 0f) {
			maxTime = 1f;
		}
		m_comboValueSlider.maxValue = maxTime;
		m_comboValueSlider.value = m_scoreManager.m_TargetComboTime;
	}
}

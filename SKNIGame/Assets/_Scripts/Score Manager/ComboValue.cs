using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboValue : MonoBehaviour {

	public ScoreManager m_scoreManager;
	Text m_comboValueText;
	// Use this for initialization
	void Start () {
		m_comboValueText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		m_comboValueText.text = m_scoreManager.m_Combo.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreValue : MonoBehaviour {

	public ScoreManager m_scoreManager;
	Text m_scoreValueText;
	// Use this for initialization
	void Start () {
		m_scoreValueText = gameObject.GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
		m_scoreValueText.text = m_scoreManager.m_Score.ToString();
	}
}

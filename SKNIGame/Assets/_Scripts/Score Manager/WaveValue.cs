using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveValue : MonoBehaviour {

	public Spawner m_spawner;
	Text m_waveValueText;
	// Use this for initialization
	void Start () {
		m_waveValueText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		m_waveValueText.text = (m_spawner.m_WaveNumber + 1).ToString();
	}
}

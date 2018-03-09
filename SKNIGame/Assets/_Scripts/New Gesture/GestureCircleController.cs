using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCircleController : MonoBehaviour {

	public ControlPoint m_ControlPointPrefab;
	public float m_ShowDuration = 0.5f;

	private ControlPoint[] m_ControlPoints;
	private Vector3[] m_ControlPointsTargetPositions;

	NewGestureController m_GestureController;
	LineRenderer m_Line;

	float m_TargetPercent;
	bool m_Show;

	private void Awake() {
		m_Line = GetComponent<LineRenderer>();
		m_Line.positionCount = 1;
	}

	private void Update() {
		if (m_Line.positionCount != 1) {
			m_Line.SetPosition(m_Line.positionCount - 1, m_GestureController.HandPosition);
		}
	}

	public void Create(int pointsCount, float radius, NewGestureController controller) {

		m_ControlPoints = new ControlPoint[pointsCount];
		m_ControlPointsTargetPositions = new Vector3[pointsCount];

		m_GestureController = controller;

		//Spawn Control points
		for (int i = 0; i < pointsCount; i++) {
			//compute angle
			float angle = ((float) i / pointsCount) * 360f * Mathf.Deg2Rad;
			//compute position
			Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * radius;

			//instantiate and initialize
			m_ControlPoints[i] = Instantiate(m_ControlPointPrefab, transform);
			m_ControlPoints[i].Initialize(this, i);
			m_ControlPoints[i].transform.position = pos;

			m_ControlPointsTargetPositions[i] = pos;
		}

		//hide this gameobject
		gameObject.SetActive(false);
	}

	//Called from CotrolPoint when it's touched
	public void ControlPointTouched(int index, Vector3 position) {
		if (m_GestureController.AddIndexToSequence(index)) {

			m_Line.SetPosition(m_Line.positionCount - 1, position);
			m_Line.positionCount++;
		}
	}

	public void Show() {
		gameObject.SetActive(true);
		m_Show = true;
		//StopAllCoroutines();
		//StartCoroutine(PointsAnimation());
	}

	public void Hide() {
		m_Line.positionCount = 1;

		m_Show = false;
		gameObject.SetActive(false);
		//StopAllCoroutines();
		//StartCoroutine(PointsAnimation());
	}

	//Currently not working	
	IEnumerator PointsAnimation() {
		while (m_TargetPercent > 0 && m_TargetPercent < 1) {
			m_TargetPercent += Time.deltaTime * (m_Show ? 1 : -1);

			for (int i = 0; i < m_ControlPoints.Length; i++) {
				var targetPos = m_Show ? m_ControlPointsTargetPositions[i] : Vector3.zero;
				m_ControlPoints[i].transform.localPosition = Vector3.Lerp(m_ControlPoints[i].transform.localPosition, targetPos, m_TargetPercent);
			}

			yield return null;
		}

		m_TargetPercent = m_Show ? 1 : 0;
		for (int i = 0; i < m_ControlPoints.Length; i++) {
			var targetPos = m_Show ? m_ControlPointsTargetPositions[i] : Vector3.zero;
			m_ControlPoints[i].transform.localPosition = targetPos;
		}
	}
}
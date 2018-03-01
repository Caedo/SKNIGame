using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGestureController : MonoBehaviour {
    public ControlPoint m_ControlPoint;
    public float m_SpawnRadius;
    public Transform m_CircleOrigin;
    public Transform m_HandCollider;

    public GameObject forTest;

    private int m_ControlPointsCount = 8;
    Camera m_Cam;

    private ControlPoint[] m_ControlPoints;
    List<int> m_CurrentSequence = new List<int>();
    LineRenderer m_Line;
    int m_NextLineIndex;

    bool m_IsTracing;

    private Vector3 MouseWorldPostion {
        get {
            Vector3 mousePos = Input.mousePosition;
            return m_Cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        }
    }

    private void Start() {
        m_Line = GetComponent<LineRenderer>();
        m_ControlPoints = new ControlPoint[m_ControlPointsCount];
        CreateCircle();
        m_CircleOrigin.gameObject.SetActive(false);

        m_Cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartTraicing();
        }

        if (Input.GetMouseButtonUp(0)) {
            EndTracing();
        }

        //Just for mouse control...
        if (m_IsTracing) {
            m_HandCollider.position = MouseWorldPostion;

            if(m_NextLineIndex != 0)
                m_Line.SetPosition(m_NextLineIndex, MouseWorldPostion);
        }

    }

    void StartTraicing() {
        Vector3 pos = MouseWorldPostion;

        m_CircleOrigin.gameObject.SetActive(true);
        m_CircleOrigin.position = pos;
        m_HandCollider.position = pos;

        //m_CircleOrigin.LookAt(m_Cam.transform);

        m_CurrentSequence.Clear();
        m_IsTracing = true;
    }

    void EndTracing() {
        m_CircleOrigin.gameObject.SetActive(false);
        m_IsTracing = false;
        m_Line.positionCount = 0;
        m_NextLineIndex = 0;

        int[] testSequence = new int[] { 0, 2, 6 };
        if (m_CurrentSequence.Count == 0)
            return;

        for (int i = 0; i < m_CurrentSequence.Count; i++) {
            if (i >= testSequence.Length || testSequence[i] != m_CurrentSequence[i])
                return;
        }

        //Test....
        Instantiate(forTest, m_CircleOrigin.transform.position, Quaternion.identity);
    }

    void CreateCircle() {
        for (int i = 0; i < m_ControlPointsCount; i++) {
            float angle = ((float) i / m_ControlPointsCount) * 360f * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * m_SpawnRadius;

            m_ControlPoints[i] = Instantiate(m_ControlPoint, m_CircleOrigin);
            m_ControlPoints[i].Initialize(this, i);

            m_ControlPoints[i].transform.localPosition = pos;
            //Debug.Log(m_ControlPoints[i].transform.position);
        }
    }

    public void AddIndexToSequence(int index) {
        if (!m_CurrentSequence.Contains(index)) {
            Debug.Log("Adding to Sequence: " + index);
            m_CurrentSequence.Add(index);

            m_Line.positionCount = m_CurrentSequence.Count + 1;

            m_Line.SetPosition(m_NextLineIndex, m_ControlPoints[index].transform.position);
            m_NextLineIndex++;
        }
    }
}
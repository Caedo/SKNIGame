using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGestureController : MonoBehaviour {
    public NewGestureLibrary m_Library;
    public Transform m_HandCollider;
    public float m_CircleRadius;

    public System.Action<NewGesture, Vector3> OnGestureMatch;

    GestureCircleController m_CircleController;

    private int m_ControlPointsCount = 8;

    List<int> m_CurrentSequence = new List<int>();


    bool m_IsTracing;

    private Vector3 MouseWorldPostion {
        get {
            return MouseUtility.MouseWorldPosition(10);
        }
    }

    public Vector3 HandPosition {
        get {
            return m_HandCollider.position;
        }
    }

    private void Start() {    
        m_CircleController = GetComponentInChildren<GestureCircleController>();
        m_CircleController.Create(m_ControlPointsCount, m_CircleRadius, this);
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
        }

    }

    void StartTraicing() {
        m_CircleController.Show();
        m_CircleController.transform.position = MouseWorldPostion;
        m_HandCollider.position = MouseWorldPostion;

        //m_CircleOrigin.LookAt(m_Cam.transform);

        m_CurrentSequence.Clear();
        m_IsTracing = true;
    }

    void EndTracing() {
        m_CircleController.Hide();
        m_IsTracing = false;

        var gesture = m_Library.MatchGesture(m_CurrentSequence);
        if (gesture != null) {
            Debug.Log(gesture.m_Name);

            if(OnGestureMatch != null) {
                OnGestureMatch(gesture, m_CircleController.transform.position);
            }
        }
    }


    public bool AddIndexToSequence(int index) {
        if (!m_CurrentSequence.Contains(index)) {
            Debug.Log("Adding to Sequence: " + index);
            m_CurrentSequence.Add(index);     

            return true;     
        }

        return false;
    }
}
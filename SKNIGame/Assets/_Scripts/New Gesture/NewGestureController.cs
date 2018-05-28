using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGestureController : MonoBehaviour {
    public NewGestureLibrary m_Library;
    public Transform m_HandCollider;
    public Transform m_PointerRayOrigin;
    
    public float m_CircleRadius;

    public float m_CircleDistance;

    public System.Action<NewGesture, Vector3> OnGestureMatch;

    GestureCircleController m_CircleController;

    private int m_ControlPointsCount = 8;

    List<int> m_CurrentSequence = new List<int>();

    bool m_IsTracing;

    Plane m_CirclePlane;


    private Vector3 MouseWorldPostion {
        get {
            return MouseUtility.MouseWorldPosition(m_CircleDistance);
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

        if(m_PointerRayOrigin == null)
            m_PointerRayOrigin = Camera.main.transform;

        InputController.Instance.SubscribeEventHandler("CreateSpellDown", StartTraicing);
        InputController.Instance.SubscribeEventHandler("CreateSpellUp", EndTracing);        
    }

    void Update() {
        //Just for mouse control...
        if (m_IsTracing) {
            //m_HandCollider.position = MouseWorldPostion;
            Ray ray = new Ray(m_PointerRayOrigin.position, m_PointerRayOrigin.forward);
            float dist;
            if (m_CirclePlane.Raycast(ray, out dist)) {
                m_HandCollider.position = ray.GetPoint(dist);
            }
        }

    }

    void StartTraicing() {
        
        m_CircleController.transform.position = MouseWorldPostion;        
        m_CircleController.transform.LookAt(m_PointerRayOrigin);

        m_CircleController.Show();
        m_HandCollider.position = MouseWorldPostion;

        m_CirclePlane = new Plane(m_CircleController.transform.forward, m_CircleController.transform.position);

        m_CurrentSequence.Clear();
        m_IsTracing = true;

    }

    void EndTracing() {
        m_CircleController.Hide();
        m_IsTracing = false;

        var gesture = m_Library.MatchGesture(m_CurrentSequence);
        if (gesture != null) {
            Debug.Log(gesture.m_Name);

            if (OnGestureMatch != null) {
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
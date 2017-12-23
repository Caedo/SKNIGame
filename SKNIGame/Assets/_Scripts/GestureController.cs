using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureController : MonoBehaviour
{

	public enum ControlType { Mouse, ViveController }

	public System.Action<Gesture> OnGestureFit;

	public ControlType m_ControlType = ControlType.Mouse;

	public GestureLibrary m_Library;
	public float m_NextPointMoveDelta;

	private List<Vector2> m_CurentRoute = new List<Vector2>();
	private bool m_IsTracing;

	private Vector2 m_LastMousePosition;
	private float m_CurrentMoveDelta;

	private Camera m_Cam;

	private Vector3 MouseWorldPostion{
		get{
			Vector3 mousePos = Input.mousePosition;
			return m_Cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
		}
	}

	private void Awake()
	{
		m_Cam = Camera.main;
		
	}

	private void Update()
	{
		switch (m_ControlType)
		{
			case ControlType.Mouse:
				MouseControl();
				break;
			case ControlType.ViveController:
				break;
		}
	}

	void MouseControl()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartTracing();
		}
		if (Input.GetMouseButtonUp(0))
		{
			EndTracing();
		}

		if (m_IsTracing)
		{
			Vector2 mousePos = MouseWorldPostion;
			m_CurrentMoveDelta += (mousePos - m_LastMousePosition).sqrMagnitude;
			m_LastMousePosition = mousePos;
			//Debug.Log("mouse Pos: " + mousePos + " Delta: " + m_CurrentMoveDelta);

			if (m_CurrentMoveDelta > m_NextPointMoveDelta * m_NextPointMoveDelta)
			{
				//Debug.Log("Added: " + mousePos);
				m_CurrentMoveDelta = 0;
				m_CurentRoute.Add(mousePos);
			}
		}
	}

	public void StartTracing()
	{
		Debug.Log("Start tracing");

		m_LastMousePosition = MouseWorldPostion;

		m_CurentRoute.Clear();
		m_IsTracing = true;
	}

	public void EndTracing()
	{
		Debug.Log("End tracing");

		m_IsTracing = false;
		Gesture gesture = m_Library.FitRoute(m_CurentRoute);
		Debug.Log(gesture == null ? "null" : gesture.name);
		if (OnGestureFit != null)
		{
			OnGestureFit(gesture);
		}
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < m_CurentRoute.Count - 1; i++)
		{
			Gizmos.color = Color.Lerp(Color.green, Color.red, (float) i / (m_CurentRoute.Count - 1));
			Gizmos.DrawLine(m_CurentRoute[i], m_CurentRoute[i + 1]);
		}
	}

}
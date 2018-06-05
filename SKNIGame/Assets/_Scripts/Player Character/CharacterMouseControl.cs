using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseControl : MonoBehaviour {

	public Vector2 m_MouseSensitivity;
	public Vector2 m_VerticalClampMinMax;
	public Transform m_Camera;

	[Header("Position Selection")]
	public LayerMask m_TowerMask;
	public Transform m_PointerOriginTransform;
	private LineRenderer m_PointerLine;

	private PlayerTower m_SelectedTower;

	bool pointingToMove;

	float m_Pitch;
	float m_Yaw;

	void Awake() {
		m_PointerLine = GetComponent<LineRenderer>();
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		m_PointerLine.enabled = false;

        //InputController.Instance.SubscribeEventHandler("MoveKeyDown", HandleMoveKeyDown);
        //InputController.Instance.SubscribeEventHandler("MoveKeyUp", HandleMoveKeyUp);		+
        InputController.Instance.m_LeftController.PadClicked += HandleMoveKeyDown;
        InputController.Instance.m_LeftController.PadUnclicked += HandleMoveKeyUp;

        InputController.Instance.m_RightController.PadClicked += HandleMoveKeyDown;
        InputController.Instance.m_RightController.PadUnclicked += HandleMoveKeyUp;

    }

    void Update() {
		//Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		//m_Pitch -= mouseDelta.y * m_MouseSensitivity.y;
		//m_Yaw += mouseDelta.x * m_MouseSensitivity.x;

		//m_Camera.localRotation = Quaternion.Euler(m_Pitch, m_Yaw, 0);

		if (pointingToMove) {
			Ray ray = new Ray(m_PointerOriginTransform.position, m_PointerOriginTransform.forward);
			RaycastHit hit;
			float dist = 200f;
			if (Physics.Raycast(ray, out hit, float.MaxValue, m_TowerMask)) {
				m_SelectedTower = hit.collider.GetComponent<PlayerTower>();
				m_SelectedTower.SetSelection(true);
				dist = hit.distance;
			} else {
				m_SelectedTower?.SetSelection(false);
				m_SelectedTower = null;
			}
			Vector3 hitPosition = ray.origin + ray.direction * dist;
			m_PointerLine.SetPositions(new [] { m_PointerOriginTransform.position, hitPosition });
		}

	}

	void HandleMoveKeyDown(object sender, ClickedEventArgs e)
    {

		m_PointerLine.enabled = true;
		pointingToMove = true;
	}
	void HandleMoveKeyUp(object sender, ClickedEventArgs e)
    {
		m_PointerLine.enabled = false;

		if (m_SelectedTower != null) {
			m_SelectedTower.SetSelection(false);
			transform.position = m_SelectedTower.m_PlyerAnchor.position;
		}

		pointingToMove = false;		
	}
}
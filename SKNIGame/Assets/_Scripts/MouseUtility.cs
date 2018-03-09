using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseUtility  {

	public static Vector3 MouseWorldPosition(float posZ) {
		Vector2 mousePos = Input.mousePosition;
		return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, posZ));
	}

	public static Vector3 MouseWorldPosition() {
		Vector2 mousePos = Input.mousePosition;
		return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
	}
}

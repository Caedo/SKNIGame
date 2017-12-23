using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
	///<Summary> Calculate distance between point and line passing through points A and B </Summary>
	public static float DistPointToLine(Vector2 A, Vector2 B, Vector2 point)
	{
		return Mathf.Abs((point.x - A.x) * (-B.y + A.y) + (point.y - A.y) * (B.x - A.x)) / Mathf.Sqrt((-B.y + A.y) * (-B.y + A.y) + (B.x - A.x) * (B.x - A.x));
	}
}
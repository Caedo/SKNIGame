// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class OldGesture
// {
// 	public string name;
// 	[Range(0f, 1f)]
// 	public float threshold;
// 	public List<Vector2> route;

// 	public bool CompareRoute(List<Vector2> _route)
// 	{
// 		int goodPointsCount = 0;
// 		for (int i = 0; i < _route.Count; i++)
// 		{
// 			Vector2 point = _route[i];
// 			for (int j = 0; j < route.Count - 1; j++)
// 			{
// 				int toIndex = (j + 1) % route.Count;
// 				float dist = MathUtility.DistPointToLine(route[j], route[toIndex], point);
// 				//Debug.Log(dist);
// 				if (dist < threshold)
// 				{
// 					goodPointsCount++;
// 					break;
// 				}
// 			}
// 		}
// 		Debug.Log("Good Points: " + goodPointsCount + " Count: " + _route.Count);
// 		return goodPointsCount >= _route.Count;
// 	}
// }

// [CreateAssetMenu()]
// public class OldGestureLibrary : ScriptableObject
// {
// 	public List<Gesture> m_Gestures;

// 	public Gesture FitRoute(List<Vector2> route)
// 	{
// 		var normalizedRoute = NormalizeRoute(route);

// 		for (int i = 0; i < m_Gestures.Count; i++)
// 		{
// 			if (m_Gestures[i].CompareRoute(normalizedRoute))
// 			{
// 				return m_Gestures[i];
// 			}
// 		}

// 		//If gesture wasn't found return null
// 		return null;
// 	}

// 	public static List<Vector2> NormalizeRoute(List<Vector2> route)
// 	{
// 		List<Vector2> newRoute = new List<Vector2>();
// 		Vector2 min = Vector2.one * float.MaxValue;
// 		Vector2 max = Vector2.one * float.MinValue;

// 		//Find min and max
// 		for (int i = 0; i < route.Count; i++)
// 		{
// 			if (route[i].x > max.x)
// 			{
// 				max.x = route[i].x;
// 			}
// 			if (route[i].y > max.y)
// 			{
// 				max.y = route[i].y;
// 			}
// 			if (route[i].x < min.x)
// 			{
// 				min.x = route[i].x;
// 			}
// 			if (route[i].y < min.y)
// 			{
// 				min.y = route[i].y;
// 			}
// 		}

// 		//move all points to <0,1> space
// 		for (int i = 0; i < route.Count; i++)
// 		{
// 			Vector2 toAdd = new Vector2(Mathf.InverseLerp(min.x, max.x, route[i].x),
// 				Mathf.InverseLerp(min.y, max.y, route[i].y));

// 			//Debug.Log(toAdd);
// 			newRoute.Add(toAdd);
// 		}

// 		return newRoute;
// 	}
// }
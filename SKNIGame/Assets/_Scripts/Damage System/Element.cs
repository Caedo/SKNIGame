using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Element : ScriptableObject
{
	static readonly float m_StrongAgainstMultiplier = 2;
	static readonly float m_WeakAgainstMultiplier = 0.5f;

	public List<Element> m_StrongAgainst = new List<Element>();
	public List<Element> m_WeakAgainst = new List<Element>();

	public float GetMultiplierAgainst(Element opposedElement)
	{
		if (m_StrongAgainst.Contains(opposedElement))
		{
			return m_StrongAgainstMultiplier;
		} 
		else if (m_WeakAgainst.Contains(opposedElement))
		{
			return m_WeakAgainstMultiplier;
		}
		else
		{
			return 1;
		}
	}
}
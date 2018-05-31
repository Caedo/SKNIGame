using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData {

	[SerializeField]
	private string name;
	public Gesture m_GestureToMatch;
	public ParticleSystem m_HandParticles;
	public SpellProjectile m_ProjectilePrefab;
	public SpellDamage m_ImpactEffectPrefab;
}

[CreateAssetMenu(menuName = "Spells/Spells Library")]
public class SpellLibrary : ScriptableObject {
	public List<SpellData> m_SpellDataList;

}
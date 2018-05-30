using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour {

    public float m_YStartOffset;

    public float m_DamageValue;
    public float m_DamageRadius;
    public LayerMask m_DamagableMask;
    public Element m_DamageElement;

    protected ParticleSystem m_Particles;

    protected void Awake() {
        m_Particles = GetComponent<ParticleSystem>();
        if (m_Particles == null) {
            m_Particles = GetComponentInChildren<ParticleSystem>();
        }
        transform.position += Vector3.up * m_YStartOffset;        
    }

    protected virtual void Start() {
        DoDamage();
        DestroyAfterParicleDuration();
    }

    protected void DoDamage() {
        var colliders = Physics.OverlapSphere(transform.position, m_DamageRadius, m_DamagableMask);
        foreach (var item in colliders) {
            var entity = item.GetComponent<EnemyHealh>();
            if (entity) {
                entity.Damage(m_DamageValue, m_DamageElement);
            }
        }
    }

    protected void DestroyAfterParicleDuration() {
        Destroy(gameObject, m_Particles.main.duration);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_DamageRadius);
    }
}
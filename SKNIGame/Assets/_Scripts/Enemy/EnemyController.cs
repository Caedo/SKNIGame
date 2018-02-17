using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealh), typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {

    public EnemyStats m_Stats;

    public EnemyHealh Health { get; private set; }
    PillarHealth m_Target;
    NavMeshAgent m_Agent;

    float m_AttackTimer;

    private void Awake() {
        Health = GetComponent<EnemyHealh>();
        m_Agent = GetComponent<NavMeshAgent>();

        PillarHealth.OnPillarDestroy += OnPillarDestroyed;
    }

    private void Start() {
        //Initialize properties
        Health.Initialize(m_Stats);
        m_Agent.speed = m_Stats.m_MovementSpeed;

        //Find attack target
        FindTarget();
    }

    private void Update() {

        m_AttackTimer += Time.deltaTime;
        if (m_AttackTimer >= m_Stats.TimeBetweenAttacks &&
            Vector3.Distance(m_Target.transform.position, transform.position) <= m_Stats.m_AttackRange) {

            m_AttackTimer = 0;
            Attack();
        }
    }

    private void Attack() {
        m_Target.Damage(m_Stats.m_Damage, m_Stats.m_Element);
    }

    //TODO: Only for test. Change it to something normal...
    //Find closest target and start move
    private void FindTarget() {
        var pillars = GameObject.FindObjectsOfType<PillarHealth>();
        if (pillars.Length == 0)
            return;

        m_Target = pillars[0];
        float minDist = float.MaxValue;
        for (int i = 0; i < pillars.Length; i++) {
            float dist = Vector3.Distance(transform.position, pillars[i].transform.position);
            if (dist < minDist && pillars[i].enabled == true) {
                minDist = dist;
                m_Target = pillars[i];
            }
        }

        Vector3 dir = (transform.position - m_Target.transform.position).normalized;

        //Set Destination based on actual attack range
        m_Agent.destination = m_Target.transform.position + dir * (m_Stats.m_AttackRange - 0.2f);
        //Debug.Log(m_Agent.destination);
    }

    void OnPillarDestroyed(PillarHealth pillar) {
        //when attacked pillar is destroyed find next
        if (pillar == m_Target) {
            FindTarget();
        }
    }
}
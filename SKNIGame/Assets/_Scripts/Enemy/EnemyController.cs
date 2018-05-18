using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TargetPrioritization { LowestHealth, GreatestHealth, Closest, Farthest }

[RequireComponent(typeof(EnemyHealh), typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {

    public EnemyStats m_Stats;
    public TargetPrioritization m_TargetPrioritization;
    public EnemyHealh Health { get; private set; }
    PillarHealth m_Target;
    NavMeshAgent m_Agent;

    float m_AttackTimer;

    List<PillarHealth> m_ActivePillars;

    private void Awake() {
        Health = GetComponent<EnemyHealh>();
        m_Agent = GetComponent<NavMeshAgent>();

        PillarHealth.OnPillarDestroy += OnPillarDestroyed;
    }

    public void InitializeTargets(List<PillarHealth> pillars) {
        m_ActivePillars = pillars;
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
            m_Target != null &&
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
        m_Target = null;
        if (m_ActivePillars.Count == 0)
            return;

        switch (m_TargetPrioritization) {
            case TargetPrioritization.Closest:
            case TargetPrioritization.Farthest:
                FindTargetBasedOnDistance();
                break;
            case TargetPrioritization.GreatestHealth:
            case TargetPrioritization.LowestHealth:
                FindTargetBasedOnHealth();
                break;
        }

        SetDestination();
    }

    void FindTargetBasedOnHealth() {
        bool findGreatest = m_TargetPrioritization == TargetPrioritization.GreatestHealth;

        float compValue = findGreatest ? float.MinValue : float.MaxValue;

        for (int i = 0; i < m_ActivePillars.Count; i++) {
            float health = m_ActivePillars[i].CurrentHealth;
            if (findGreatest ? health > compValue : compValue > health) {
                compValue = health;
                m_Target = m_ActivePillars[i];
            }
        }
    }

    void FindTargetBasedOnDistance() {
        bool findGreatest = m_TargetPrioritization == TargetPrioritization.Farthest;

        float compValue = findGreatest ? float.MinValue : float.MaxValue;

        for (int i = 0; i < m_ActivePillars.Count; i++) {
            float dist = (transform.position - m_ActivePillars[i].transform.position).sqrMagnitude;
            if (findGreatest ? dist > compValue : compValue > dist) {
                compValue = dist;
                m_Target = m_ActivePillars[i];
            }
        }
    }
    void SetDestination() {
        Vector3 dir = (transform.position - m_Target.transform.position).normalized;

        //Set Destination based on actual attack range
        m_Agent.destination = m_Target.transform.position + dir * (m_Stats.m_AttackRange - 0.5f);
        //Debug.Log(m_Agent.destination);
    }

    void OnPillarDestroyed(PillarHealth pillar) {
        m_ActivePillars.Remove(pillar);

        //when attacked pillar is destroyed find next
        if (pillar == m_Target) {
            FindTarget();
        }
    }

    private void OnDestroy() {
        PillarHealth.OnPillarDestroy -= OnPillarDestroyed;
    }
}
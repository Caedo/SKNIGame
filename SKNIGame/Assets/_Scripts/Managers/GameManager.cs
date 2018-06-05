using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject m_Player;
    public PlayerTower m_StartingTower;

    public static GameManager Instance { get; private set; }

    public List<PillarHealth> ActivePillars { get; private set; }

    public bool IsGameStarted { get; private set; }

    public System.Action OnGameOver;
    public System.Action OnGameWon;

    private void Awake() {
        //I'm evil so I'm creating singletons!
        if (Instance != null) {
            Debug.LogWarning("Second GameManager on Scene, deleting old one!");
            DestroyImmediate(Instance.gameObject);
        }

        Instance = this;

        //Subscribe OnPillarDestroyed to static event
        PillarHealth.OnPillarDestroy += OnPillarDestroyed;
    }

    private void Start() {
        //Probably this should be changed
        ActivePillars = FindObjectsOfType<PillarHealth>().ToList();

        m_Player.transform.position = m_StartingTower.m_PlyerAnchor.position;
    }

    void OnPillarDestroyed(PillarHealth pillar) {
        ActivePillars.Remove(pillar);

        //When all pillars are destroyed game is over
        if (ActivePillars.Count <= 0) {
            GameOver();
        }
    }

    public void StartGame() {
        IsGameStarted = true;
    }

    void GameOver() {
        OnGameOver?.Invoke();
    }

    public void GameWon() {
        IsGameStarted = false;
        OnGameWon?.Invoke();
    }
}
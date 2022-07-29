using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
        EventCenter.AddListener(EventType.AddScore, AddScore);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.AddScore, AddScore);
    }

    public bool IsGameStarted { get; set; }
    public bool IsGamePause { get; set; }
    public bool IsGameOver { get; set; }

    public int Score { get; set; }

    private void AddScore() {
        Score++;
        EventCenter.Broadcast(EventType.UpdateScoreText, Score);
    }
}
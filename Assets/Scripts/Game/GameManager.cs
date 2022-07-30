using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private ManagerVars _vars;

    private void Awake() {
        Instance = this;
        _vars = ManagerVars.GetManagerVars();
        FallTime = _vars.defaultPlatformFallTime;
        EventCenter.AddListener(EventType.AddScore, AddScore);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.AddScore, AddScore);
    }

    public bool IsGameStarted { get; set; }
    public bool IsGamePause { get; set; }
    public bool IsGameOver { get; set; }

    public float FallTime { get; set; }
    public int Score { get; set; }


    private void AddScore() {
        Score++;
        if (Score % 50 == 0) {
            FallTime /= 2;
            if (FallTime < 0.2) {
                FallTime = 0.2f;
            }
        }

        EventCenter.Broadcast(EventType.UpdateScoreText, Score);
    }
}
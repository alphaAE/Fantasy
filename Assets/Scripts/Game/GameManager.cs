using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private ManagerVars _vars;

    public bool IsGameStarted { get; set; }
    public bool IsGamePause { get; set; }
    public bool IsGameOver { get; set; }

    public float FallTime { get; set; }
    public int Score { get; set; }

    private void Awake() {
        Instance = this;
        _vars = ManagerVars.GetManagerVars();
        FallTime = _vars.defaultPlatformFallTime;
        EventCenter.AddListener(EventType.AddScore, AddScore);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.AddScore, AddScore);
    }

    private void Start() {
        if (GameData.IsAgainGame) {
            GameData.IsAgainGame = false;
            EventCenter.Broadcast(EventType.ShowGamePanel);
            Instance.IsGameStarted = true;
            return;
        }

        EventCenter.Broadcast(EventType.ShowMainPanel);
    }


    private void AddScore() {
        Score++;
        EventCenter.Broadcast(EventType.UpdateScoreText, Score);

        // 缩减掉落时间
        if (Score % 50 == 0) {
            FallTime /= 2;
            if (FallTime < 0.4) {
                FallTime = 0.4f;
            }
        }
    }
}